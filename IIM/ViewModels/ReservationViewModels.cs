using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IIM.ViewModels.ReservationViewModels
{
    public class ReservationsViewModel
    {   
        public int Id { get; private set; }
        [Display(Name = "Aanmaakdatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreationDate { get; private set; }
        [Display(Name = "Ophaaldatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime StartDate { get; private set; }
        [Display(Name = "Terugbrengdatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        //[Compare(StartDate)] Nog opzoeken
        public DateTime EndDate { get; private set; }

        public List<ReservationDetail> Details { get; set; }

        public ReservationsViewModel(Reservation r)
        {
            Id = r.Id;
            CreationDate = r.CreationDate;
            StartDate = r.StartDate;
            EndDate = r.EndDate;
            Details = r.Details;
        }
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
            this.BroughtBackDate = detail.BroughtBackDate;
            this.PickUpDate = detail.PickUpDate;
            this.Material = new MaterialViewModel(detail.MaterialIdentifier.Material);
        }
    }
}