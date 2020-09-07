namespace TP3_NET.Models.Employe
{
    public class Operateur : Employe
    {
        public Compte.Compte Compte { get; set; }
    }
}