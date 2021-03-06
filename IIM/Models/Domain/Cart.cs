﻿using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool AddMaterial(Material material)
        {
            if (Materials.Contains(material))
            {
                return false;
            }
            Materials.Add(material);
            return true;
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
            return Materials.Any(m => m.Id == id);
        }

        public Cart()
        {
            CreationDate = DateTime.Now;
        }
    }
}