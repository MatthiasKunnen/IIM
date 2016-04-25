using System.Data.Entity;
using System.Linq;
using IIM.Models.Domain;

namespace IIM.Models.DAL
{
    public class CurricularRepository : ICurricularRepository
    {
        private readonly DbSet<Curricular> _curriculars;

        public CurricularRepository(IIMContext context)
        {
            _curriculars = context.Curriculars;
        }

        public IQueryable<Curricular> FindAll()
        {
            return _curriculars;
        }

        public Curricular FindById(int id)
        {
            return _curriculars.SingleOrDefault(c => c.Id == id);
        }

        public Curricular FindByName(string name)
        {
            return _curriculars.SingleOrDefault(c => c.Name.Equals(name));
        }
    }
}