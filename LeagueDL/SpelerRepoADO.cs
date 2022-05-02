using LeagueBL.Domein;
using LeagueBL.DTO;
using LeagueBL.Interfaces;
using LeagueDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
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
        public bool BestaatSpeler(int spelerId) {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM dbo.Speler WHERE id=@id";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@int", System.Data.SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@id"].Value = spelerId;

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
        public IReadOnlyList<SpelerInfo> SelecteerSpelers(int? id, string naam) {
            if ((!id.HasValue) && string.IsNullOrWhiteSpace(naam) == true) { throw new SpelerRepoADOException("SelecteerSpelers - geen input"); }
            string query = "SELECT s.*,	" +
                                "CASE " +
                                    "WHEN t.naam IS NULL THEN NULL " +
                                    "ELSE CONCAT(t.naam, '-', t.Stamnummer, ' (' + t.Bijnaam + ')') " +
                                "END Teamnaam " +
                            "FROM Speler s " +
                            "LEFT JOIN Team t ON s.TeamId = t.Stamnummer ";
            if (id.HasValue) { query += " WHERE s.Id=@id;"; } else if (!string.IsNullOrWhiteSpace(naam)) { query += "WHERE s.Naam =@naam;"; }

            List<SpelerInfo> spelers = new List<SpelerInfo>();
            SqlConnection connection = GetConnection();

            using (SqlCommand command = connection.CreateCommand()) {
                command.CommandText = query;
                connection.Open();
                try {
                    if (id.HasValue) {
                        command.Parameters.AddWithValue("@id", id);
                    } else {
                        command.Parameters.AddWithValue("@naam", naam);
                    }
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        int spelerId = (int)reader["id"];
                        string spelerNaam = (string)reader["naam"];
                        int? lengte = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("lengte"))) { lengte = (int)reader["lengte"]; }
                        int? gewicht = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("gewicht"))) { gewicht = (int)reader["gewicht"]; }
                        int? rugnummer = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("rugnummer"))) { rugnummer = (int)reader["rugnummer"]; }
                        string teamnaam = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("teamnaam"))) { teamnaam = (string)reader["teamnaam"]; }
                        SpelerInfo spelerInfo = new SpelerInfo(spelerId, spelerNaam, lengte, gewicht, rugnummer, teamnaam);
                        spelers.Add(spelerInfo);
                    }
                    return spelers.AsReadOnly();
                } catch (Exception ex) {
                    throw new SpelerRepoADOException("SelecteerSpelers", ex);
                }
                finally {
                    connection.Close();
                }
            }
        }

        public Speler SelecteerSpeler(int id) {
            string query = "SELECT s.id spelerid, s.naam spelernaam, s.rugnummer spelerrugnummer, s.lengte spelerlengte, s.gewicht spelergewicht, stamnummer, t.naam teamnaam, bijnaam, ss.* " +
                "FROM speler s " +
                "LEFT JOIN team t ON s.TeamId= t.Stamnummer " +
                "LEFT JOIN speler ss ON t.Stamnummer = ss.TeamId " +
                "WHERE s.id = @id";

            SqlConnection connection = GetConnection();
            try {
                Speler speler = null;
                Team team = null;
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    bool heeftTeam = false;
                    while (reader.Read()) {
                        if (speler == null) {
                            int? lengte = null;
                            int? gewicht = null;
                            int? rugnummer = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("spelerlengte"))) { lengte = (int?)reader["spelerlengte"]; }
                            if (!reader.IsDBNull(reader.GetOrdinal("spelergewicht"))) { gewicht = (int?)reader["spelergewicht"]; }
                            if (!reader.IsDBNull(reader.GetOrdinal("spelerrugnummer"))) { rugnummer = (int?)reader["spelerrugnummer"]; }
                            speler = new Speler(id, (string)reader["spelernaam"], lengte, gewicht);
                            if (rugnummer.HasValue) { speler.ZetRugnummer((int)rugnummer); }
                            heeftTeam = !reader.IsDBNull(reader.GetOrdinal("stamnummer"));
                        }
                        if (heeftTeam) {
                            if (team == null) {
                                int stamnummer = (int)reader["stamnummer"];
                                string teamnaam = (string)reader["teamnaam"];
                                string bijnaam = null;
                                if (!reader.IsDBNull(reader.GetOrdinal("bijnaam"))) { bijnaam = (string)reader["bijnaam"]; }
                                team = new Team(stamnummer, teamnaam);
                                if (bijnaam != null) { team.ZetBijnNaam(bijnaam); }
                                speler.ZetTeam(team);
                            }

                            int? lengte = null;
                            int? gewicht = null;
                            int? rugnummer = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("lengte"))) { lengte = (int?)reader["lengte"]; }
                            if (!reader.IsDBNull(reader.GetOrdinal("gewicht"))) { gewicht = (int?)reader["gewicht"]; }
                            if (!reader.IsDBNull(reader.GetOrdinal("rugnummer"))) { rugnummer = (int?)reader["rugnummer"]; }
                            Speler teamSpeler = new Speler((int)reader["id"], (string)reader["naam"], lengte, gewicht);
                            if (rugnummer.HasValue) { teamSpeler.ZetRugnummer((int)rugnummer); }
                            heeftTeam = !reader.IsDBNull(reader.GetOrdinal("stamnummer"));
                            teamSpeler.ZetTeam(team);
                        }
                    }
                    reader.Close();

                    return speler;
                }
            } catch (Exception ex) {
                throw new SpelerRepoADOException("SelecteerSpeler", ex);
            }
            finally {

            }
        }
    }
}
