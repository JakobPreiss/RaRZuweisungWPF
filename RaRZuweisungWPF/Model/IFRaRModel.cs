using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public interface IFRaRModel
    {
        void newParticipant(string name, bool old, bool available1, bool available2, bool available3, bool available4, bool available5);

        void deleteParticipant(Participant participant);

        void changeAvailability(Participant participant, int round);

        List<RaR2> getRaR2Round(int  round);

        List<RaR3> getRaR3Round(int round);

        void createNextRound(int round);

        void resetDatabase();

        void changeRaRRoundManually(int round, RaR2 rarToBeChanged, string name1, string name2);

        void changeRaRRoundManually(int round, RaR3 rarToBeChanged, string name1, string name2, string name3);

    }
}
