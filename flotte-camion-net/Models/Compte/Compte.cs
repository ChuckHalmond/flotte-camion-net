using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3_NET.Models.Compte
{
    public class Compte
    {
        [Key]
        public int IdCompte { get; set; }

        public TypeCompte TypeCompte { get; set; } = TypeCompte.Client;

        [Required(ErrorMessage = "Veuillez saisir votre adresse mail")]
        [MaxLength(25, ErrorMessage = "Votre adresse mail ne peut dépasser 25 caractères")]
        [RegularExpression("[A-Za-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}", ErrorMessage =
            "Veuillez respecter le format de l'adresse mail")]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Veuillez saisir votre mot de passe")]
        [MinLength(6, ErrorMessage = "Votre mot de passe doit contenir au minimum 6 caractères")]
        [MaxLength(64, ErrorMessage = "Votre mot de passe ne peut pas faire plus de 64 caractères")]
        public string MotDePasse { get; set; }
    }
}