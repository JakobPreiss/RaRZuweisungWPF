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

        }

        public static void deleteParticipant(Participant participant)
        {

        }

        public static void setAvailability(Participant participant, int round)
        {

        }

        public static List<Participant> readParticipants()
        {
            return null;
        }

        public static List<RaR2> readRaR2Round(int round)
        {
            return null;
        }

        public static List<RaR3> readRaR3Round(int round)
        {
            return null;
        }

        public static void writeRaR(int round, List<RaR2> rars)
        {

        }

        public static void writeRaR(int round, List<RaR3> rars)
        {

        }

        public static void deleteRaR(int round)
        {

        }

        public static List<Participant> readPairings(Participant participant)
        {
            return null;
        }

        public static void writePairing(Participant participant1, Participant participant2)
        {

        }

        public void deletePairing(Participant participant1, Participant participant2)
        {

        }

        public static void resetDataBase()
        {

        }


    }
}
