using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    /*
     * Other Idea:
        Create all rounds at once with the given availabilty and if the availability changes
        then change the affected Round and calculate the next rounds so that it has very litte doubling in the other rounds.
        ->more flexible
        --> probably better */

    /*
     * basic call stack:
     * createRound
     *      checkIfRoundsExist
     *          checkIfRoundIsFine
     *              checkRoundDoubling
     *                  if not doubling :)
     *                  if doubleing
     *                      delete Pairings from round
     *                      try for new assignment with max tries
     *                          if success :)
     *                              write next round and pairings on db
     *                          if not success
     *                              take the one with the least doubling (always safe the round with the least doubleing
     *                          check other Rounds -> iterating
     *          if not fine:
     *              delete Round 
     *      or if Round doesn't exist
     *      create 2-er or 3-er groups with set max tries for all rounds with least doubleing
     *          
     */
    internal class Assignment
    {
        private DataBaseAccess access;

        public Assignment(DataBaseAccess access)
        {
            this.access = access;
        }
        /// <summary>
        /// creates the next round (indicated by parameter), makes the assignments and writes them to the Database
        /// </summary>
        /// <param name="round">indicated round to be created</param>
        /// <exception cref="NotImplementedException"></exception>
        internal void createRounds()
        {
            throw new NotImplementedException();
        }

        internal bool createRound(int round, bool is2erRound)
        {
            int maxTries = 1000;
            int tries = 0;
            Dictionary<Participant, Participant> pairs = access.readPairings();
            List<Participant> participants = access.readParticipants();
            List<List<RaR>> raRs = new List<List<RaR>>();
            for (int i = 1; i < round; i++)
            {
                List<RaR2> rar2= access.readRaR2Round(round);
                if (rar2.Count > 0)
                {
                    raRs.Add(rar2.Cast<RaR>().ToList());
                } else
                {
                    raRs.Add(access.readRaR3Round(round).Cast<RaR>().ToList());
                }
            }



        }

        internal bool participantHasThisRound(Participant participant, int round, List<List<RaR>> rars)
        {
            bool hasRound = false;
            foreach (RaR rar in rars[round])
            {
                if(rar.GetParticipants().Contains(participant)) {  hasRound = true; break; }
            }
            return hasRound;
        }
        internal bool checkIfRoundExists(int round)
        {
            List<RaR2> raR2s = access.readRaR2Round(round);
            List<RaR3> raR3s = access.readRaR3Round(round);
            return raR2s.Count > 0 || raR3s.Count > 0;
        }

        /// <summary>
        /// checks if a given round is still appropiate considering the availibilty of the involved participants (so if there is an old and a new Participant in every group and if there are no 1-er groups)
        /// </summary>
        /// <param name="round">identifies the round that should be checked</param>
        /// <returns>if the round is correctly assigned</returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool checkIfRoundIsFine(int round)
        {
            List<RaR2> rar2round = access.readRaR2Round(round);
            if (rar2round.Count > 0)
            {
                foreach(RaR2 r in rar2round)
                {
                    if (!r.OldParticipant.Availability[round]) { return false; }
                    if (!r.NewParticipant.Availability[round]) { return false; }
                }
            }
            else
            {
                List<RaR3> rar3round = access.readRaR3Round(round);
                foreach (RaR3 r in rar3round)
                {
                    if (!r.OldParticipant.Availability[round])
                    {
                        if(!(r.EitherParticipant != null && r.EitherParticipant.Old && r.EitherParticipant.Availability[round])) { return false; }
                    }
                    if (!r.NewParticipant.Availability[round])
                    { 
                        if(!(r.EitherParticipant != null && !r.EitherParticipant.Old && r.EitherParticipant.Availability[round])) { return false; }
                    }
                    if (!r.NewParticipant.Availability[round] && !r.OldParticipant.Availability[round]) { return false;}
                }
            }
            return true;
        }

        /// <summary>
        /// checks if the given participants can be matched in a Group or if they have already been matched in the past
        /// </summary>
        /// <param name="participant1">first participant</param>
        /// <param name="participant2">second participant</param>
        /// <returns>if the two participants have been matched together already</returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool checkPairing(Participant participant1, Participant participant2, Dictionary<Participant, Participant> pairs)
        {
            bool arePaired = false;
            if (pairs.ContainsKey(participant1))
            {
                arePaired = pairs[participant1].Equals(participant2);
                return arePaired;
            }
            if (pairs.ContainsKey(participant2))
            {
                arePaired = pairs[participant2].Equals(participant1);
                return arePaired;
            }
            return arePaired;
        }
         /// <summary>
         /// checks a given RaRlist for Pairings that have already been paired. Used for reducing the doubling of pairings
         /// </summary>
         /// <param name="rar2Round">List of RaRs to be checked</param>
         /// <returns>Number of already paired participants that are paired again</returns>
         /// <exception cref="NotImplementedException"></exception>
        private int checkRoundsForDoubles(List<RaR2> rar2Round)
        {
            int doubles = 0;
            Dictionary<Participant, Participant> pairs = access.readPairings();
            foreach (RaR2 r in rar2Round)
            {
                if(checkPairing(r.OldParticipant, r.NewParticipant, pairs)) { doubles++; }
            }
            return doubles;
        }

        /// <summary>
        /// checks a given RaRlist for Pairings that have already been paired. Used for reducing the doubling of pairings
        /// </summary>
        /// <param name="rar3Round">List of RaRs to be checked</param>
        /// <returns>Number of already paired participants that are paired again</returns>
        /// <exception cref="NotImplementedException"></exception>
        private int checkRoundsForDoubles(List<RaR3> rar3Round)
        {
            int doubles = 0;
            Dictionary<Participant, Participant> pairs = access.readPairings();
            foreach (RaR3 r in rar3Round)
            {
                if(checkPairing(r.OldParticipant, r.NewParticipant, pairs)) {doubles ++;}
                if(r.EitherParticipant != null)
                {
                    if (checkPairing(r.OldParticipant, r.EitherParticipant, pairs)) { doubles++; }
                    if (checkPairing(r.NewParticipant, r.EitherParticipant, pairs)) { doubles++; }
                }
            }
            return doubles;
        }

    }
}
