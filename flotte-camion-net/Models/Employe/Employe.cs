using System.ComponentModel.DataAnnotations;

namespace TP3_NET.Models.Employe
{
    public abstract class Employe
    {
        [Key] public int IdEmploye { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le nom de l'employé")]
        [MaxLength(50, ErrorMessage = "Le nom de l'employé ne peut dépasser 50 caractères")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le prénom de l'employé")]
        [MaxLength(50, ErrorMessage = "Le prénom de l'employé ne peut dépasser 50 caractères")]
        public string Prenom { get; set; }
    }
}