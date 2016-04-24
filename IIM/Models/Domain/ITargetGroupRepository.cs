using System.Linq;

namespace IIM.Models.Domain
{
    public interface ITargetGroupRepository
    {
        IQueryable<TargetGroup> FindAll();
        TargetGroup FindById(int id);
        TargetGroup FindByName(string name);
    }
}
