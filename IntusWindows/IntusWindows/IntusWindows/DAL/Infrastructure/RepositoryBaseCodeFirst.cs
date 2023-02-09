using IntusWindows.DAL.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntusWindows.DAL.Infrastructure
{
    public abstract class RepositoryBaseCodeFirst<T>
        where T : class
    {
        private IntusWindowsDbContext _dataContext;
        private readonly DbSet<T> _dbset;
        protected RepositoryBaseCodeFirst(IntusWindowsDbContext dataContext)
        {
            _dataContext = dataContext;
            _dbset = DataContext.Set<T>();
        }

        public virtual void Commit()
        {
            DataContext.SaveChanges();
        }
        protected IntusWindowsDbContext DataContext
        {
            get { return _dataContext; }
        }

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateConditional(Expression<Func<T, bool>> where)
        {


        }



        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            _dbset.RemoveRange(objects);
            //foreach (T obj in objects)
            //    _dbset.Remove(obj);
        }
        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.ToList();
        }
        
        public virtual IEnumerable<T> GetAll(string include)
        {
                if (string.IsNullOrEmpty(include))
                    return _dbset.ToList();
                var properties = (typeof(T)).GetProperties();
                var includeProperty = properties.FirstOrDefault(x => x.Name.ToLower() == include.ToLower());
                if (includeProperty != null)
                {
                    return _dbset.Include(include).ToList();
                }
                return _dbset.ToList();
        }


        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string include = "")
        {

            return _dbset.Include(include).Where(where).ToList();
        }



        public T Get(Expression<Func<T, bool>> where, string include = "")
        {
            if (string.IsNullOrEmpty(include))
                return _dbset.FirstOrDefault<T>(where);
            var properties = (typeof(T)).GetProperties();
            var includeProperty = properties.FirstOrDefault(x => x.Name.ToLower() == include.ToLower());
            if (includeProperty != null)
            {
                return _dbset.Include(include).FirstOrDefault<T>(where);
            }
            return _dbset.FirstOrDefault<T>(where);
        }
    }
}
