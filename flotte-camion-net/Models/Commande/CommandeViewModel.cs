using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP3_NET.Models.Commande
{
    public class CommandeViewModel
    {
        public int IdCommande { get; set; }

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Veuillez saisir la date de la commande")]
        [Display(Name = "Date de commande", Description = "")]
        public DateTime DateCommande { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Veuillez saisir la date de départ souhaitée")]
        [Display(Name = "Date de départ souhaitée", Description = "")]
        public DateTime DepartSouhaitee { get; set; }

        /*
         * Adresse de départ
         */

        public int IdAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir la rue de départ")]
        [MaxLength(25, ErrorMessage = "La rue de départ ne peut comporter que 25 caractères maximum")]
        [Display(Name = "Rue", Prompt = "Rue de départ", Description = "")]
        public string RueAD { get; set; }

        [MaxLength(25, ErrorMessage = "Le complément d'adresse de départ ne peut contenir que 25 caractères")]
        [Display(Name = "Complément d'adresse", Prompt = "Complément d'adresse de départ", Description = "")]
        public string ComplementAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir la ville de départ")]
        [MaxLength(25, ErrorMessage = "La ville de départ ne peut faire que 25 caractères maximum")]
        [Display(Name = "Ville", Prompt = "Ville de départ", Description = "")]
        public string VilleAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le pays de départ")]
        [MaxLength(25, ErrorMessage = "Le pays de départ ne peut faire que 25 caractères maximum")]
        [Display(Name = "Pays", Prompt = "Pays de départ", Description = "")]
        public string PaysAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le code postal de départ")]
        [MaxLength(10, ErrorMessage = "Le code postal ne peut contenir que 10 caractères maximum")]
        [Display(Name = "Code postal", Prompt = "Code postal de départ", Description = "")]
        public string CodePostalAD { get; set; }

        /*
         * Adresse d'arrivée
         */

        public int IdAA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir la rue d'arrivée")]
        [MaxLength(25, ErrorMessage = "La rue d'arrivée ne peut comporter que 25 caractères maximum")]
        [Display(Name = "Rue", Prompt = "Rue d'arrivée", Description = "")]
        public string RueAA { get; set; }

        [MaxLength(25, ErrorMessage = "Le complément d'adresse d'arrivée ne peut contenir que 25 caractères")]
        [Display(Name = "Complément d'adresse", Prompt = "Complément d'adresse d'arrivée", Description = "")]
        public string ComplementAA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir la ville d'arrivée")]
        [MaxLength(25, ErrorMessage = "La ville d'arrivée ne peut faire que 25 caractères maximum")]
        [Display(Name = "Ville", Prompt = "Ville d'arrivée", Description = "")]
        public string VilleAA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le pays d'arrivée")]
        [MaxLength(25, ErrorMessage = "Le pays d'arrivée ne peut faire que 25 caractères maximum")]
        [Display(Name = "Pays", Prompt = "Pays d'arrivée", Description = "")]
        public string PaysAA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Veuillez saisir le code postal d'arrivée")]
        [MaxLength(10, ErrorMessage = "Le code postal ne peut contenir que 10 caractères maximum")]
        [Display(Name = "Code postal", Prompt = "Code postal d'arrivée", Description = "")]
        public string CodePostalAA { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Veuillez choisir une fréquence")]
        [Display(Name = "Fréquence", Prompt = "Sélectionner une fréquence", Description = "")]
        public Frequence Frequence { get; set; } = Frequence.NonApplicable;

        public List<Marchandise.Marchandise> Marchandises { get; set; }

        public Commande ToCommande()
        {
            Commande commande = new Commande();
            commande.IdCommande = IdCommande;
            commande.DateCommande = DateCommande;
            commande.DepartSouhaitee = DepartSouhaitee;
            commande.Frequence = Frequence;
            commande.Marchandises = Marchandises;

            Commun.Adresse adresseDepart = new Commun.Adresse();
            adresseDepart.IdAdresse = IdAD;
            adresseDepart.CodePostal = CodePostalAD;
            adresseDepart.Pays = PaysAD;
            adresseDepart.Rue = RueAD;
            adresseDepart.Ville = VilleAD;

            Commun.Adresse adresseArrivee = new Commun.Adresse();
            adresseArrivee.IdAdresse = IdAA;
            adresseArrivee.CodePostal = CodePostalAA;
            adresseArrivee.Pays = PaysAA;
            adresseArrivee.Rue = RueAA;
            adresseArrivee.Ville = VilleAA;

            commande.LieuArrivee = adresseArrivee;
            commande.LieuDepart = adresseDepart;

            return commande;
        }

        public static CommandeViewModel FromCommande(Commande commande)
        {
            CommandeViewModel commandeViewModel = new CommandeViewModel();
            commandeViewModel.IdCommande = commande.IdCommande;
            commandeViewModel.DateCommande = commande.DateCommande;
            commandeViewModel.DepartSouhaitee = commande.DepartSouhaitee;
            commandeViewModel.Frequence = commande.Frequence;
            commandeViewModel.Marchandises = commande.Marchandises;

            Commun.Adresse adresseDepart = commande.LieuDepart;
            if (adresseDepart != null)
            {
                commandeViewModel.IdAD = adresseDepart.IdAdresse;
                commandeViewModel.CodePostalAD = adresseDepart.CodePostal;
                commandeViewModel.PaysAD = adresseDepart.Pays;
                commandeViewModel.RueAD = adresseDepart.Rue;
                commandeViewModel.VilleAD = adresseDepart.Ville;
            }

            Commun.Adresse adresseArrivee = commande.LieuArrivee;
            if (adresseArrivee != null)
            {
                commandeViewModel.IdAA = adresseArrivee.IdAdresse;
                commandeViewModel.CodePostalAA = adresseArrivee.CodePostal;
                commandeViewModel.PaysAA = adresseArrivee.Pays;
                commandeViewModel.RueAA = adresseArrivee.Rue;
                commandeViewModel.VilleAA = adresseArrivee.Ville;
            }

            return commandeViewModel;
        }
    }
}