using IntusWindows.DAL.DataRepositories;
using IntusWindows.DAL.DataModels;
using System.Linq.Expressions;

namespace IntusWindows.DAL.DataServices
{
    public class SubElementService : ISubElementService
    {
        private readonly ISubElementRepository repository;

        public SubElementService(ISubElementRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<SubElement> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public IEnumerable<SubElement> GetAll(string includePropertyName)
        {
            var entities = repository.GetAll(includePropertyName);
            return entities;
        }
        public SubElement GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public SubElement Create(SubElement objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(SubElement objectToUpdate)
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

        public IEnumerable<SubElement> GetMany(Expression<Func<SubElement, bool>> where, string include = "")
        {
            var entities = repository.GetMany(where, include);
            return entities;
        }

        public void Delete(Expression<Func<SubElement, bool>> where)
        {
            repository.Delete(where);
            Save();
        }
        public SubElement Get(Expression<Func<SubElement, bool>> where, string include = "")
        {
            var entity = repository.Get(where, include);
            return entity;
        }
    }
}
