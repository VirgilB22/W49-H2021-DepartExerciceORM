using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;
using Xunit;

namespace TestORMCodeFirst.DAL
{
    public class EFEtudiantRepositoryTest
    {

        private EFEtudiantRepository repoEtudiants;
        private EFInscCoursRepository repoInscriptions;
        private void SetUp()
        {
            // Initialiser les objets nécessaires aux tests
            var builder = new DbContextOptionsBuilder<CegepContext>();
            builder.UseInMemoryDatabase(databaseName: "testEtudiant_db");   // Database en mémoire
            var contexte = new CegepContext(builder.Options);
            repoEtudiants = new EFEtudiantRepository(contexte);
            repoInscriptions = new EFInscCoursRepository(contexte);
        }

        [Fact]
        public void ValiderNombreCoursInscrits()
        {
            // Arrange
            SetUp();
            Etudiant etud = new Etudiant
            {
                Nom = "Simard",
                Prenom = "Serge",
                DateNaissance = Convert.ToDateTime("1977-10-10"),
                NoProgramme = 420
            };

            repoEtudiants.AjouterEtudiant(etud);

            // Act
            var result = repoEtudiants.NombreCoursInscrits(etud.EtudiantID);

            // Assert
            Assert.Equal(expected: 0, actual: result);
        }

        [Fact]
        public void CreerEtudiant()
        {
            // Arrange
            SetUp();
            Etudiant etud = new Etudiant
            {
                Nom = "Simard",
                Prenom = "Serge",
                DateNaissance = Convert.ToDateTime("1977-10-10"),
                NoProgramme = 420
            };

            // Act
            repoEtudiants.AjouterEtudiant(etud);

            // Assert
            var result = this.repoEtudiants.ObtenirListeEtudiants();
            Assert.Single(result);
            Assert.Same(etud, result.First());
        }

        [Fact]
        public void SupprimerTousEtudiants()
        {
            // Arrange
            SetUp();
            Etudiant etud = new Etudiant
            {
                Nom = "Simard",
                Prenom = "Serge",
                DateNaissance = Convert.ToDateTime("1977-10-10"),
                NoProgramme = 420
            };

            Etudiant etud2 = new Etudiant
            {
                Nom = "Tremblay",
                Prenom = "Sylvie",
                DateNaissance = Convert.ToDateTime("1982-10-10"),
                NoProgramme = 420
            };

            repoEtudiants.AjouterEtudiant(etud);
            repoEtudiants.AjouterEtudiant(etud2);

            // Act
            repoEtudiants.SupprimerTousEtudiants();

            // Assert
            var result = repoEtudiants.ObtenirListeEtudiants();
            Assert.Empty(result);
        }

        [Fact]
        public void TrouverEtudiantParNom()
        {
            // Arrange
            SetUp();
            Etudiant etud = new Etudiant
            {
                Nom = "Simard",
                Prenom = "Serge",
                DateNaissance = Convert.ToDateTime("1977-10-10"),
                NoProgramme = 420
            };

            Etudiant etud2 = new Etudiant
            {
                Nom = "Tremblay",
                Prenom = "Sylvie",
                DateNaissance = Convert.ToDateTime("1982-10-10"),
                NoProgramme = 420
            };

            repoEtudiants.AjouterEtudiant(etud);
            repoEtudiants.AjouterEtudiant(etud2);

            // Act
            var result = repoEtudiants.TrouverEtudiantParNom(etud.Nom);

            // Assert
            Assert.Equal(expected: etud.Nom, actual: result.Nom);
        }

        [Fact]
        public void TrouverEtudiantParNomPasResultat()
        {
            // Arrange
            SetUp();
            Etudiant etud = new Etudiant
            {
                Nom = "Simard",
                Prenom = "Serge",
                DateNaissance = Convert.ToDateTime("1977-10-10"),
                NoProgramme = 420
            };

            Etudiant etud2 = new Etudiant
            {
                Nom = "Tremblay",
                Prenom = "Sylvie",
                DateNaissance = Convert.ToDateTime("1982-10-10"),
                NoProgramme = 420
            };

            repoEtudiants.AjouterEtudiant(etud);
            repoEtudiants.AjouterEtudiant(etud2);

            // Act
            var result = repoEtudiants.TrouverEtudiantParNom("Tanguay");

            // Assert
            Assert.Null(result);
        }

    }
}
