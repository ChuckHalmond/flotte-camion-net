using System;
using System.ComponentModel.DataAnnotations;

namespace TP3_NET.Models.Livraison
{
    public enum EtatLivraison
    {
        [Display(Name = "En préparation")]
        EnPreparation = 1,

        [Display(Name = "En cours")]
        EnCours = 2,

        [Display(Name = "Livrée")]
        Livree = 3
    }
}