﻿using System;
using System.Collections.Generic;

namespace IIM.Models.Domain
{
    public class Material
    {
        
        public string Name { get; private set; }

        public string Description { get; private set; }

        public Firm Firm { get; private set; }

        public decimal Price { get; set; }

        public string ArticleNr { get; private set; }

        public int Id { get; private set; }

        public string Encoding { get; private set; }

        public List<Curricular> Curriculars { get; private set; }

        public List<TargetGroup> TargetGroups { get; private set; }

        public int Identifiers { get; private set; }
    }
}