namespace IIM.Models.Domain
{
    public class User
    {
        public int Id { get; private set; }

        public string Email {
            get { return Email; }
            set {  }
        }

        public string Faculty { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string TelNumber { get; set; }

        public Type Type { get; private set; }
    }
}