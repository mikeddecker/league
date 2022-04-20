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
                if (!repo.BestaatTeam(t)) {
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
    }
}
