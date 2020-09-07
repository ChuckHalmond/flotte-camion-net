using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_NET.Models.Service
{
    public class IdentityService
    {
        public static bool SessionUserIsIdentified(HttpSessionStateBase session)
        {
            return session["typeCompte"] != null;
        }

        public static bool SessionUserIsOperateur(HttpSessionStateBase session)
        {
            return session["typeCompte"] != null && session["typeCompte"].Equals(TP3_NET.Models.Compte.TypeCompte.Operateur);
        }

        public static bool SessionUserIsClient(HttpSessionStateBase session)
        {
            return session["typeCompte"] != null && session["typeCompte"].Equals(TP3_NET.Models.Compte.TypeCompte.Client);
        }
    }
}