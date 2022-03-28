using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Domein {
    public class SpelerBeheerder {
        //public ISpelerRepo? SpelerRepo { get; private set; }
        //public ITranserRepo? TransferRepo { get; private set; }
        public void RegistreerSpeler(int id, string naam, int? lengte, int? gewicht, int? rugnr) { }
        public void VerwijderSpeler(Speler speler) { }
        public void UpdateSpeler(Speler speler) { 
        // of met parameters van nieuwe speler
        }
        public void SelecteerTeam(int id) { }
        public void MaakTransfer(Speler speler, Team nieuwTeam, double Kost) { }
    }
}
