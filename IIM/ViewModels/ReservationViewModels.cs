using Foolproof;
using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using Type = IIM.Models.Domain.Type;

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

    public class ReservationDetailViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Terugbrengdatum")]
        public DateTime BroughtBackDate { get; private set; }
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Afhaaldatum")]
        public DateTime PickUpDate { get; private set; }
        public MaterialViewModel Material { get; private set; }
        public ReservationDetailViewModel(ReservationDetail detail)
        {
            if (detail.BroughtBackDate != null) this.BroughtBackDate = detail.BroughtBackDate.Value;
            if (detail.PickUpDate != null) this.PickUpDate = detail.PickUpDate.Value;
            this.Material = new MaterialViewModel(detail.MaterialIdentifier.Material);
        }
    }


    public class NewReservationViewModel
    {
        public ReservationDateRangeViewModel ReservationDateRange { get; set; }
        public NewReservationMaterialsViewModel ReservationMaterials { get; set; }

        public NewReservationViewModel(ReservationDateRangeViewModel reservationDateRange, NewReservationMaterialsViewModel reservationMaterials)
        {
            ReservationDateRange = reservationDateRange;
            ReservationMaterials = reservationMaterials;
        }

        public NewReservationViewModel()
        {

        }
    }

    public class NewReservationMaterialsViewModel
    {
        public IEnumerable<ReservationDetailSelectionViewModel> Materials { get; set; }

        public bool IsDisabled { get; set; }

        public NewReservationMaterialsViewModel() : this(null)
        {

        }
        public NewReservationMaterialsViewModel(IEnumerable<ReservationDetailSelectionViewModel> materials) : this(materials, true)
        {

        }

        public NewReservationMaterialsViewModel(IEnumerable<ReservationDetailSelectionViewModel> materials, bool isDisabled)
        {
            Materials = materials;
            IsDisabled = isDisabled;
        }
    }

    public class ReservationDateRangeViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DateFieldAmount { get; set; }
        public ReservationDateRangeViewModel(DateTime startDate, DateTime endDate, Type userType)
        {
            StartDate = startDate;
            EndDate = endDate;
            SetType(userType);
        }
        public ReservationDateRangeViewModel()
        {

        }

        public void SetType(Type userType)
        {
            switch (userType)
            {
                case Type.Staff:
                    DateFieldAmount = 2;
                    break;
                case Type.Student:
                    DateFieldAmount = 1;
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(userType), (int)userType, typeof(Type));
            }
        }
    }

    public class ReservationDetailSelectionViewModel
    {
        [Display(Name = "Maximaal aantal")]
        public int MaxAmount { get; private set; }
        [Display(Name = "Gewenst aantal")]
        public int RequestedAmount { get; set; }
        [Display()]
        public MaterialViewModel Material { get; private set; }
        public ReservationDetailSelectionViewModel(Material material, int maxAmount, int requestedAmount)
        {
            Material = new MaterialViewModel(material);
            MaxAmount = maxAmount;
            RequestedAmount = requestedAmount;
        }
    }

}