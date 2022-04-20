using LeagueBL.Domein;
using LeagueBL.Exceptions;
using LeagueBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Managers {
    public class TransferManager {
        private ITransferRepository repo;
        public TransferManager(ITransferRepository repo) {
            this.repo = repo;
        }
        public void RegistreerTransfer(Speler speler, Team nieuwTeam, int prijs) {
            if (speler == null) { throw new TransferManagerException("RegistreerTransfer - speler is null"); }
            Transfer transfer = null;
            try {
                //speler stopt
                if (nieuwTeam == null) {
                    if (speler.Team == null) { throw new TransferManagerException("RegistreerTransfer - team is null"); }
                    transfer = new Transfer(speler, speler.Team); //speler + oud team
                    speler.VerwijderTeam();
                } else if (speler.Team == null) {
                    // nieuwe speler
                    speler.ZetTeam(nieuwTeam);
                    transfer = new Transfer(speler, nieuwTeam, prijs);
                } else {
                    //klassieke transfer
                    transfer = new Transfer(speler, nieuwTeam, speler.Team, prijs);
                    speler.ZetTeam(nieuwTeam);
                }

                //beginnende speler

                //Transfer transfer = new Transfer(speler, nieuwTeam, prijs);

                //speler.ZetTeam(nieuwTeam);

                repo.SchrijfTransferInDB(transfer);
            } catch (Exception ex) {
                throw new TransferManagerException("RegistreerTransfer", ex);
            }
        }
    }
}
