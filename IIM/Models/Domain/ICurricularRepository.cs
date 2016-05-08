using System.Linq;

namespace IIM.Models.Domain
{
    public interface ICurricularRepository
    {
        IQueryable<Curricular> FindAll();
        Curricular FindById(int id);
        Curricular FindByName(string name);
    }
}
