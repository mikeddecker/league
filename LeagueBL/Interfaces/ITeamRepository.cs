using LeagueBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Interfaces {
    public interface ITeamRepository {
        bool BestaatTeam(Team t);
        void SchrijfTeamInDB(Team t);
    }
}
