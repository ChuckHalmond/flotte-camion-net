using System.ComponentModel.DataAnnotations;
using TP3_NET.Models.Commun;

namespace TP3_NET.Models.Client
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }

        [MaxLength(50, ErrorMessage = "Le nom de votre entreprise ne peut dépasser 50 caractères")]
        public string Entreprise { get; set; }

        [Required(ErrorMessage = "Veuillez saisir votre nom")]
        [MaxLength(50, ErrorMessage = "Votre nom ne peut dépasser 50 caractères")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Veuillez saisir votre prénom")]
        [MaxLength(50, ErrorMessage = "Votre prénom ne peut dépasser 50 caractères")]
        public string Prenom { get; set; }

        [MaxLength(12, ErrorMessage = "Votre numéro de téléphone ne peut dépasser 12 caractères")]
        [RegularExpression("^(?:0|\\(?\\+33\\)?\\s?|0033\\s?)[1-79](?:[\\.\\-\\s]?\\d\\d){4}$", ErrorMessage =
            "Veuillez respecter la format du numéro de téléphone")]
        public string Telephone { get; set; }

        public Adresse Adresse { get; set; }
        public Fidelite Fidelite { get; set; } = Fidelite.Bronze;
        public Compte.Compte Compte { get; set; }
    }
}