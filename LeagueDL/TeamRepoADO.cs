using LeagueBL.Domein;
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
    public class TeamRepoADO : ITeamRepository {
        private string connectieString;

        public TeamRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }
        public bool BestaatTeam(int stamnummer) {
            SqlConnection conn = GetConnection();
            string query = "SELECT count(*) FROM dbo.Team WHERE stamnummer=@stamnummer";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@stamnummer", System.Data.SqlDbType.Int));
                    cmd.CommandText = query;
                    cmd.Parameters["@stamnummer"].Value = stamnummer;

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

        public Team SelecteerTeam(int stamnummer) {
            SqlConnection conn = GetConnection();
            string query = "SELECT t.stamnummer, t.naam AS ploegnaam, t.Bijnaam, s.* " +
                "FROM dbo.Team t " +
                "LEFT JOIN dbo.Speler s " +
                "ON t.stamnummer=s.TeamId " +
                "WHERE t.stamnummer=@stamnummer ";

            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@stamnummer", System.Data.SqlDbType.Int));
                    cmd.Parameters["@stamnummer"].Value = stamnummer;
                    cmd.CommandText = query;
                    Team team = null;
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        if (team == null) { // 1 malig doorlopen we dit om de teamgegevens in te vullen
                            string naam = (string)reader["ploegnaam"];
                            string bijnaam = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("bijnaam"))) bijnaam = (string)reader["bijnaam"];
                            team = new Team(stamnummer, naam);
                            if (bijnaam != null) { team.ZetBijnNaam(bijnaam); }
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("id"))) { // DB negeert upper en lower case
                            int? lengte = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("lengte"))) lengte = (int?)reader["lengte"];
                            int? gewicht = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("gewicht"))) gewicht = (int?)reader["gewicht"];
                            Speler speler = new Speler((int)reader["id"], (string)reader["naam"], lengte, gewicht);
                            speler.ZetTeam(team);
                            if (!reader.IsDBNull(reader.GetOrdinal("rugnummer"))) {
                                speler.ZetRugnummer((int)reader["rugnummer"]);
                            }
                        }
                    }
                    reader.Close();
                    return team;
                }
            } catch (Exception ex) {
                throw new TeamRepoADOException("BestaatTeam", ex);
            }
            finally {
                conn.Close();
            }
        }

        public void UpdateTeam(Team team) {
            SqlConnection conn = GetConnection();
            string query = "UPDATE team " +
                "SET naam=@naam, bijnaam=@bijnaam " +
                "WHERE stamnummer=@stamnummer";
            try {
                using (SqlCommand cmd = conn.CreateCommand()) {
                    conn.Open();
                    cmd.Parameters.Add(new SqlParameter("@stamnummer", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@naam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@bijnaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@stamnummer"].Value = team.Stamnummer;
                    cmd.Parameters["@naam"].Value = team.Naam;
                    if (team.Bijnaam == null) {
                        cmd.Parameters["@bijnaam"].Value = DBNull.Value;
                    } else {
                        cmd.Parameters["@bijnaam"].Value = team.Bijnaam;
                    }
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception ex) {
                throw new SpelerRepoADOException("UpdateTeam", ex);
            }
            finally {
                conn.Close();
            }
        }

    }
}
