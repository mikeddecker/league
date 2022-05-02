using LeagueBL.Domein;
using LeagueBL.DTO;
using LeagueBL.Exceptions;
using LeagueBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Managers {
    public class SpelerManager {
        private ISpelerRepository Repo;

        public SpelerManager(ISpelerRepository repo) {
            this.Repo = repo;
        }

        public Speler RegistreerSpeler(string naam, int? lengte, int? gewicht) {
            try {
                Speler s = new Speler(naam, lengte, gewicht);
                if (!Repo.BestaatSpeler(s)) {
                    s = Repo.SchrijfSpelerInDB(s);
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
        public void UpdateSpeler(SpelerInfo spelerinfo) {
            if (spelerinfo == null) { throw new SpelerManagerException("Update speler - speler is null"); }
            try {
                if (Repo.BestaatSpeler(spelerinfo.Id)) {
                    //evt. TODO check eigenschappen van speler of er wel veranderingen zijn.
                    Speler speler = Repo.SelecteerSpeler(spelerinfo.Id);
                    bool changed = false;
                    if (speler.Naam != spelerinfo.Naam) { speler.ZetNaam(spelerinfo.Naam); changed = true; }
                    // Eerst HasValue vragen, stel dat er niets inzit, dan gaat die een foutmelding geven.
                    if (speler.Lengte.HasValue && speler.Lengte != spelerinfo.Lengte) { speler.ZetLengte((int)spelerinfo.Lengte); changed = true; }
                    if (speler.Gewicht.HasValue && speler.Gewicht != spelerinfo.Lengte) { speler.ZetGewicht((int)spelerinfo.Gewicht); changed = true; }
                    if (speler.Rugnummer.HasValue && speler.Rugnummer != spelerinfo.Rugnummer) { speler.ZetRugnummer((int)spelerinfo.Rugnummer); changed = true; }

                    if (!changed) { throw new SpelerManagerException("UpdateSpeler - geen veranderingen"); }
                    Repo.UpdateSpeler(speler);

                } else {
                    throw new SpelerManagerException("UpdateSpeler - speler niet gevonden");
                }
            } catch (SpelerManagerException) {
                throw;
            } catch (Exception ex) {
                throw new SpelerManagerException("UpdateSpeler", ex);
            }
        }
        public IReadOnlyList<SpelerInfo> SelecteerSpelers(int? id, string naam) {
            if (!id.HasValue && string.IsNullOrWhiteSpace(naam)) { throw new SpelerManagerException("SelecteerSpelers - geen input"); }
            try {
                return Repo.SelecteerSpelers(id, naam);
            } catch (Exception ex) {
                throw new SpelerManagerException("SelecteerSpelers", ex);
            }
        }
    }
}
