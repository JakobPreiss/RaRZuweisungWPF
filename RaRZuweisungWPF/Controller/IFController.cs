using RaRZuweisungWPF.aView;
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

        public void AddParticipant(Participant participant);

        public void RemoveParticipant(Participant participant);

        public void changeParticipant(Participant participant, string name, bool old, Dictionary<int, bool> availability);

        public void resetDatabase();

        public List<RaR2> getRaR2Round(int round);

        public List<RaR3> getRaR3Round(int round);

        public bool checkIfRoundIs2(int round);

        public void createRounds(int round, bool isRaR2Rund);
        void Notify();

        public void setRoundPlan(bool[] areRounds2er);


        //die beiden vielleicht al letztes implementieren (Optional, weil könnte stuff sehr kompliziert machen)
        public bool changePairing(int  round, Participant oldRoundParticipant, string name1, string name2, string name3);

        public bool changePairing(int round, Participant oldRoundParticipant, string name1, string name2);
    }
}
