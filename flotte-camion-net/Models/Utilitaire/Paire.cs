using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_NET.Models.Utilitaire
{
    public class Paire<T, P>
    {
        public T Item1 { get; set; }
        public P Item2 { get; set; }

        public Paire()
        {
            Item1 = default(T);
            Item2 = default(P);
        }

        public Paire(T item1, P item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}