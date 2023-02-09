using IntusWindows.DAL.ApplicationDbContext;
using IntusWindows.DAL.DataModels;
using IntusWindows.DAL.Infrastructure;

namespace IntusWindows.DAL.DataRepositories
{
    public class WindowRepository : RepositoryBaseCodeFirst<Window>, IWindowRepository
    {
        public WindowRepository(IntusWindowsDbContext dataContext)
            : base(dataContext)
        {

        }
    }
}
