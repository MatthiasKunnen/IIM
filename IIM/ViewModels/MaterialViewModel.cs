using System;
using System.Collections.Generic;
using System.Linq;
using IIM.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace IIM.ViewModels
{
    public class MaterialViewModel
    {
        private string _photoUrl;

        public string ArticleNr { get; set; }
        public List<Curricular> Curriculars { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public Firm Firm { get; set; }
        public string Name { get; set; }
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set { _photoUrl = value ?? "~/Content/photo-coming-soon.jpg"; }
        }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        public List<TargetGroup> TargetGroups { get; set; }

        public MaterialViewModel(Material m)
        {
            ArticleNr = m.ArticleNr;
            Id = m.Id;
            Curriculars = m.Curriculars;
            Firm = m.Firm;
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