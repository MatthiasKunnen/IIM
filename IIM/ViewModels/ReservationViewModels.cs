using Foolproof;
using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IIM.ViewModels.ReservationViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; private set; }
        [Display(Name = "Aanmaakdatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreationDate { get; private set; }
        [Display(Name = "Ophaaldatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [LessThan("EndDate")]
        public DateTime StartDate { get; private set; }
        [Display(Name = "Terugbrengdatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [GreaterThan("StartDate")]
        public DateTime EndDate { get; private set; }

        public List<ReservationDetailViewModel> Details { get; set; }

        public ReservationViewModel(Reservation r)
        {
            Id = r.Id;
            CreationDate = r.CreationDate;
            StartDate = r.StartDate;
            EndDate = r.EndDate;
            Details = r.Details.Select(d => new ReservationDetailViewModel(d)).ToList();
        }
    }

    public class NewReservationViewModel{
        //public datepickerviewmodel
        public List<ReservationDetailSelectionViewModel> TheDetails; 
    }

    public class ReservationDetailViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime BroughtBackDate { get; private set; }
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime PickUpDate { get; private set; }
        public MaterialViewModel Material { get; private set; }
        public ReservationDetailViewModel(ReservationDetail detail)
        {
            if (detail.BroughtBackDate != null) this.BroughtBackDate = detail.BroughtBackDate.Value;
            if (detail.PickUpDate != null) this.PickUpDate = detail.PickUpDate.Value;
            this.Material = new MaterialViewModel(detail.MaterialIdentifier.Material);
        }
    }

    public class ReservationDetailSelectionViewModel
    {        
        public int MaxAmount { get; private set; }
        [Display(Name = "Aantal")]
        public int RequestedAmount { get; set; }
        [Display()]
        public MaterialViewModel TheMaterial {get; private set;}
        public ReservationDetailSelectionViewModel(Material material, int maxAmount, int requestedAmount)
        {
            TheMaterial = new MaterialViewModel(material);
            MaxAmount = maxAmount;
            RequestedAmount = requestedAmount;
        }
    }

}