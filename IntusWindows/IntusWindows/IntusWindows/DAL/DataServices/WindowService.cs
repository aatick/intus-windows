using IntusWindows.DAL.DataRepositories;
using IntusWindows.DAL.DataModels;
using System.Linq.Expressions;

namespace IntusWindows.DAL.DataServices
{
    public class WindowService : IWindowService
    {
        private readonly IWindowRepository repository;

        public WindowService(IWindowRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Window> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public IEnumerable<Window> GetAll(string includePropertyName)
        {
            var entities = repository.GetAll(includePropertyName);
            return entities;
        }
        public Window GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public Window Create(Window objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Window objectToUpdate)
        {
            repository.Update(objectToUpdate);
            Save();
        }
        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            repository.Delete(entity);
            Save();
        }

        public IEnumerable<Window> GetMany(Expression<Func<Window, bool>> where, string include = "")
        {
            var entities = repository.GetMany(where, include);
            return entities;
        }

        public void Delete(Expression<Func<Window, bool>> where)
        {
            repository.Delete(where);
            Save();
        }
        public Window Get(Expression<Func<Window, bool>> where, string include = "")
        {
            var entity = repository.Get(where, include);
            return entity;
        }
    }
}
