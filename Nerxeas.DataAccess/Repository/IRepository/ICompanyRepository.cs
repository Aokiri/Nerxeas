using Nerxeas.Models;

namespace Nerxeas.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}
