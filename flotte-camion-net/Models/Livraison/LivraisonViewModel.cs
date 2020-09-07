using System;
using System.ComponentModel.DataAnnotations;

namespace TP3_NET.Models.Livraison
{
    public class LivraisonViewModel
    {
        [Required]
        public int IdLivraison { get; set; }

        [Display(Name = "Commande", Description = "")]
        public int IdCommande { get; set; }

        [Display(Name = "Client", Description = "")]
        public int IdClient { get; set; }

        [Display(Name = "Nb. marchandises", Description = "")]
        public int NbMarchandises { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un conducteur")]
        [Display(Name = "Conducteur", Prompt = "Sélectionner un conducteur", Description = "")]
        public int IdConducteur { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Veuillez saisir la date de départ")]
        [Display(Name = "Date de départ", Description = "")]
        public DateTime DateDepart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Veuillez saisir la date d'arrivée")]
        [Display(Name = "Date d'arrivée", Description = "")]
        public DateTime DateArrivee { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un camion")]
        [Display(Name = "Camion", Prompt = "Sélectionner un camion", Description = "")]
        public int IdCamion { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner une remorque")]
        [Display(Name = "Remorque", Prompt = "Sélectionner une remorque", Description = "")]
        public int IdRemorque { get; set; }

        [Display(Name = "État de la livraison", Description = "")]
        public EtatLivraison EtatLivraison { get; set; } = EtatLivraison.EnPreparation;

        [Display(Name = "Itinéraire", Description = "")]
        public string Itineraire { get; set; }
        public int IdItineraire { get; set; }

        public static LivraisonViewModel FromLivraison(Livraison livraison)
        {
            LivraisonViewModel livraisonViewModel = new LivraisonViewModel();

            livraisonViewModel.IdCamion = (livraison.Camion == null) ? 0 : livraison.Camion.IdCamion;
            livraisonViewModel.IdClient = livraison.Client.IdClient;
            livraisonViewModel.IdCommande = livraison.Commande.IdCommande;
            livraisonViewModel.NbMarchandises = livraison.Marchandises.Count;
            livraisonViewModel.IdConducteur = (livraison.Conducteur == null) ? 0 : livraison.Conducteur.IdEmploye; 
            livraisonViewModel.DateArrivee = livraison.DateArrivee;
            livraisonViewModel.DateDepart = livraison.DateDepart;
            livraisonViewModel.EtatLivraison = livraison.EtatLivraison;
            livraisonViewModel.IdLivraison = livraison.IdLivraison;
            livraisonViewModel.Itineraire = (livraison.Itineraire.Adresses.Count > 2) ? livraison.Itineraire.Adresses[0].Ville + " -> " + livraison.Itineraire.Adresses[livraison.Itineraire.Adresses.Count - 1].Ville : "Itinéraire indéterminable";
            livraisonViewModel.IdItineraire = (livraison.Itineraire == null) ? 0 : livraison.Itineraire.IdItineraire;
            livraisonViewModel.IdRemorque = (livraison.Remorque == null) ? 0 : livraison.Remorque.IdRemorque;

            return livraisonViewModel;
        }
    }
}