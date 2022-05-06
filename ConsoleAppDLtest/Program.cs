using LeagueDL;
using System;
using LeagueBL.Managers;
using LeagueBL.Domein;
using LeagueBL.Interfaces;
using LeagueBL.DTO;

namespace ConsoleAppDLtest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            string connString = @"Data Source=LAPTOP-BFPIKR71\SQLEXPRESS;Initial Catalog=LeagueDB;Integrated Security=True";
            SpelerRepoADO spelerRepo = new SpelerRepoADO(connString);
            SpelerManager m = new SpelerManager(spelerRepo);
            var speler2 = spelerRepo.SelecteerSpeler(2);
            var speler3 = spelerRepo.SelecteerSpeler(3);

            //var spelerlijst = m.SelecteerSpelers(null, "jos");
            //try {
            //    Speler s = m.RegistreerSpeler("Fred", 169, null);
            //    Console.WriteLine(s);
            //} catch (Exception ex) {
            //    Console.WriteLine(ex.Message);
            //}
            //Speler s = m.RegistreerSpeler("Yorben", 178, 63);
            //s.ZetGewicht(72);
            //s.ZetLengte(179);
            //m.UpdateSpeler(s);

            TeamRepoADO teamRepo = new TeamRepoADO(connString);
            TeamManager t = new TeamManager(teamRepo);
            //var teamlijst = t.SelecteerTeams();

            //t.RegistreerTeam(114, "Westerlo2", null);
            //Team team = new Team(114, "Westerlo2");
            //team.ZetBijnNaam("The jumping unicorns");
            //t.UpdateTeam(team);
            //Team team = t.SelecteerTeam(112);

            //try {
            //    t.RegistreerTeam(112, "Gent2", "random");
            //} catch(Exception ex) {
            //    Console.WriteLine(ex.Message);
            //}
            //Team team = t.SelecteerTeam(112);
            //SpelerRepoADO spelerRepoADO = new SpelerRepoADO(connString);
            //SpelerManager spelerManager = new SpelerManager(spelerRepoADO);
            //Speler speler = spelerManager.RegistreerSpeler("Ellen", 172, null);
            //TeamRepoADO teamRepoADO = new TeamRepoADO(connString);
            //TeamManager teamManager = new TeamManager(teamRepoADO);
            SpelerInfo spelerinfo = new SpelerInfo(4, "Fred", 169, null, null, null);
            TeamInfo teamInfo = new TeamInfo(114, "Westerlo2", "The Jumping Unicorns");

            ITransferRepository transferRepository = new TransferRepoADO(connString);
            TransferManager transferManager = new TransferManager(transferRepository, spelerRepo, teamRepo);
            transferManager.RegistreerTransfer(spelerinfo, teamInfo, 100);
        }
    }
}
