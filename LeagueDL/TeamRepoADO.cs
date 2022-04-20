using LeagueBL.Domein;
using LeagueBL.Interfaces;
using LeagueDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueDL {
    public class TeamRepoADO : ITeamRepository {
        private string connectieString;

        public TeamRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public bool BestaatTeam(Team t) {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM dbo.Team WHERE stamnummer=@stamnummer";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@stamnummer", System.Data.SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@stamnummer"].Value = t.Stamnummer;

                    int n = (int)cmd.ExecuteScalar();
                    //(n > 0) ? true : false;
                    if (n > 0) { return true; } else { return false; }
                }
            } catch (Exception ex) {
                throw new TeamRepoADOException("BestaatTeam");
            }
            finally {
                conn.Close();
            }
        }

        public void SchrijfTeamInDB(Team t) {
            SqlConnection conn = GetConnection();
            string query = "INSERT INTO dbo.Team(stamnummer, naam, bijnaam) "
                + " VALUES(@stamnummer,@naam,@bijnaam)";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@stamnummer", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@bijnaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@naam"].Value = t.Naam;
                    if (t.Bijnaam == null) {
                        cmd.Parameters["@bijnaam"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@bijnaam"].Value = t.Bijnaam;
                    }
                    cmd.Parameters["@stamnummer"].Value = t.Stamnummer;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new TeamRepoADOException("SchrijfSpelerInDB");
            }
            finally {
                conn.Close();
            }
        }
    }
}
