namespace IIM.Models.Domain
{
    public class MaterialIdentifier
    {

        public int Id { get; private set; }

        public string Place { get; private set; }

        public Visibility Visibility { get; private set; }

        public Material Material { get; private set; }
    }
}