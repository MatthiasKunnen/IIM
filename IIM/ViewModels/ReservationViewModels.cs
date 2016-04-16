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
        public DateTime CreationDate { get; private set; }
        [Display(Name = "Ophaaldatum")]
        public DateTime StartDate { get; private set; }
        [Display(Name = "Terugbrengdatum")]
        //[Compare(StartDate)] Nog opzoeken
        public DateTime EndDate { get; private set; }

        public IEnumerable<ReservationDetail> Details { get; set; }

        public ReservationsViewModel(Reservation r)
        {
            Id = r.Id;
            CreationDate = r.CreationDate;
            StartDate = r.StartDate;
            EndDate = r.EndDate;
        }
    }



    public class ReservationDetailViewModel
    {
        public DateTime BroughtBackDate { get; private set; }
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