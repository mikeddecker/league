using LeagueBL.Domein;
using LeagueBL.Exceptions;
using LeagueBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Managers {
    public class TeamManager {
        private ITeamRepository repo;
        public TeamManager(ITeamRepository repo) {
            this.repo = repo;
        }
        public void RegistreerTeam(int stamnummer, string naam, string bijnaam) {
            try {
                Team t = new Team(stamnummer, naam);
                if (!string.IsNullOrWhiteSpace(bijnaam)) { t.ZetBijnNaam(bijnaam); }
                if (!repo.BestaatTeam(stamnummer)) {
                    repo.SchrijfTeamInDB(t);
                } else {
                    throw new TeamManagerException("TeamManager.RegistreerTeam() - Team bestaat al");
                }
            } catch (TeamManagerException e) {
                throw;
            } catch (Exception ex) {
                throw new TeamManagerException("RegistreerTeam", ex);
            }
        }
        public Team SelecteerTeam(int stamnummer) {
            try {
                if (repo.BestaatTeam(stamnummer)) {
                    return repo.SelecteerTeam(stamnummer);
                } else {
                    throw new TeamManagerException("");
                }
            } catch (Exception ex) {
                throw new TeamManagerException("SelecteerTeam", ex);
            }
        }
        public void UpdateTeam(Team team) {
            if (team == null) { throw new TeamManagerException("Update speler - speler is null"); }
            try {
                if (repo.BestaatTeam(team.Stamnummer)) {
                    //TODO check eigenschappen van speler of er wel veranderingen zijn.
                    repo.UpdateTeam(team);
                } else {
                    throw new TeamManagerException("UpdateSpeler - speler niet gevonden");
                }
            } catch (TeamManagerException) {
                throw;
            } catch (Exception ex) {
                throw new SpelerManagerException("UpdateTeam", ex);
            }
        }
    }
}
