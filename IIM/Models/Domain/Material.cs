using System;
using System.Collections.Generic;
using System.Linq;
using IIM.Models.DAL;
using Ninject.Infrastructure.Language;

namespace IIM.Models.Domain
{
    public class Material
    {
        public string ArticleNr { get; private set; }
        public virtual List<Curricular> Curriculars { get; private set; }
        public string Description { get; private set; }
        public string Encoding { get; private set; }
        public virtual Firm Firm { get; private set; }
        public int Id { get; private set; }
        public virtual ICollection<MaterialIdentifier> Identifiers { get; private set; }
        public string Name { get; private set; }
        public string PhotoUrl => this.Id == 0 || this.Encoding == null ? null : $"https://iim.blob.core.windows.net/images/{this.Id}.{this.Encoding}";
        public decimal Price { get; private set; }
        public virtual List<TargetGroup> TargetGroups { get; private set; }
        
    }
}