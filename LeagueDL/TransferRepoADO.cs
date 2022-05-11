using LeagueBL;
using LeagueBL.Interfaces;
using LeagueDL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueDL {
    public class TransferRepoADO : ITransferRepository {
        private string connectieString;

        public TransferRepoADO(string connectionString) {
            this.connectieString = connectionString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public Transfer SchrijfTransferInDB(Transfer transfer) {
            SqlConnection conn = GetConnection();
            string querySpeler = "UPDATE speler SET teamid=@teamid WHERE id=@id";
            string queryTransfer = "INSERT INTO transfer(spelerid,prijs,oudteamid,nieuwteamid) " +
                " output INSERTED.ID VALUES(@spelerid,@prijs,@oudteamid,@nieuwteamid) ";

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            using (SqlCommand cmdSpeler = conn.CreateCommand())
            using (SqlCommand cmdTransfer = conn.CreateCommand()) {
                cmdSpeler.Transaction = tran;
                cmdTransfer.Transaction = tran;
                try {

                    //transfer
                    cmdTransfer.Parameters.Add(new SqlParameter("@spelerid", System.Data.SqlDbType.Int));
                    cmdTransfer.Parameters.Add(new SqlParameter("@prijs", System.Data.SqlDbType.Int));
                    cmdTransfer.Parameters.Add(new SqlParameter("@oudteamid", System.Data.SqlDbType.NVarChar));
                    cmdTransfer.Parameters.Add(new SqlParameter("@nieuwteamid", System.Data.SqlDbType.NVarChar));
                    cmdTransfer.CommandText = queryTransfer;
                    cmdTransfer.Parameters["@spelerid"].Value = transfer.Speler.Id;
                    cmdTransfer.Parameters["@prijs"].Value = transfer.Prijs;
                    if (transfer.OudTeam != null) {
                        cmdTransfer.Parameters["@oudteamid"].Value = transfer.OudTeam.Stamnummer;
                    } else {
                        cmdTransfer.Parameters["@oudteamid"].Value = DBNull.Value;
                    }
                    if (transfer.NieuwTeam != null) {
                        cmdTransfer.Parameters["@nieuwteamid"].Value = transfer.NieuwTeam.Stamnummer;
                    } else {
                        cmdTransfer.Parameters["@nieuwteamid"].Value = DBNull.Value;
                    }

                    
                    int transferId = (int)cmdTransfer.ExecuteScalar();
                    transfer.ZetId(transferId);

                    //speler
                    cmdSpeler.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                    cmdSpeler.Parameters.Add(new SqlParameter("@teamid", System.Data.SqlDbType.Int));
                    cmdSpeler.CommandText = querySpeler;
                    cmdSpeler.Parameters["@id"].Value = transfer.Speler.Id;
                    if (transfer.NieuwTeam != null) {
                        cmdSpeler.Parameters["@teamid"].Value = transfer.NieuwTeam.Stamnummer;
                    } else {
                        cmdSpeler.Parameters["@teamid"].Value = DBNull.Value;
                    }
                    cmdSpeler.ExecuteNonQuery();
                    tran.Commit();
                    return transfer;

                } catch (Exception ex) {
                    tran.Rollback();
                    throw new TransferRepoADOException("SchrijfTransferInDB", ex);
                }
                finally {
                    conn.Close();
                }
            }
        }
    }
}
