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
    public class TransferManager {
        private ITransferRepository transferRepo;
        private ISpelerRepository spelerRepo;
        private ITeamRepository teamRepo;
        public TransferManager(ITransferRepository transferRepo, ISpelerRepository spelerRepository, ITeamRepository teamRepository) {
            this.transferRepo = transferRepo;
            this.spelerRepo = spelerRepository;
            this.teamRepo = teamRepository;
        }
        public void RegistreerTransfer(SpelerInfo spelerInfo, TeamInfo nieuwTeamInfo, int prijs) {
            if (spelerInfo == null) { throw new TransferManagerException("RegistreerTransfer - speler is null"); }
            Transfer transfer = null;
            try {
                Speler speler = spelerRepo.SelecteerSpeler(spelerInfo.Id);
                //speler stopt
                if (nieuwTeamInfo == null) {
                    if (spelerInfo.TeamNaam == null) { throw new TransferManagerException("RegistreerTransfer - team is null"); }
                    transfer = new Transfer(speler, speler.Team); //speler + oud team
                    speler.VerwijderTeam();
                }
                // nieuwe speler
                else if (spelerInfo.TeamNaam == null) {
                    Team nieuwTeam = teamRepo.SelecteerTeam(nieuwTeamInfo.Stamnummer);
                    speler.ZetTeam(nieuwTeam);
                    transfer = new Transfer(speler, nieuwTeam, prijs);
                }
                //klassieke transfer
                else {
                    Team nieuwTeam = teamRepo.SelecteerTeam(nieuwTeamInfo.Stamnummer);
                    transfer = new Transfer(speler, nieuwTeam, speler.Team, prijs);
                    speler.ZetTeam(nieuwTeam);
                }

                //beginnende speler

                //Transfer transfer = new Transfer(speler, nieuwTeam, prijs);

                //speler.ZetTeam(nieuwTeam);

                transferRepo.SchrijfTransferInDB(transfer);
            } catch (Exception ex) {
                throw new TransferManagerException("RegistreerTransfer", ex);
            }
        }
    }
}
