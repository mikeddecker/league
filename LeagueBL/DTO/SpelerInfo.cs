using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.DTO {
    public class SpelerInfo {
        public SpelerInfo(int id, string naam, int? lengte, int? gewicht, int? rugnummer, string teamNaam) {
            this.Id = id;
            this.Naam = naam;
            this.Lengte = lengte;
            this.Gewicht = gewicht;
            this.Rugnummer = rugnummer;
            this.TeamNaam = teamNaam;
        }

        public int Id {get; set;}
        public string Naam { get; set; }
        public int? Lengte { get; set; }
        public int? Gewicht { get; set; }
        public int? Rugnummer { get; set; }
        public string TeamNaam { get; set; }
    }
}
