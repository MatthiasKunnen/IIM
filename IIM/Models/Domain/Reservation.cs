﻿using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class Reservation : IReservationManager
    {
        private readonly IReservationManager _reservationManager;
        private DateTime _startDateTime;

        protected Reservation()
        {
        }

        public Reservation(DateTime creationDate, DateTime startDate, DateTime endDate, ApplicationUser user)
        {
            Details = new List<ReservationDetail>();
            CreationDate = creationDate;
            StartDate = startDate;
            EndDate = endDate;
            User = user;
            _reservationManager = TypeManagerFactory.CreateTypeManager(user.Type);
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public DateTime StartDate { get; private set; }
    

        public DateTime EndDate { get; private set; }

        public ApplicationUser User { get; private set; }


        public virtual List<ReservationDetail> Details { get; }

        public void AddDetail(ReservationDetail detail)
        {
            Details.Add(detail);
        }

        public void AddAllDetails(List<ReservationDetail> details)
        {
            Details.AddRange(details);
        }

        public void RemoveDetail(ReservationDetail detail)
        {
            Details.Remove(detail);
        }

        public void RemoveAllDetails(List<ReservationDetail> details)
        {
            details.ForEach(d => details.Remove(d));
        }

        //WIP
        public void AddCart(Cart cart, IReservationRepository reservationRepository)
        {
            List<MaterialIdentifier> identifiers = new List<MaterialIdentifier>();

            foreach (Material m in cart.Materials)
            {
                //identifiers.AddRange(reservationRepository.GetAvailableIdentifiers(this.StartDate,this.EndDate,cart.Materials[m],m));
            }

            AddAllDetails(identifiers.ConvertAll<ReservationDetail>(m => new ReservationDetail(this, m)));

        }

        public List<ReservationDetail> GetOverridableIdentifiers(List<ReservationDetail> reservationDetails)
        {
            return _reservationManager.GetOverridableIdentifiers(reservationDetails);
        }
    }
}