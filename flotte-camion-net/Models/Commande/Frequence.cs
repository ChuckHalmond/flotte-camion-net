using System;
using System.ComponentModel.DataAnnotations;

namespace TP3_NET.Models.Commande
{
    public enum Frequence
    {
        [Display(Name = "Non applicable")]
        NonApplicable = 1,

        [Display(Name = "Quotidien")]
        Quotidien = 2,

        [Display(Name = "Hebdomadaire")]
        Hebdomadaire = 3,

        [Display(Name = "Mensuel")]
        Mensuel = 4
    }
}