using IntusWindows.DAL.ApplicationDbContext;
using IntusWindows.DAL.DataModels;
using IntusWindows.DAL.Infrastructure;

namespace IntusWindows.DAL.DataRepositories
{
    public class OrderRepository : RepositoryBaseCodeFirst<Order>, IOrderRepository
    {
        public OrderRepository(IntusWindowsDbContext dataContext)
            : base(dataContext)
        {

        }
    }
}