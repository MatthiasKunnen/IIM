﻿using System.Linq;

namespace IIM.Models.Domain
{
    public interface IMaterialRepository
    {
        Material FindByName(string name);
        IQueryable<Material> FindAll();
        Material FindById(int id);
        void SaveChanges();
    }
}
