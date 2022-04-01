using LeagueBL.Domein;
using LeagueBL.Exceptions;
using System;
using Xunit;

namespace TestProjectDomein {
    public class UnitTestSpeler {
        [Fact]
        public void ZetId_valid() {
            Speler s = new Speler(10, "Jos", 180, 80);
            Assert.Equal(10, s.Id);
            s.ZetId(1);
            Assert.Equal(1, s.Id);
        }
        
        [Fact]
        public void ZetId_invalid() {
            Speler s = new Speler(10, "Jos", 180, 80);
            Assert.Equal(10, s.Id);
            Assert.Throws<SpelerException>(() => s.ZetId(0));
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void ZetRugnummer_valid(int rugnr) {
            Speler s = new Speler(10, "Jos", 180, 80);
            s.ZetRugnummer(rugnr);
            Assert.Equal(rugnr, s.Rugnummer);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public void ZetRugnummer_invalid(int rugnr) {
            Speler s = new Speler(10, "Jos", 180, 80);
            Assert.Throws<SpelerException>(() => s.ZetRugnummer(rugnr));
        }

        [Theory]
        [InlineData("Jeff", "Jeff")]
        [InlineData("Jefke     ", "Jefke")]
        [InlineData("Eden Hazard", "Eden Hazard")]
        [InlineData("     Eden Hazard", "Eden Hazard")]
        public void ZetNaam_valid(string naamIn, string naamUit) {
            Speler s = new Speler(10, "Jos", 180, 80);
            s.ZetNaam(naamIn);
            Assert.Equal(naamUit, s.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetNaam_invalid(string naam) {
            Speler s = new Speler(10, "Jos", 180, 80);
            Assert.Throws<SpelerException>(() => s.ZetNaam(naam));
        }
        [Theory]
        [InlineData(10, "Jos", 150,80, 10, "Jos", 150, 80)]
        [InlineData(1, " Jos ", 150, 50, 1, "Jos", 150, 50)]
        [InlineData(1, " Jos ", null, 50, 1, "Jos", null, 50)]
        [InlineData(1, " Jos ", 170, null, 1, "Jos", 170, null)]
        public void Ctor_valid(int id, string naam, int? lengte, int? gewicht, int vid, string vnaam, int? vlengte, int? vgewicht) { // v van verwacht
            Speler s = new Speler(id, naam, lengte, gewicht);
            Assert.Equal(vid, s.Id);
            Assert.Equal(vnaam, s.Naam);
            Assert.Equal(vlengte, s.Lengte);
            Assert.Equal(vgewicht, s.Gewicht);
        }
        [Theory]
        [InlineData(0, "Jos", 150, 80)]
        [InlineData(5, " ", 150, 80)]
        [InlineData(5, "", 150, 80)]
        [InlineData(5, null, 150, 80)]
        [InlineData(5, " \n ", 150, 80)]
        [InlineData(5, " \r ", 150, 80)]
        [InlineData(5, "Jos", 149, 80)]
        [InlineData(5, "Jos", 149, 49)]
        public void Ctor_invalid(int id, string naam, int? lengte, int? gewicht) {
            Assert.Throws<SpelerException>(() => new Speler(id, naam, lengte, gewicht));
        }

        [Fact]
        public void VerwijderTeam_valid() {
            Speler s = new Speler(10, "Jos", 180, 80);
            Team t = new Team(1, "A'pen");
            s.ZetTeam(t);
            s.VerwijderTeam();
            Assert.Null(s.Team);
            Assert.DoesNotContain(s, t.Spelers());
        }
        [Fact]
        public void ZetTeam_valid() {
            Speler s = new Speler(10, "Jos", 180, 80);
            Team t = new Team(1, "Apen");
            Team t2 = new Team(1, "Gent"); // op deze manier checken we ook de equals
            s.ZetTeam(t);
            Assert.Equal(t2, s.Team);
            Assert.Contains(s, t.Spelers());
        }
        [Fact]
        public void ZetTeam_invalid() {
            Speler s = new Speler(10, "Jos", 180, 80);
            Team t = new Team(1, "Apen");
            s.ZetTeam(t);
            // Team t2 = new Team(1, "Gent"); // op deze manier checken we ook de equals
            Assert.Throws<SpelerException>(() => s.ZetTeam(null));
            Assert.Throws<SpelerException>(() => s.ZetTeam(t));
            Assert.Equal(t, s.Team); // controleren of hij effectief niets heeft veranderd.
            Assert.Contains(s, t.Spelers());
        }
    }
}
