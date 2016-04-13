using System;
using System.Collections.Generic;
using System.Linq;
using IIM.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IIM.ViewModels
{
    public class MaterialViewModel
    {
        private string _photoUrl;
        private ViewDataDictionary viewData;
        [Display(Name = "Artikelnummer")]

        public string ArticleNr { get; set; }
        [Display(Name= "Opleidingsonderdelen")]
        public List<Curricular> Curriculars { get; set; }
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }
        public int Id { get; set; }

        public string Image { get; set; }
        [Display(Name = "Firma")]
        public Firm Firm { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set { _photoUrl = value ?? "~/Content/photo-coming-soon.jpg"; }
        }

        [DisplayFormat(DataFormatString = " €{0:0.00}")]
        [Display(Name = "Prijs")]
        public decimal Price { get; set; }
        [Display(Name = "Doelgroepen")]
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

        public MaterialViewModel(ViewDataDictionary viewData)
        {
            this.viewData = viewData;
        }
    }
}