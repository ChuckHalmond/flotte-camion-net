using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3_NET.Models.Marchandise
{
    public class Marchandise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMarchandise { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le nom de la marchandise")]
        [MaxLength(25, ErrorMessage = "Le nom de la marchandise ne peut pas dépasser 25 caractères")]
        [Display(Name = "Nom", Prompt = "Insérer un nom", Description = "")]
        public string Nom { get; set; }

        [Range(1, 100, ErrorMessage = "Le volume de marchandise doit être situé entre 1 et 100m³")]
        [Required(ErrorMessage = "Veuillez saisir le volume de la marchandise")]
        [Display(Name = "Volume", Prompt = "Insérer un volume", Description = "")]
        public double Volume { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Veuillez choisir le type de la marchandise")]
        [Display(Name = "Type de marchandise", Prompt = "Sélectionner un type de marchandise", Description = "")]
        public TypeMarchandise TypeMarchandise { get; set; }

        public virtual Commande.Commande Commande { get; set; }
        public int IdCommande { get; set; }

        public virtual Livraison.Livraison Livraison { get; set; }
        public int? IdLivraison { get; set; }
    }
}