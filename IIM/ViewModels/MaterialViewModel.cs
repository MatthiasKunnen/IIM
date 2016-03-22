using System;
using System.Collections.Generic;
using System.Linq;
using IIM.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace IIM.ViewModels
{
    public class MaterialViewModel
    {
        public string ArticleNr { get; set; }
        public List<Curricular> Curriculars { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Firm Firm { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        public List<TargetGroup> TargetGroups { get; set; }

        public MaterialViewModel(Material m)
        {
            ArticleNr = m.ArticleNr;
            Curriculars = m.Curriculars;
            Description = m.Description;
            Image = m.Encoding;
            Firm = m.Firm;
            Name = m.Name;
            PhotoUrl = m.PhotoUrl;
            Price = m.Price;
            TargetGroups = m.TargetGroups;
        }
    }
}