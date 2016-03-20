using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class Material
    {
        #region Attributes

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        public Firm Firm { get; set; }

        public decimal Price { get; set; }

        public string ArticleNr { get; set; }

        public int Id { get; set;}

        public string Encoding { get; set; }

        public int Amount { get; set; }

        public string Place { get; set; }

        private Visibility Visibility { get; set; }

        public List<Curricular> Curriculars { get; set; }

        public List<TargetGroup> TargetGroups { get; set; }

        #endregion

        #region Constructors

        public Material()
        {
        }

        public Material(string name, int amount)
        {
            this.Name = name;
            this.Amount = amount;
        }
        #endregion

        #region Actions

        #endregion
    }
}