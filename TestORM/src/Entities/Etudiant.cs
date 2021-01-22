using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace TestORMCodeFirst.Entities
{
    [Table("ETUDIANT")]
    public class Etudiant
    {
        //Propriétés
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Identity est la valeur par défaut
        public short EtudiantID { get; set; }

        [Required]
        //[Column("NOM", TypeName = "varchar(50)", Order = 3)]
        [Column(TypeName = "varchar(50)")]
        public string Nom { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Prenom { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DateNaissance { get; set; }

        public short? NoProgramme { get; set; }

        public short? EtudiantTuteurID { get; set; } //short? permet les valeurs NULL, utile pour Datetime?, decimal?

        //Propriétés de navigation

        [ForeignKey("EtudiantTuteurID")]
        public virtual Etudiant Tuteur { get; set; }

        //Propriétés de navigation      

        public virtual ICollection<InscriptionCours> Cours { get; set; }
       

        //Méthodes
        public override String ToString() { return Nom + ", " + Prenom + ", " + DateNaissance.ToString(); }

        //Constructeur
        public Etudiant()
        {
            Cours = new List<InscriptionCours>();
        }


    }
}
