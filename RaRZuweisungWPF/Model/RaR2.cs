using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public record RaR2
    {
        public Participant? oldParticipant;

        public Participant? newParticipant;

        public int round;

        public RaR2(Participant? oldParticipant, Participant? newParticipant, int round)
        {
            this.oldParticipant = oldParticipant;
            this.newParticipant = newParticipant;
            this.round = round;
        }
    }
}
