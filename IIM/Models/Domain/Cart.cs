using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIM.Models.Domain
{
    public class Cart
    {
        public int Id { get; private set; }
        public Dictionary<Material, int> Materials { get; private set; }
        public DateTime CreationDate { get; private set; }
        public void AddMaterial(Material material, int amount)
        {
            if (Materials.ContainsKey(material))
            {
                Materials[material] += amount;
            }
            else
            {
                Materials[material] = amount;
            }

        }
        public void RemoveMaterial(Material material, int amount)
        {
            if (Materials.ContainsKey(material))
            {
                Materials[material] -= amount;
            }

            if (Materials[material] <= 0)
            {
                Materials.Remove(material);
            }
        }
    }
}