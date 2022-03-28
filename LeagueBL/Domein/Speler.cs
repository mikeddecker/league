using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueBL.Exceptions;

namespace LeagueBL.Domein {
    public class Speler {
        public Speler(int id, string naam, int? lengte, int? gewicht) {
            ZetId(id);
            ZetNaam(naam);
            if (lengte != 0) { ZetLengte(lengte.Value); }
            if (gewicht != 0) { ZetGewicht(gewicht.Value); }
        }

        public int Id { get; private set; }
        public string Naam { get; private set; }
        public Team Team { get; private set; }
        public int? Lengte { get; private set; }
        public int? Rugnummer { get; private set; }
        public int? Gewicht { get; private set; }

        public void ZetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) { throw new SpelerException("ZetNaam"); }
            Naam = naam.Trim();
        }
        public void ZetLengte(int lengte) {
            if(lengte<150) {
                // throw new SpelerException("ZetLengte");
                SpelerException ex = new SpelerException("ZetLengte");
                ex.Data.Add("lengte", lengte);
                throw ex;
            }
            Lengte = lengte;
        }
        public void ZetId(int id) {
            if (id <= 0) { throw new SpelerException("ZetId"); }
            Id = id;
        }
        public void ZetGewicht(int gewicht) {
            if (gewicht < 50) { throw new SpelerException("ZetGewicht"); }
            Gewicht = gewicht;
        }
        public void ZetRugnummer(int rugnr) {
            if (rugnr <= 0 || rugnr > 99) { throw new SpelerException("ZetRugnummer"); }
            Rugnummer = rugnr;
        }
        public void VerwijderTeam() {
            Team = null;
        }
        public void ZetTeam(Team team) {
            if(team == null) { throw new SpelerException("ZetTeam"); }
            if(team == Team) { throw new SpelerException("ZetTeam"); }
            if(Team != null && Team.HeeftSpeler(this)) {
                Team.VerwijderSpeler(this);
            }
            if (!team.HeeftSpeler(this)) { team.VoegSpelerToe(this); }
            Team = team;
        }

        public override bool Equals(object obj) {
            return obj is Speler speler &&
                   Id == speler.Id;
        }
        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }
    }
}
