using System;
using System.ComponentModel.DataAnnotations;

namespace TP3_NET.Models.Employe
{
    public class Competence
    {
        [Flags]
        public enum EnumCompetence
        {
            Diagnostiquer = 1,
            Entretenir = 2,
            Reparer = 3
        }

        public static EnumCompetence Parse(string competence)
        {
            switch (competence)
            {
                 case "Diagnostiquer":
                     return EnumCompetence.Diagnostiquer;
                 case "Entretenir":
                     return EnumCompetence.Entretenir;
                 case "Reparer":
                     return EnumCompetence.Reparer;
                 default:
                     throw new ArgumentException("Impossible de parser la compétence");
            }
        }

        [Key] public int IdCompetence { get; set; }
        public EnumCompetence TypeCompetence { get; set; }
    }
}