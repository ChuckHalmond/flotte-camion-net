using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP3_NET.Models.Commun;

namespace TP3_NET.Models.Commande
{
    public class Commande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCommande { get; set; }
        public Client.Client Client { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCommande { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DepartSouhaitee { get; set; }

        public Adresse LieuDepart { get; set; }
        public Adresse LieuArrivee { get; set; }

        public Frequence Frequence { get; set; } = Frequence.NonApplicable;

        public List<Marchandise.Marchandise> Marchandises { get; set; }
    }
}