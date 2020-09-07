using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TP3_NET.Models.Commun;

namespace TP3_NET.Models.Livraison
{
    public class Itineraire
    {
        [Key] public int IdItineraire { get; set; }

        public List<Adresse> Adresses { get; set; }
    }
}