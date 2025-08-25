using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public interface IFRaRModel
    {
        void newParticipant(Participant participant);

        void deleteParticipant(Participant participant);

        void changeAvailability(Participant participant, int round);

        List<RaR2> getRaR2Round(int  round);

        List<RaR3> getRaR3Round(int round);

        void createNextRound(int round, bool is2round);

        void resetDatabase();

        List<Participant> getAllParticipants();

        void changeRaRRoundManually(int round, RaR2 rarToBeChanged, string name1, string name2);

        void changeRaRRoundManually(int round, RaR3 rarToBeChanged, string name1, string name2, string name3);

        void changeParticipant(Participant participant, string name, bool old, Dictionary<int, bool> availability);

    }
}
