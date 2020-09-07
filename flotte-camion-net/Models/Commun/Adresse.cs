using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3_NET.Models.Commun
{
    public class Adresse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdresse { get; set; }

        [Required(ErrorMessage = "Veuillez saisir votre adresse")]
        [MaxLength(25, ErrorMessage = "L'adresse ne peut comporter que 25 caractères maximum")]
        public string Rue { get; set; }

        [MaxLength(25, ErrorMessage = "Le complément d'adresse ne peut contenir que 25 caractères")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "Veuillez saisir la ville de votre adresse")]
        [MaxLength(25, ErrorMessage = "Le champs ville n'accepte que 25 caractères")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le pays de votre adresse")]
        [MaxLength(25, ErrorMessage = "Le pays ne peut faire que 25 caractères")]
        public string Pays { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le code postal de votre adresse")]
        [MaxLength(10, ErrorMessage = "Le code postal ne peut contenir que 10 caractères")]
        public string CodePostal { get; set; }

        public int? IdItineraire { get; set; }
        public virtual Models.Livraison.Itineraire Itineraire { get; set; }
    }
}