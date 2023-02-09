using IntusWindows.DAL.DataRepositories;
using IntusWindows.DAL.DataModels;
using System.Linq.Expressions;

namespace IntusWindows.DAL.DataServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;

        public OrderService(IOrderRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Order> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public IEnumerable<Order> GetAll(string includePropertyName)
        {
            var entities = repository.GetAll(includePropertyName);
            return entities;
        }
        public Order GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public Order Create(Order objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Order objectToUpdate)
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

        public IEnumerable<Order> GetMany(Expression<Func<Order, bool>> where, string include = "")
        {
            var entities = repository.GetMany(where);
            return entities;
        }

        public void Delete(Expression<Func<Order, bool>> where)
        {
            repository.Delete(where);
            Save();
        }

        public Order Get(Expression<Func<Order, bool>> where, string include = "")
        {
            var entity = repository.Get(where, include);
            return entity;
        }
    }
}
