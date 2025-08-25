using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public record RaR2
    {
        public Participant? OldParticipant { get; set; }

        public Participant? NewParticipant {  get; set; }

        public int Round { get; set; }

        public RaR2(Participant? oldParticipant, Participant? newParticipant, int round)
        { 
            OldParticipant = oldParticipant;
            NewParticipant = newParticipant;
            Round = round;
        }
    }
}
