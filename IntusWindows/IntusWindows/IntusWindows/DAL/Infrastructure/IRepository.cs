using System.Linq.Expressions;

namespace IntusWindows.DAL.Infrastructure
{
    public interface IRepository<T>
        where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where, string include = "");
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string include);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string include = "");
        void Commit();
    }
}
