using System.Data.Entity;
using System.Linq;
using IIM.Models.Domain;

namespace IIM.Models.DAL
{
    public class TargetGroupRepository : ITargetGroupRepository
    {
        private readonly DbSet<TargetGroup> _targetGroups;

        public TargetGroupRepository(IIMContext context)
        {
            _targetGroups = context.TargetGroups;
        }

        public IQueryable<TargetGroup> FindAll()
        {
            return _targetGroups;
        }

        public TargetGroup FindById(int id)
        {
            return _targetGroups.SingleOrDefault(c => c.Id == id);
        }

        public TargetGroup FindByName(string name)
        {
            return _targetGroups.SingleOrDefault(c => c.Name.Equals(name));
        }
    }
}