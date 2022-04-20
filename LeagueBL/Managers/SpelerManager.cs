using LeagueBL.Domein;
using LeagueBL.Exceptions;
using LeagueBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Managers {
    public class SpelerManager {
        private ISpelerRepository repo;

        public SpelerManager(ISpelerRepository repo) {
            this.repo = repo;
        }

        public Speler RegistreerSpeler(string naam, int? lengte, int? gewicht) {
            try {
                Speler s = new Speler(naam, lengte, gewicht);
                if (!repo.HeeftSpeler(s)) {
                    s = repo.SchrijfSpelerInDB(s);
                    return s;
                } else {
                    throw new SpelerManagerException("RegistreerSpeler - speler bestaat al");
                }
            } catch (SpelerManagerException e) {
                throw;
            } catch (Exception ex) {
                throw new SpelerManagerException("RegistreerSpeler", ex);
            }
        }
    }
}
