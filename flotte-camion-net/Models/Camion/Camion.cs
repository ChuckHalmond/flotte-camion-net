using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3_NET.Models.Camion
{
    public class Camion
    {
        [Key] public int IdCamion { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Veuillez saisir l'immatriculation du camion")]
        [MaxLength(6, ErrorMessage = "L'immatriculation ne peut dépasser 6 caractères")]
        public string Immatriculation { get; set; }

        [Required(ErrorMessage = "Veuillez saisir le prix journalier du camion")]
        public double PrixJournalier { get; set; }

        [Required(ErrorMessage = "Veuillez saisir la capacité du camion")]
        public double Capacite { get; set; }

        [Required(ErrorMessage = "Veuillez choisir l'état actuel du camion")]
        public EtatCamion EtatCamion { get; set; } = EtatCamion.Disponible;
    }
}