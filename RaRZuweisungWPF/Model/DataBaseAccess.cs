using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RaRZuweisungWPF.Model
{
    internal class DataBaseAccess
    {
        private string connectionstring;

        private SqliteConnection connection;
        public DataBaseAccess()
        {
            try
            {
                string dbPath = Path.Combine(AppContext.BaseDirectory, "rarDataBase.db");
                string connectionstringbackslash = $"Data Source={dbPath}";
                this.connectionstring = connectionstringbackslash.Replace("\\", "/");
                this.connection = new SqliteConnection(connectionstring);
                using (connection)
                {
                    connection.Open();
                    var createSql = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "blueprint.sql"));
                    using var command = connection.CreateCommand();
                    command.CommandText = createSql;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }



        }
        public void writeParticipant(Participant participant)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"insert into Participant values(@Name, @Old, @A1, @A2, @A3, @A4, @A5);";
                    command.Parameters.AddWithValue("@Name", participant.Name);
                    int old = 0;
                    if (participant.Old) { old = 1; }
                    command.Parameters.AddWithValue("@Old", old);
                    int[] available = { 0, 0, 0, 0, 0 };
                    for (int i = 0; i < 5; i++)
                    {
                        if (participant.Availability[i + 1]) { available[i] = 1; }
                    }
                    command.Parameters.AddWithValue("@A1", available[0]);
                    command.Parameters.AddWithValue("@A2", available[1]);
                    command.Parameters.AddWithValue("@A3", available[2]);
                    command.Parameters.AddWithValue("@A4", available[3]);
                    command.Parameters.AddWithValue("@A5", available[4]);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }

        }

        public void deleteParticipant(Participant participant)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"delete from Participant where ParticipantName = @Name;";
                    command.Parameters.AddWithValue("@Name", participant.Name);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }

        }

        public void changeAvailability(Participant participant, int round)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string sql = @"update Participant set Available" + round + " = @Available where ParticipantName = @Name;";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@Name", participant.Name);
                    int available = 0;
                    if (participant.Availability[round]) { available = 1; }
                    command.Parameters.AddWithValue("@Available", available);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public List<Participant> readParticipants()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select ParticipantName, Old, Available1, Available2, Available3, Available2, Available5 from Participant;";
                    List<Participant> participants = new List<Participant>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool old = reader.GetInt32(1) == 1;
                            bool[] available = new bool[5];
                            available[0] = reader.GetInt32(2) == 1;
                            available[1] = reader.GetInt32(3) == 1;
                            available[2] = reader.GetInt32(4) == 1;
                            available[3] = reader.GetInt32(5) == 1;
                            available[4] = reader.GetInt32(6) == 1;
                            participants.Add(new Participant(reader.GetString(0), old, available[0], available[1], available[2], available[3], available[4]));
                        }
                        return participants;
                    }
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        private Participant readParticipant(string name)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select Old, Available1, Available2, Available3, Available2, Available5 from Participant where ParticipantName = @name;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool old = reader.GetInt32(1) == 1;
                            bool[] available = new bool[5];
                            available[0] = reader.GetInt32(2) == 1;
                            available[1] = reader.GetInt32(3) == 1;
                            available[2] = reader.GetInt32(4) == 1;
                            available[3] = reader.GetInt32(5) == 1;
                            available[4] = reader.GetInt32(6) == 1;
                            return new Participant(name, old, available[0], available[1], available[2], available[3], available[4]);
                        }
                        return null;
                    }
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public List<RaR2> readRaR2Round(int round)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select P1.ParticipantName, P2.ParticipantName from RaR2 inner join Participant as P1 on RaR2.Participant1 = P1.ParticipantName inner join Participant as P2 on RaR2.Participant2 = P2.ParticipantName where RaR2.Round = @Round;";
                    command.Parameters.AddWithValue("@Round", round);
                    List<RaR2> rars = new List<RaR2>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rars.Add(new RaR2(readParticipant(reader.GetString(0)), readParticipant(reader.GetString(1)), round));
                        }
                        return rars;
                    }
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        public List<RaR3> readRaR3Round(int round)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select P1.ParticipantName, P2.ParticipantName P3.Participants from RaR3 inner join Participant as P1 on RaR2.Participant1 = P1.ParticipantName inner join Participant as P2 on RaR2.Participant2 = P2.ParticipantName inner join Participant as P3 on RaR2.Participant3 = P3.ParticipantName where RaR3.Round = @Round;";
                    command.Parameters.AddWithValue("@Round", round);
                    List<RaR3> rars = new List<RaR3>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rars.Add(new RaR3(readParticipant(reader.GetString(0)), readParticipant(reader.GetString(1)), readParticipant(reader.GetString(2)), round));
                        }
                        return rars;
                    }
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        public void writeRaR(int round, List<RaR2> rars)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    foreach (RaR2 rar in rars)
                    {
                        command.CommandText = @"insert into RaR2 values(@P1, @P2, @Round);";
                        command.Parameters.AddWithValue("@P1", rar.OldParticipant.Name);
                        command.Parameters.AddWithValue("@P2", rar.NewParticipant.Name);
                        command.Parameters.AddWithValue("@Round", round);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        public void writeRaR(int round, List<RaR3> rars)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    foreach (RaR3 rar in rars)
                    {
                        command.CommandText = @"insert into RaR3 values(@P1, @P2, @P3, @Round);";
                        command.Parameters.AddWithValue("@P1", rar.OldParticipant.Name);
                        command.Parameters.AddWithValue("@P2", rar.NewParticipant.Name);
                        command.Parameters.AddWithValue("@P3", rar.EitherParticipant.Name);
                        command.Parameters.AddWithValue("@Round", round);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public void deleteRaR(int round)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    if (readRaR2Round(round).Count == 0)
                    {
                        command.CommandText = @"delete from RaR3 where Round = @round;";
                    }
                    else
                    {
                        command.CommandText = @"delete from RaR2 where Round = @round;";
                    }
                    command.Parameters.AddWithValue("@round", round);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public Dictionary<Participant, Participant> readPairings()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select Participant1, Participant2 from Pairing;";
                    Dictionary<Participant, Participant> pairings = new Dictionary<Participant, Participant>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Participant p1 = readParticipant(reader.GetString(0));
                            Participant p2 = readParticipant(reader.GetString(1));
                            pairings.Add(p1, p2);
                            pairings.Add(p2, p1);
                        }
                    }
                    return pairings;
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public void writePairing(Participant participant1, Participant participant2)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"insert into Pairing values (@name1, @name2);";
                    command.Parameters.AddWithValue("@name1", participant2.Name);
                    command.Parameters.AddWithValue("@name2", participant1.Name);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public void deletePairing(Participant participant1, Participant participant2)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"delete from Pairing where (Participant1 = @name1 And Participant2 = @name2) OR (Participant2 = @name1 And Participant1 = @name2);";
                    command.Parameters.AddWithValue("@name1", participant2.Name);
                    command.Parameters.AddWithValue("@name2", participant1.Name);
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        public void resetDataBase()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var createSql = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "deleteTables.sql"));
                    using var command = connection.CreateCommand();
                    command.CommandText = createSql;
                    command.ExecuteNonQuery();
                    createSql = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "blueprint.sql"));
                    command.CommandText = createSql;
                    command.ExecuteNonQuery();
                }
            }
            finally { if (connection != null) { connection.Close(); } }
        }

        internal void changeRaRRound(int round, RaR3 rarToBeChanged, string name1, string name2, string name3)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"update RaR3 set Participant1 = @name1, Participant2 = @name2, Participant3 = @name3 where Participant1 = @oldName1 AND Participant2 = @oldName2 AND Participant3 = @oldName3 AND Round = @round;";
                    command.Parameters.AddWithValue("@name1", name1);
                    command.Parameters.AddWithValue("@name2", name2);
                    command.Parameters.AddWithValue("@name3", name3);
                    command.Parameters.AddWithValue("@oldName1", rarToBeChanged.OldParticipant.Name);
                    command.Parameters.AddWithValue("@oldName2", rarToBeChanged.NewParticipant.Name);
                    command.Parameters.AddWithValue("@oldName3", rarToBeChanged.EitherParticipant.Name);
                    command.Parameters.AddWithValue("@round", round);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        internal void changeRaRRound(int round, RaR2 rarToBeChanged, string name1, string name2)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"update RaR2 set Participant1 = @name1, Participant2 = @name2 where Participant1 = @oldName1 AND Participant2 = @oldName2 AND Round = @round;";
                    command.Parameters.AddWithValue("@name1", name1);
                    command.Parameters.AddWithValue("@name2", name2);
                    command.Parameters.AddWithValue("@oldName1", rarToBeChanged.OldParticipant.Name);
                    command.Parameters.AddWithValue("@oldName2", rarToBeChanged.NewParticipant.Name);
                    command.Parameters.AddWithValue("@round", round);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        internal void changeParticipant(Participant participant, string name, bool old, Dictionary<int, bool> availability)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"update Participant set ParticipantName = @name, Old = @old, Available1 = @a1, Available2 = @a2, Available3 = @a3, Available4 = @a4, Available5 = @a5 where ParticipantName = @oldname, Old = @oldold, Available1 = @olda1, Available2 = @olda2, Available3 = @olda3, Available4 = @olda4, Available5 = @olda5;";
                    command.Parameters.AddWithValue("@name", name);
                    int oldint = 0;
                    if (old) { oldint = 1; }
                    command.Parameters.AddWithValue("@old", oldint);

                    command.Parameters.AddWithValue("@oldname", participant.Name);
                    int oldoldint = 0;
                    if (participant.Old) { oldoldint = 1; }
                    command.Parameters.AddWithValue("@name", oldoldint);

                    int[] available = { 0, 0, 0, 0, 0 };
                    for (int i = 0; i < 5; i++)
                    {
                        if (availability[i + 1]) { available[i] = 1; }
                    }
                    command.Parameters.AddWithValue("@a1", available[0]);
                    command.Parameters.AddWithValue("@a2", available[1]);
                    command.Parameters.AddWithValue("@a3", available[2]);
                    command.Parameters.AddWithValue("@a4", available[3]);
                    command.Parameters.AddWithValue("@a5", available[4]);

                    int[] oldavailable = { 0, 0, 0, 0, 0 };
                    for (int i = 0; i < 5; i++)
                    {
                        if (participant.Availability[i + 1]) { oldavailable[i] = 1; }
                    }
                    command.Parameters.AddWithValue("@olda1", oldavailable[0]);
                    command.Parameters.AddWithValue("@olda2", oldavailable[1]);
                    command.Parameters.AddWithValue("@olda3", oldavailable[2]);
                    command.Parameters.AddWithValue("@olda4", oldavailable[3]);
                    command.Parameters.AddWithValue("@olda5", oldavailable[4]);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        internal void setRoundPlan(bool[] areRounds2er)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO RoundPlan (RoundPlanId, Rounds) VALUES (1, '@r1@r2@r2@r3@r4@r5') ON CONFLICT(ID) DO UPDATE SET Rounds = '@r1@r2@r2@r3@r4@r5';";
                    int[] roundplan = { 0, 0, 0, 0, 0 };
                    if (areRounds2er.Length != 5)
                    {
                        throw new Exception("wron array length on roundplan");
                    }
                    foreach (bool b in areRounds2er)
                    {
                        if (b) { roundplan[0] = 1; }
                    }
                    command.Parameters.AddWithValue("@r1", roundplan[0]);
                    command.Parameters.AddWithValue("@r2", roundplan[1]);
                    command.Parameters.AddWithValue("@r3", roundplan[2]);
                    command.Parameters.AddWithValue("@r4", roundplan[3]);
                    command.Parameters.AddWithValue("@r5", roundplan[4]);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }

        internal bool[] getRoundPlan()
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"Select Rounds from RoundPlan Where RoundPlanId = 1";
                    bool[] roundplan = new bool[5];
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rounds = reader.GetString(0);
                            for (int i = 0; i < 5; i++)
                            {
                                roundplan[i] = rounds.Substring(i, 1) == "1";
                            }
                        }
                    }
                    return roundplan;
                }
            }
            finally
            {
                if (connection != null) { connection.Close(); }
            }
        }
    }
}
