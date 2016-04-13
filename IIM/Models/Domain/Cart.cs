using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIM.Models.Domain
{
    public class Cart
    {
        private List<Material> _materials;
        public int Id { get; private set; }
        public virtual List<Material> Materials
        {
            get { return _materials ?? (_materials = new List<Material>()); }
            private set { _materials = value; }
        }
        public DateTime CreationDate { get; private set; }
        public void AddMaterial(Material material)
        {
            Materials.Add(material);
        }
        public void RemoveMaterial(Material material)
        {
            Materials.Remove(material);
        }
<<<<<<< HEAD
        public Cart()
        {
            CreationDate = DateTime.Now;
        }
=======
        
>>>>>>> 9f451aff4606cd6edb317b5c6be6deb0b7885fe7
    }
}