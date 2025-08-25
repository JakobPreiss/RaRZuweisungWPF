using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    internal class DataBaseAccess
    {
        public static void writeParticipant(Participant participant)
        {
            throw new NotImplementedException();
        }

        public static void deleteParticipant(Participant participant)
        {
            throw new NotImplementedException();
        }

        public static void setAvailability(Participant participant, int round)
        {
            throw new NotImplementedException();
        }

        public static bool isAvailable(Participant participant, int round)
        {
            throw new NotImplementedException();
        }

        public static List<Participant> readParticipants()
        {
            throw new NotImplementedException();
        }

        public static List<RaR2> readRaR2Round(int round)
        {
            throw new NotImplementedException();
        }

        public static List<RaR3> readRaR3Round(int round)
        {
            throw new NotImplementedException();
        }

        public static void writeRaR(int round, List<RaR2> rars)
        {
            throw new NotImplementedException();
        }

        public static void writeRaR(int round, List<RaR3> rars)
        {
            throw new NotImplementedException();
        }

        public static void deleteRaR(int round)
        {
            throw new NotImplementedException();
        }

        public static List<Participant> readPairings(Participant participant)
        {
            throw new NotImplementedException();
        }

        public static void writePairing(Participant participant1, Participant participant2)
        {
            throw new NotImplementedException();
        }

        public void deletePairing(Participant participant1, Participant participant2)
        {
            throw new NotImplementedException();
        }

        public static void resetDataBase()
        {
            throw new NotImplementedException();
        }

        internal static void changeRaRRound(int round, RaR3 rarToBeChanged, string name1, string name2, string name3)
        {
            throw new NotImplementedException();
        }

        internal static void changeRaRRound(int round, RaR2 rarToBeChanged, string name1, string name2)
        {
            throw new NotImplementedException();
        }
    }
}
