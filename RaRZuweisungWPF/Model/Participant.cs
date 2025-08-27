using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaRZuweisungWPF.Model
{
    public class Participant
    {
        public bool Old { get; set; }

        public string Name { get; set; }

        public Dictionary<int, bool> Availability { get; set; }

        public Participant(string name, bool old, bool available1, bool available2, bool available3, bool available4, bool available5)
        { 
            Name = name;
            Availability = new Dictionary<int, bool>();
            Old = old;
            Availability.Add(1, available1);
            Availability.Add(2, available2);
            Availability.Add(3, available3);
            Availability.Add(4, available4);
            Availability.Add(5, available5);
        }

        public override bool Equals(object? obj)
        {
            if(obj is Participant p)
            {
                Participant participant = (Participant)obj;
                return participant.Name.Equals(this.Name);
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
