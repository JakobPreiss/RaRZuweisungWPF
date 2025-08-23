using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public interface IFRaRModel
    {
        void newParticipant(string name, bool old);

        void deleteParticipant(Participant participant);

        void changeAvailability(Participant participant, int round);

        List<RaR2> getRaR2Round(int  round);

        List<RaR3> getRaR3Round(int round);

        void createNextRound(int round, int numberOfParticipants);

        void resetDatabase();


    }
}
