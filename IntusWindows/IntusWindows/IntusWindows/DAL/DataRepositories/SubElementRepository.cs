using IntusWindows.DAL.ApplicationDbContext;
using IntusWindows.DAL.DataModels;
using IntusWindows.DAL.Infrastructure;

namespace IntusWindows.DAL.DataRepositories
{
    public class SubElementRepository : RepositoryBaseCodeFirst<SubElement>, ISubElementRepository
    {
        public SubElementRepository(IntusWindowsDbContext dataContext)
            : base(dataContext)
        {

        }
    }
}
