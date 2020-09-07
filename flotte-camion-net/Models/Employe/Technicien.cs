using System.Collections.Generic;

namespace TP3_NET.Models.Employe
{
    public class Technicien : Employe
    {
        public ICollection<Competence> Competences { get; set; }

        public void AddCompetence(Competence competence)
        {
            if (Competences == null)
                Competences = new List<Competence>();
            var found = false;
            foreach (var inCompetence in Competences)
            {
                if (inCompetence.TypeCompetence.Equals(competence.TypeCompetence))
                    found = true;
            }
            if (!found)
                Competences.Add(competence);
        }
    }
}