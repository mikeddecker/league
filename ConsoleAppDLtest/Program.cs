using LeagueDL;
using System;
using LeagueBL.Managers;
using LeagueBL.Domein;

namespace ConsoleAppDLtest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            string connString = @"Data Source=LAPTOP-BFPIKR71\SQLEXPRESS;Initial Catalog=LeagueDB;Integrated Security=True";
            //SpelerRepoADO r = new SpelerRepoADO(connString);
            //SpelerManager m = new SpelerManager(r);
            //Speler s = m.RegistreerSpeler("Fred", 169, null);
            //Console.WriteLine(s);
            TeamRepoADO r = new TeamRepoADO(connString);
            TeamManager t = new TeamManager(r);
            t.RegistreerTeam(112, "Gent2", "random");

        }
    }
}
