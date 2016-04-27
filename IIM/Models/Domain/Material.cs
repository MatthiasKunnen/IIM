using System;
using System.Collections.Generic;
using System.Linq;

namespace IIM.Models.Domain
{
    public class Material
    {
        public string ArticleNr { get; private set; }
        public virtual List<Curricular> Curriculars { get; private set; }
        public string Description { get; private set; }
        public string Encoding { get; private set; }
        public virtual Firm Firm { get; private set; }
        public int Id { get; private set; }
        public virtual ICollection<MaterialIdentifier> Identifiers { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public virtual List<TargetGroup> TargetGroups { get; private set; }

        public IEnumerable<MaterialIdentifier> GetAvailableIdentifiers(DateTime startDate, DateTime endDate)
        {
            return Identifiers.Where(i => i.IsAvailable(startDate, endDate,Type.Student));
        }

        public int GetAvailableIdentifierCount(DateTime startDate, DateTime endDate, Type userType)
        {
                return Identifiers.Count(i => i.IsAvailable(startDate, endDate, userType));
        }


        internal IEnumerable<Reservation> GetReservationRange(DateTime startDate, DateTime endDate)
        {
            return Identifiers.SelectMany(i => i.GetDetailRange(startDate, endDate).Select(d => d.Reservation));
        }
    }
}