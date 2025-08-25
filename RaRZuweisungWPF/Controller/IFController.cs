using RaRZuweisungWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Controller
{
    public interface IFController
    {
        public List<Participant> GetParticipants();

        public bool AddParticipant(Participant participant);

        public bool RemoveParticipant(Participant participant);

        public bool changeParticipant(Participant participant, string name, bool old, Dictionary<int, bool> availability);

        public bool resetDatabase();

        public List<RaR2> getRaR2Round(int round);

        public List<RaR3> getRaR3Round(int round);

        public bool checkIfRoundIs2(int round);

        public bool createRound(int round, bool isRaR2Rund);

        
        //die beiden vielleicht al letztes implementieren (Optional, weil könnte stuff sehr kompliziert machen)
        public bool changePairing(int  round, Participant oldRoundParticipant, string name1, string name2, string name3);

        public bool changePairing(int round, Participant oldRoundParticipant, string name1, string name2);
    }
}
