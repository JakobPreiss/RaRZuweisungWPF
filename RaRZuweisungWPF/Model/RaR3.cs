using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public record RaR3
    {
        public Participant? oldParticipant;

        public Participant? newParticipant;

        public Participant? EitherParticipant;

        public int round;

        public RaR3(Participant? oldParticipant, Participant? newParticipant, Participant eitherparticipant, int round)
        {
            this.oldParticipant = oldParticipant;
            this.newParticipant = newParticipant;
            this.round = round;
            EitherParticipant = eitherparticipant;
        }
    }
}
