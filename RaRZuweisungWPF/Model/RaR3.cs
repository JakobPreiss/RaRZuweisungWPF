using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public record RaR3 : IFRaR
    {
        public Participant OldParticipant { get; set; }

        public Participant NewParticipant { get; set; }

        public Participant? EitherParticipant { get; set; }

        public int Round {  get; set; }

        public RaR3(Participant oldParticipant, Participant newParticipant, Participant? eitherparticipant, int round)
        {
            OldParticipant = oldParticipant;
            NewParticipant = newParticipant;
            Round = round;
            EitherParticipant = eitherparticipant;
        }

        public List<Participant> GetParticipants()
        {
            if (EitherParticipant == null)
            {
                return new List<Participant> { OldParticipant, NewParticipant };
            }
            else
            {
                return new List<Participant> { OldParticipant, NewParticipant, EitherParticipant };
            }
        }
    }
}
