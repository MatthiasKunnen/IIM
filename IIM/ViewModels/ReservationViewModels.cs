using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIM.ViewModels.ReservationViewModels
{
    public class ReservationsViewModel
    {   
        public int Id { get; private set; }
        //attributen maken vo algemene shit over de reservatie
        //pieter deze lijst gaje moeten opvullen in de controller xxx 
        public IEnumerable<ReservationDetail> Details { get; set; }
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