﻿using System;
using IIM.Models.Domain;
using IIM.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace IIM.Controllers
{
    public class InventoryController : Controller
    {
        private IMaterialRepository _materialRepository;
        public InventoryController(IMaterialRepository repository)
        {
            this._materialRepository = repository;
        }

        // GET
        public ActionResult Index(string searchName, string searchCurricular)
        {
            if (!String.IsNullOrEmpty(searchName) && String.IsNullOrEmpty(searchCurricular))
            {
                return View(_materialRepository
                .FindAll()
                .Where( m => m.Name.Contains(searchName) ||
                             m.Description.Contains(searchName))
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
            }
            if (String.IsNullOrEmpty(searchName) && !String.IsNullOrEmpty(searchCurricular))
            {
                return View(_materialRepository
                .FindAll()
                
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
            }
            if (!String.IsNullOrEmpty(searchName) && !String.IsNullOrEmpty(searchCurricular))
            {
                return View(_materialRepository
                .FindAll()
                .Where(m => m.Name.Contains(searchName) ||
                            m.Description.Contains(searchName))
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
            }

            return View(_materialRepository
                .FindAll()
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
        }
    }
}