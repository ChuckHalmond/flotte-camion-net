using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP3_NET.Models.Marchandise;

namespace TP3_NET.Models.Camion
{
    public class Remorque
    {
        [Key] public int IdRemorque { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(6, ErrorMessage = "L'immatriculation ne peut dépasser 6 caractères")]
        [Required(ErrorMessage = "Veuillez saisir l'immatriculation de la remorque")]
        public string Immatriculation { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le prix journalier de la remorque")]
        public double PrixJournalier { get; set; }

        [Required(ErrorMessage = "Veuillez saisir la capacité de la remorque")]
        public double Capacite { get; set; }

        [Required(ErrorMessage = "Veuillez choisir le type de marchandises")]
        public TypeMarchandise TypeMarchandise { get; set; }
    }
}