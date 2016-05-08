using Foolproof;
using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.WebPages;
using Type = IIM.Models.Domain.Type;

namespace IIM.ViewModels.ReservationViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Aanmaakdatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Ophaaldatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [LessThan("EndDate")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Terugbrengdatum")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [GreaterThan("StartDate")]
        public DateTime EndDate { get; set; }
        public bool Status { get; private set; }
        public bool EverythingHere { get; private set; }

        public List<ReservationDetailViewModel> Details { get; set; }

        public ReservationViewModel(Reservation r)
        {
            Id = r.Id;
            CreationDate = r.CreationDate;
            StartDate = r.StartDate;
            EndDate = r.EndDate;
            Details = r.Details.Select(d => new ReservationDetailViewModel(d)).ToList();
            Status = r.IsCompleted();
            EverythingHere = r.IsEverythingHere();
        }
    }

    public class ReservationDetailViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Terugbrengdatum")]
        public DateTime? BroughtBackDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Display(Name = "Afhaaldatum")]
        public DateTime? PickUpDate { get; set; }
        public MaterialViewModel Material { get; set; }
        public MaterialIdentifier MaterialIdentifier { get; set; }
        public int ReservationId { get; set; }
        public ReservationDetailViewModel(ReservationDetail detail)
        {
            if (detail.BroughtBackDate.HasValue) BroughtBackDate = detail.BroughtBackDate.Value;
            if (detail.PickUpDate.HasValue) PickUpDate = detail.PickUpDate.Value;
            Material = new MaterialViewModel(detail.MaterialIdentifier.Material);
            MaterialIdentifier = detail.MaterialIdentifier;
            ReservationId = detail.Reservation.Id;
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
        private Type _userType;
        private bool _endDateHasError;
        private bool _startDateHasError;

        public int DateFieldAmount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public string EndDateError { get; set; }

        public bool EndDateHasError
        {
            get { return _endDateHasError || (!EndDateError?.IsEmpty() ?? false); }
            set { _endDateHasError = value; }
        }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        public string StartDateError { get; set; }

        public bool StartDateHasError
        {
            get { return _startDateHasError || (!StartDateError?.IsEmpty() ?? false); }
            set { _startDateHasError = value; }
        }

        public Type UserType
        {
            get { return _userType; }
            set
            {
                switch (value)
                {
                    case Type.Staff:
                        DateFieldAmount = 2;
                        break;
                    case Type.Student:
                        DateFieldAmount = 1;
                        break;
                    default:
                        throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(Type));
                }
                _userType = value;
            }
        }

        public ReservationDateRangeViewModel(DateTime startDate, DateTime endDate, Type userType)
        {
            StartDate = startDate;
            EndDate = endDate;
            UserType = userType;
        }
        public ReservationDateRangeViewModel()
        {
        }
    }

    public class ReservationDetailSelectionViewModel
    {
        [Display(Name = "Maximaal aantal")]
        public int MaxAmount { get; set; }
        [Display(Name = "Gewenst aantal")]
        public int RequestedAmount { get; set; }
        [Display()]
        public MaterialViewModel Material { get; set; }
        public ReservationDetailSelectionViewModel(Material material, int maxAmount, int requestedAmount)
        {
            Material = new MaterialViewModel(material);
            MaxAmount = maxAmount;
            RequestedAmount = requestedAmount;
        }

        public ReservationDetailSelectionViewModel()
        {

        }
    }

}