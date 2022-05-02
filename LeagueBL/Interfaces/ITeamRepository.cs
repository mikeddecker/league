using LeagueBL.Domein;
using LeagueBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Interfaces {
    public interface ITeamRepository {
        bool BestaatTeam(int stamnummer);
        void SchrijfTeamInDB(Team t);
        Team SelecteerTeam(int stamnummer);
        void UpdateTeam(Team team);
        IReadOnlyList<TeamInfo> SelecteerTeams();
    }
}
