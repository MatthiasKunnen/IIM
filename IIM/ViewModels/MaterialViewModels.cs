using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IIM.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace IIM.ViewModels
{
    public class MaterialViewModels
    {
        //public int Id { get; private set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public int Amount { get;  set; }
        public string ArticleNr { get;  set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get;  set; }
        public Firm Firm { get;  set; }
        public List<TargetGroup> TargetGroups { get;  set; }
        public List<Curricular> Curriculars { get;  set; }
        public MaterialViewModels() { }
        /*public MaterialViewModels(Material m)
        {
            this.Name = m.Name;
            this.Description = m.Description;
            this.Amount = m.Amount;
            this.ArticleNr = m.ArticleNr;
            this.Price = m.Price;
            this.Firm = m.Firm;
            this.TargetGroups = m.TargetGroups;
            this.Curriculars = m.Curriculars;
        }*/
    }
   
}