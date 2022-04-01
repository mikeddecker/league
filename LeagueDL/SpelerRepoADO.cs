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
    public class SpelerRepoADO : ISpelerRepository {
        private string connectieString;

        public SpelerRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public bool HeeftSpeler(Speler speler) {
            //TODO implement
            return false;
        }

        public Speler SchrijfSpelerInDB(Speler s) {
            SqlConnection conn = GetConnection();
            string query = "INSERT INTO dbo.Speler(naam,lengte,gewicht) "
                + "output INSERTED.ID VALUES(@naam,@lengte,@gewicht)";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@lengte", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@gewicht", System.Data.SqlDbType.Int));
                    cmd.Parameters["@naam"].Value = s.Naam;
                    if (s.Lengte==null) {
                        cmd.Parameters["@lengte"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@lengte"].Value = s.Lengte;
                    }
                    if (s.Gewicht==null) {
                        cmd.Parameters["@gewicht"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@gewicht"].Value = s.Gewicht;
                    }
                    cmd.CommandText = query;
                    int newID = (int)cmd.ExecuteScalar();
                    s.ZetId(newID);
                    return s;
                }
            } catch (Exception ex) {
                throw new SpelerRepoADOException("SchrijfSpelerInDB");
            } finally {
                conn.Close();
            }
        }
    }
}
