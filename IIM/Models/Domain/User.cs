namespace IIM.Models.Domain
{
    using System.Linq;
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; private set; }

        public string Email
        {
            get { return Email; }
            set { }
        }

        public string Faculty { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string TelNumber { get; set; }

        public Type Type { get; private set; }

        public Cart WishList { get; private set; }

        public virtual List<Reservation> Reservations { get; private set; }

        public void AddReservation(Reservation r)
        {
            Reservations.Add(r);
        }

        public void AddReservations(ICollection<Reservation> r)
        {
            Reservations.AddRange(r);
        }

        public void RemoveReservation(Reservation r)
        {
            Reservations.Remove(r);
        }

        public void RemoveReservations(List<Reservation> r)
        {
            r.ForEach(res => Reservations.Remove(res));
        }
    }
}