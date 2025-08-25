using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    internal class Assignment
    {
        /// <summary>
        /// creates the next round (indicated by parameter), makes the assignments and writes them to the Database
        /// </summary>
        /// <param name="round">indicated round to be created</param>
        /// <exception cref="NotImplementedException"></exception>
        internal static void createNextRound(int round, bool is2Round)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// checks if a given round is still appropiate considering the availibilty of the involved participants
        /// </summary>
        /// <param name="round">identifies the round that should be checked</param>
        /// <returns>if the round is correctly assigned</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool checkIfRoundIsFine(int round)
        { 
            throw new NotImplementedException();
        }

        /// <summary>
        /// checks if the given participants can be matched in a Group or if they have already been matched in the past
        /// </summary>
        /// <param name="participant1">first participant</param>
        /// <param name="participant2">second participant</param>
        /// <returns>if the two participants have been matched together already</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool checkPairing(Participant participant1, Participant participant2)
        {
            throw new NotImplementedException();
        }
         /// <summary>
         /// checks a given RaRlist for Pairings that have already been paired. Used for reducing the doubling of pairings
         /// </summary>
         /// <param name="rar2Round">List of RaRs to be checked</param>
         /// <returns>Number of already paired participants that are paired again</returns>
         /// <exception cref="NotImplementedException"></exception>
        private static int checkRoundsForDoubles(List<RaR2> rar2Round)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// checks a given RaRlist for Pairings that have already been paired. Used for reducing the doubling of pairings
        /// </summary>
        /// <param name="rar3Round">List of RaRs to be checked</param>
        /// <returns>Number of already paired participants that are paired again</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static int checkRoundsForDoubles(List<RaR3> rar3Round)
        {
            throw new NotImplementedException();
        }

    }
}
