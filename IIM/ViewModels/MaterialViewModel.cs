using System;
using System.Collections.Generic;
using System.Linq;
using IIM.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web.Mvc;
using IIM.App_Start;

namespace IIM.ViewModels
{
    public class MaterialViewModel
    {
        private string _photoUrl;
        [Display(Name = "Artikelnummer")]
        public string ArticleNr { get; set; }
        [Display(Name = "Opleidingsonderdelen")]
        public List<Curricular> Curriculars { get; set; }
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }
        public int Id { get; set; }
        [Display(Name = "Codering")]
        public string Encoding { get; set; }
        [Display(Name = "Firma")]
        public Firm Firm { get; set; }
        [Display(Name = "Naam")]
        public string Name { get; set; }
        [Display(Name = "Foto")]
        public string PhotoUrl
        {
            get { return _photoUrl ?? "~/Content/photo-coming-soon.jpg"; }
            set { _photoUrl = value; }
        }

        [DisplayFormat(DataFormatString = "{0:c}")]
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
            Encoding = m.Encoding;
            Firm = m.Firm;
            Name = m.Name;
            PhotoUrl = $"{AppSettings.ImageStorageUrl}/{m.Id}.{m.Encoding}";
            Price = m.Price;
            TargetGroups = m.TargetGroups;
        }

        public MaterialViewModel()
        {
            
        }
    }

    public class InventoryViewModel
    {

        public IEnumerable<SearchableItemModel> CurricularModels { get; set; }
        public SearchableItemModel CurricularSelectedValue { get; set; }
        public IEnumerable<MaterialViewModel> MaterialViewModels { get; set; }
        public IEnumerable<SearchableItemModel> TargetGroupModels { get; set; }
        public SearchableItemModel TargetGroupSelectedValue { get; set; }

        public InventoryViewModel()
        {
            
        }

        public InventoryViewModel(IEnumerable<SearchableItemModel> curricularModels, SearchableItemModel curricularSelectedValue, IEnumerable<MaterialViewModel> materialViewModels, IEnumerable<SearchableItemModel> targetGroupModels, SearchableItemModel targetGroupSelectedValue)
        {
            CurricularModels = curricularModels;
            CurricularSelectedValue = curricularSelectedValue;
            MaterialViewModels = materialViewModels;
            TargetGroupModels = targetGroupModels;
            TargetGroupSelectedValue = targetGroupSelectedValue;
        }
    }

    public class SearchableItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SearchableItemModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}