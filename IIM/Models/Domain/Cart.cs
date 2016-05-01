using System;
using System.Collections.Generic;
using System.Linq;

namespace IIM.Models.Domain
{
    public class Cart
    {
        private List<Material> _materials;
        public int Id { get; private set; }

        public string UserId { get; set; }

        public virtual List<Material> Materials
        {
            get { return _materials ?? (_materials = new List<Material>()); }
            private set { _materials = value; }
        }
        public DateTime CreationDate { get; private set; }
        public bool AddMaterial(Material material)
        {
            if (Materials.Contains(material))
            {
                return false;
            }
            else
            {
                Materials.Add(material);
                return true;
            }
        }

        public bool RemoveMaterial(Material material)
        {
            return Materials.Remove(material);
        }

        public Material GetMaterial(int id)
        {
            return Materials.First(m => m.Id == id);
        }

        public bool AlreadyInCart(int id)
        {
            return Materials.FirstOrDefault(m => m.Id.Equals(id)) != null;
        }

        public void Clear()
        {
            Materials.Clear();
            Materials = null;
        }

        public Cart()
        {
            CreationDate = DateTime.Now;
        }
    }
}