using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public class RaRModel : IFRaRModel
    {
        public void changeAvailability(Participant participant, int round)
        {
            DataBaseAccess.setAvailability(participant, round);
            throw new NotImplementedException();
        }

        public void createNextRound(int round, int numberOfParticipants)
        {
            Assignment.createNextRound(round, numberOfParticipants);
            throw new NotImplementedException();
        }

        public void deleteParticipant(Participant participant)
        {
            DataBaseAccess.deleteParticipant(participant);
            throw new NotImplementedException();
        }

        public List<RaR2> getRaR2Round(int round)
        {
            DataBaseAccess.readRaR2Round(round);
            throw new NotImplementedException();
        }

        public List<RaR3> getRaR3Round(int round)
        {
            DataBaseAccess.readRaR3Round(round);
            throw new NotImplementedException();
        }

        public void newParticipant(string name, bool old, bool available1, bool available2, bool available3, bool available4, bool available5)
        {
            DataBaseAccess.writeParticipant();
            throw new NotImplementedException();
        }

        public void resetDatabase()
        {
            DataBaseAccess.resetDataBase();
            throw new NotImplementedException();
        }
    }
}
