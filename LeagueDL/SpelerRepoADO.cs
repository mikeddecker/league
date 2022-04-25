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

        public bool BestaatSpeler(Speler speler) {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM dbo.Speler WHERE naam=@naam";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                    cmd.CommandText = query;
                    cmd.Parameters["@naam"].Value = speler.Naam;

                    int n = (int)cmd.ExecuteScalar();
                    //(n > 0) ? true : false;
                    if (n > 0) { return true; } else { return false; }
                }
            } catch (Exception ex) {
                throw new SpelerRepoADOException("BestaatSpeler", ex);
            }
            finally {
                conn.Close();
            }
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
                    if (s.Lengte == null) {
                        cmd.Parameters["@lengte"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@lengte"].Value = s.Lengte;
                    }
                    if (s.Gewicht == null) {
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
                throw new SpelerRepoADOException("SchrijfSpelerInDB", ex);
            }
            finally {
                conn.Close();
            }
        }
        public void UpdateSpeler(Speler speler) {
            SqlConnection conn = GetConnection();
            string query = "UPDATE speler " +
                "SET naam=@naam, lengte=@lengte, gewicht=@gewicht, rugnummer=@rugnummer " +
                "WHERE id=@id";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@lengte", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@gewicht", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@rugnummer", System.Data.SqlDbType.Int));
                    cmd.Parameters["@id"].Value = speler.Id;
                    cmd.Parameters["@naam"].Value = speler.Naam;
                    if (speler.Lengte == null) {
                        cmd.Parameters["@lengte"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@lengte"].Value = speler.Lengte;
                    }
                    if (speler.Gewicht == null) {
                        cmd.Parameters["@gewicht"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@gewicht"].Value = speler.Gewicht;
                    }
                    if (speler.Rugnummer == null) {
                        cmd.Parameters["@rugnummer"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@rugnummer"].Value = speler.Rugnummer;
                    }

                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new SpelerRepoADOException("SchrijfSpelerInDB", ex);
            }
            finally {
                conn.Close();
            }
        }

    }
}
