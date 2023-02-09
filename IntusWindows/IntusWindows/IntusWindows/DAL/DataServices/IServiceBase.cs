using System.Linq.Expressions;

namespace IntusWindows.DAL.DataServices
{
    public interface IServiceBase<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string includePropertyName);
        T GetById(int id);
        T Create(T objectToCreate);
        void Update(T objectToUpdate);
        void Delete(int id);
        void Save();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string include = "");
        void Delete(Expression<Func<T, bool>> where);
        T Get(Expression<Func<T, bool>> where, string include = "");
    }
}
