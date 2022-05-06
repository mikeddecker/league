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
    public class TeamManager {
        private ITeamRepository Repo;
        public TeamManager(ITeamRepository repo) {
            this.Repo = repo;
        }
        public void RegistreerTeam(int stamnummer, string naam, string bijnaam) {
            try {
                Team t = new Team(stamnummer, naam);
                if (!string.IsNullOrWhiteSpace(bijnaam)) { t.ZetBijnaam(bijnaam); }
                if (!Repo.BestaatTeam(stamnummer)) {
                    Repo.SchrijfTeamInDB(t);
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
                if (Repo.BestaatTeam(stamnummer)) {
                    return Repo.SelecteerTeam(stamnummer);
                } else {
                    throw new TeamManagerException("");
                }
            } catch (Exception ex) {
                throw new TeamManagerException("SelecteerTeam", ex);
            }
        }
        public IReadOnlyList<TeamInfo> SelecteerTeams() {
            return Repo.SelecteerTeams();
        }
        public void UpdateTeam(TeamInfo teaminfo) {
            if (teaminfo == null) { throw new TeamManagerException("Update speler - team is null"); }
            try {
                if (Repo.BestaatTeam(teaminfo.Stamnummer)) {
                    bool changed = false;
                    Team team = Repo.SelecteerTeam(teaminfo.Stamnummer);
                    if (teaminfo.Naam != team.Naam) { team.ZetNaam(teaminfo.Naam); changed = true; }
                    if (teaminfo.Bijnaam != team.Bijnaam) {
                        if (!string.IsNullOrWhiteSpace(teaminfo.Bijnaam)) {
                            team.ZetBijnaam(teaminfo.Bijnaam);
                            changed = true;
                        } else {
                            team.VerwijderBijnaam();
                            changed = true;
                        }
                    }
                    if (!changed) { throw new TeamManagerException("UpdateTeam - geen update"); }
                    Repo.UpdateTeam(team);
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
