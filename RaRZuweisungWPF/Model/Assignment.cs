using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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
            /*List<List<RaR>> pastRaRs = new List<List<RaR>>();
            int rounds = 1;
            while(rounds < 6)
            {
                List<RaR2> rar2 = access.readRaR2Round(i);
                if (rar2.Count > 0)
                {
                    pastRaRs.Add(rar2.Cast<RaR>().ToList());
                }
                else
                {
                    List<RaR3> rar3 = access.readRaR3Round(i);
                    if(rar3.Count > 0)
                    {
                        pastRaRs.Add(rar3.Cast<RaR>().ToList());
                    }
                    else
                    {
                        break;
                    }
                }
                rounds++;
            }*/
            int maxOuterTries = 200;
            int outerTries = 0;
            int maxtries = 200;
            int tries = 0;
            bool roundCreationSuccess = false;
            int round = 1;
            int rounds = 5;
            bool [] raR2Rounds = access.getRoundPlan();
            bool allRoundsSuccess = false;
            while(!allRoundsSuccess && outerTries < maxOuterTries)
            {
                while (tries++ < maxtries && round <= rounds)
                {
                    roundCreationSuccess = createRound(round, raR2Rounds[round - 1], 0);
                    if (roundCreationSuccess)
                    {
                        round++;
                        roundCreationSuccess = false;
                    }
                }
                if (round < rounds + 1)
                {
                    access.resetRaRsAndPairs();
                    roundCreationSuccess = false;
                    round = 1;
                    tries = 0;
                    outerTries++;
                } else
                {
                    allRoundsSuccess = true;
                }
            }
            if(!allRoundsSuccess)
            {
                throw new Exception("Rundenerstellung ist fehlgeschlagen");
            }
        }
        
        //maxtries notwendig? -> verlagern oder wiederholung/reset dadurch einbauen?
        internal bool createRound(int round, bool is2erRound, int howManyDoubleingAllowed)
        {
            int maxTries = 200;
            int tries = 0;
            Dictionary<string, List<Participant>> pairs = access.readPairings();
            List<Participant> participants = access.readParticipants();
            int numberOfParticipants = participants.Count;
            bool doubleingAllowed = !(howManyDoubleingAllowed == 0);
            if (is2erRound)
            {
                List<RaR2> raR2s = new List<RaR2>();
                int newParticipants = 0;
                foreach (Participant p in participants) { if (!p.Old) { newParticipants++; } }
                while (raR2s.Count < newParticipants && tries < maxTries)
                {
                    RaR2 rar2 = createRaR2(pairs, raR2s, participants, round, doubleingAllowed);
                    if (rar2 == null) { return false; }
                    pairs[rar2.OldParticipant.Name].Add(rar2.NewParticipant);
                    pairs[rar2.NewParticipant.Name].Add(rar2.OldParticipant);
                    participants.Remove(rar2.NewParticipant);
                    participants.Remove(rar2.OldParticipant);
                    raR2s.Add(rar2);
                    tries++;
                }
                if (participants.Count > 0)
                {
                    foreach(Participant p in participants)
                    {
                        if(!participants[0].Old) { return false; }
                    }
                }
                bool successful = raR2s.Count== newParticipants;
                if (successful) {
                    if (howManyDoubleingAllowed >= checkRoundsForDoubles(raR2s)) {
                        access.writeRaR(round, raR2s);
                        foreach (RaR2 r in raR2s)
                        {
                            access.writePairing(r.OldParticipant, r.NewParticipant);
                        }
                        return true;
                    } else { return false; }
                } else {  return false; }
                    
            } else
            {
                List<RaR3> raR3s = new List<RaR3>();
                int numberOfRaR3s;
                if (numberOfParticipants % 3 == 2) { numberOfRaR3s = numberOfParticipants / 3 + 1; }
                else { numberOfRaR3s = numberOfParticipants / 3;  }
                while(raR3s.Count < numberOfRaR3s && participants.Count > 1 && tries < maxTries)
                {
                    RaR3 rar3 = createRaR3(pairs, raR3s, participants, round, doubleingAllowed);
                    if(rar3 == null) { return false; }
                    participants.Remove(rar3.NewParticipant);
                    participants.Remove(rar3.OldParticipant);
                    if (rar3.EitherParticipant != null)
                    {
                        participants.Remove(rar3.EitherParticipant);
                        pairs[rar3.NewParticipant.Name].Add(rar3.EitherParticipant);
                        pairs[rar3.OldParticipant.Name].Add(rar3.EitherParticipant);
                        pairs[rar3.EitherParticipant.Name].Add(rar3.NewParticipant);
                        pairs[rar3.EitherParticipant.Name].Add(rar3.OldParticipant);
                    }
                    pairs[rar3.OldParticipant.Name].Add(rar3.NewParticipant);
                    pairs[rar3.NewParticipant.Name].Add(rar3.OldParticipant);
                    raR3s.Add(rar3);

                }
                if (participants.Count == 1)
                {
                    if (!participants[0].Old) { return false; }
                }
                if(participants.Count > 1)
                {
                    return false;
                }
                bool successful = raR3s.Count == numberOfRaR3s;
                if (successful)
                {
                    if (howManyDoubleingAllowed >= checkRoundsForDoubles(raR3s)) {
                        access.writeRaR(round, raR3s);
                        foreach (RaR3 r in raR3s)
                        {
                            if(r.EitherParticipant != null)
                            {
                                access.writePairing(r.EitherParticipant, r.NewParticipant);
                                access.writePairing(r.EitherParticipant, r.OldParticipant); 
                            }
                            access.writePairing(r.NewParticipant, r.OldParticipant);
                        }
                        return true;
                    }
                    else { return false; }
                }
                else { return false; }
            }


        }

        internal RaR2 createRaR2(Dictionary<string, List<Participant>> pairs, List<RaR2> thisRound, List<Participant> participants, int round, bool doublingAllowed)
        {
            RaR2? rar2 = null;
            int tries = 0;
            int maxtries = 200;
            while (rar2 == null && tries < maxtries)
            {
                if(participants.Count == 1) { return null; }
                Random random = new Random();
                Participant old = participants[random.Next(participants.Count)];
                if(!old.Old || !old.Availability[round]) { tries++;  continue; }
                Participant newbie = participants[random.Next(participants.Count)];
                if(newbie.Old || !newbie.Availability[round]) { tries++;  continue; }
                if(participantHasThisRound(newbie, round, thisRound.Cast<IFRaR>().ToList()) || participantHasThisRound(old, round, thisRound.Cast<IFRaR>().ToList())) { tries++; continue; }
                if(!doublingAllowed && checkPairing(old, newbie, pairs)) { tries++; continue; }
                tries++;
                rar2 = new RaR2(old, newbie, round);
            }
            return rar2;
        }

        internal RaR3 createRaR3(Dictionary<string, List<Participant>> pairs, List<RaR3> thisRound, List<Participant> participants, int round, bool doublingAllowed)
        {
            RaR3? rar3 = null;
            int tries = 0;
            int maxtries = 200;
            while (rar3 == null && tries < maxtries)
            {
                Random random = new Random();
                Participant old = participants[random.Next(participants.Count)];
                if (!old.Old || !old.Availability[round]) { tries++; continue; }
                Participant newbie = participants[random.Next(participants.Count)];
                if (newbie.Old || !newbie.Availability[round]) { tries++; continue; }
                if (participants.Count == 2)
                {
                    if (participantHasThisRound(newbie, round, thisRound.Cast<IFRaR>().ToList()) || participantHasThisRound(old, round, thisRound.Cast<IFRaR>().ToList())) { tries++; continue; }
                    if (!doublingAllowed && checkPairing(old, newbie, pairs)) { tries++; continue; }
                    tries++;
                    rar3 = new RaR3(old, newbie, null, round);
                } else
                {
                    Participant either = participants[random.Next(participants.Count)];
                    if (!either.Availability[round] || either.Equals(old) || either.Equals(newbie)) { tries++; continue; }
                    if (participantHasThisRound(newbie, round, thisRound.Cast<IFRaR>().ToList()) ||
                        participantHasThisRound(old, round, thisRound.Cast<IFRaR>().ToList()) ||
                        participantHasThisRound(either, round, thisRound.Cast<IFRaR>().ToList())) { tries++; continue; }
                    if (!doublingAllowed && (checkPairing(old, newbie, pairs) || checkPairing(old, either, pairs) || checkPairing(newbie, either, pairs))) { tries++; continue; }
                    tries++;
                    rar3 = new RaR3(old, newbie, either, round);
                }
            }
            return rar3;
        }

        internal bool participantHasThisRound(Participant participant, int round, List<IFRaR> rars)
        {
            bool hasRound = false;
            foreach (IFRaR rar in rars)
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
        private bool checkPairing(Participant participant1, Participant participant2, Dictionary<string, List<Participant>> pairs)
        {
            bool arePaired = false;
            if (pairs.ContainsKey(participant1.Name))
            {
                arePaired = pairs[participant1.Name].Contains(participant2);
                return arePaired;
            }
            if (pairs.ContainsKey(participant2.Name))
            {
                arePaired = pairs[participant2.Name].Contains(participant1);
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
            Dictionary<string, List<Participant>> pairs = access.readPairings();
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
            Dictionary<string, List<Participant>> pairs = access.readPairings();
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
