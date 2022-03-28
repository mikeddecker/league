using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBL.Domein;

namespace LeagueBL {
    public class Transfer {
        public int Id { get; private set; }
        public Speler Speler { get; private set; }
        public double Kost { get; private set; }
        public Team OutTeam { get; private set; }
        public Team NewTeam { get; private set; }
    }
}
