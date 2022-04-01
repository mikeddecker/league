using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBL.Domein;
using LeagueBL.Exceptions;

namespace LeagueBL {
    public class Transfer {
        public Transfer(int id, Speler speler, int prijs, Team oudTeam, Team nieuwTeam) {
            ZetId(id);
            ZetSpeler(speler);
            ZetPrijs(prijs);
            ZetOudTeam(oudTeam);
            ZetNieuwTeam(nieuwTeam);
        }

        // speler is nieuw
        public Transfer(int id, Speler speler, int prijs, Team nieuwTeam) {
            ZetId(id);
            ZetSpeler(speler);
            ZetPrijs(prijs);
            ZetNieuwTeam(nieuwTeam);
        }

        //speler stopt
        public Transfer(int id, Speler speler, Team oudTeam) {
            ZetId(id);
            ZetSpeler(speler);
            ZetPrijs(0);
            ZetOudTeam(oudTeam);
        }

        public int Id { get; private set; }
        public Speler Speler { get; private set; }
        public int Prijs { get; private set; }
        public Team OudTeam { get; private set; }
        public Team NieuwTeam { get; private set; }
        public void ZetId(int id) {
            if (id <= 0) { throw new TransferException("ZetId"); }
            Id = id;
        }
        public void ZetPrijs(int prijs) {
            if (prijs < 0) { throw new TransferException("ZetPrijs"); }
            Prijs = prijs;
        }
        public void VerwijderOudTeam(Team team) {
            if (NieuwTeam is null) { throw new TransferException("VerwijderOudTeam"); }
            OudTeam = null;
        }
        public void ZetOudTeam(Team team) {
            //if(team == null) { throw new TransferException("ZetOudTeam"); }
            if (team == NieuwTeam) { throw new TransferException("ZetOudTeam"); }
            OudTeam = team;
        }
        public void VerwijderNieuwTeam(Team team) {
            if (OudTeam is null) { throw new TransferException("ZetNieuwTeam"); }
            NieuwTeam = null;
        }
        public void ZetNieuwTeam(Team team) {
            if (team == OudTeam) { throw new TransferException("ZetNieuwTeam"); }
            NieuwTeam = team;
        }
        public void ZetSpeler(Speler speler) {
            if (speler is null) { throw new TransferException("ZetSpeler"); }
            Speler = speler;
        }
        
    }
}
