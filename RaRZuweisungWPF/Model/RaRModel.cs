using RaRZuweisungWPF.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public class RaRModel : IFRaRModel
    {
        private DataBaseAccess access;
        private Assignment assignment;
        private TheBigC controller;

        public RaRModel(TheBigC c)
        {
            access = new DataBaseAccess();
            assignment = new Assignment(access, this);
            controller = c;
        }
        public void changeAvailability(Participant participant, int round)
        {
            access.changeAvailability(participant, round);
        }

        public void createRounds()
        {
            assignment.createRounds();
        }

        public void deleteParticipant(Participant participant)
        {
            access.deleteParticipant(participant);
        }

        public List<RaR2> getRaR2Round(int round)
        {
            return access.readRaR2Round(round);
        }

        public List<RaR3> getRaR3Round(int round)
        {
            return access.readRaR3Round(round);
        }

        public void newParticipant(Participant participant)
        {
            access.writeParticipant(participant);
        }

        public void changeParticipant(Participant participant, string name, bool old, Dictionary<int, bool> availability)
        {
            access.changeParticipant(participant, name, old, availability);
        }

        public void resetDatabase()
        {
            access.resetDataBase();
        }

        public void changeRaRRoundManually(int round, RaR2 rarToBeChanged, string name1, string name2)
        {
            access.changeRaRRound(round, rarToBeChanged, name1, name2);
        }

        public void changeRaRRoundManually(int round, RaR3 rarToBeChanged, string name1, string name2, string name3)
        {
            access.changeRaRRound(round, rarToBeChanged, name1, name2, name3);
        }

        public List<Participant> getAllParticipants()
        {
            return access.readParticipants();
        }

        public void setRoundPlan(bool[] areRounds2er)
        {
            access.setRoundPlan(areRounds2er);
        }

        public void textPrint(string text)
        {
            controller.textShow(text);
        }
    }
}
