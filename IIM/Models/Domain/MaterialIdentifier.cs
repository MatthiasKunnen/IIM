using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class MaterialIdentifier
    {
        private int? _materialId;

        public int Id { get; private set; }

        public int MaterialId
        {
            get { return _materialId ?? (_materialId = Material.Id).Value; }
            set { _materialId = value; }
        }

        public string Place { get; private set; }

        public Visibility Visibility { get; private set; }

        public virtual Material Material { get; private set; }

        public virtual List<ReservationDetail> ReservationDetails { get; private set; }

    }
}