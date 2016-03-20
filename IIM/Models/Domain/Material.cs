using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class Material
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Firm Firm { get; set; }

        public decimal Price { get; set; }

        public string ArticleNr { get; set; }

        public int Id { get; set; }

        public string Encoding { get; set; }

        public List<Curricular> Curriculars { get; set; }

        public List<TargetGroup> TargetGroups { get; set; }

        public int Identifiers { get; set; }
    }
}