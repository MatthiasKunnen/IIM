using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIM.Models.Domain
{
    public class Cart
    {
        public int Id { get; private set; }
        public List<Material> Materials { get; private set; }
        public DateTime CreationDate { get; private set; }
        public void AddMaterial(Material material)
        {
            Materials.Add(material);

        }
        public void RemoveMaterial(Material material)
        {
            Materials.Remove(material);
        }
    }
}