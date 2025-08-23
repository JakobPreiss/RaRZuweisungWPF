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
    }
}
