using System;
using System.ComponentModel.DataAnnotations;

namespace TP3_NET.Models.Marchandise
{
    public enum TypeMarchandise
    {
        [Display(Name = "Frais")]
        Frais = 1,

        [Display(Name = "Fragile")]
        Fragile = 2,

        [Display(Name = "Congelé")]
        Congele = 3,

        [Display(Name = "Animaux")]
        Animal = 4,

        [Display(Name = "Viande")]
        Viande = 5,

        [Display(Name = "Poisson")]
        Poisson = 6,

        [Display(Name = "Essence")]
        Essence = 7,

        [Display(Name = "Matériaux de construction")]
        MaterielConstruction = 8
    }
}