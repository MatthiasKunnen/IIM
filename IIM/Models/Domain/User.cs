namespace IIM.Models.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Faculty { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TelNumber { get; set; }

        public Type Type { get; set; }
    }
}