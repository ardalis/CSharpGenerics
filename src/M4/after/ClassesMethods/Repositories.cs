using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseEntity { }
    public interface IAggregateRoot { }
    public class Customer : BaseEntity, IAggregateRoot
    {
        internal void Add(Order order)
        {
            throw new NotImplementedException();
        }
    }
    public class Order : BaseEntity, IAggregateRoot { }
    public interface ISpecification<T> { } // see Ardalis.Specification nuget package

    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, IAggregateRoot;
        Task<List<T>> ListAsync<T>() where T : BaseEntity, IAggregateRoot;
        Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
        Task UpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
        Task DeleteAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
    }

    public class SomeController
    {
        private readonly IRepository _repo;

        public SomeController(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Customer>> ListCustomers()
        {
            return await _repo.ListAsync<Customer>();
        }
        public async Task<IEnumerable<Order>> ListOrders()
        {
            return await _repo.ListAsync<Order>();
        }

        public async Task AddOrderToCustomer(int orderId, int customerId)
        {
            var order = await _repo.GetByIdAsync<Order>(orderId);
            var customer = await _repo.GetByIdAsync<Customer>(customerId);
            customer.Add(order);
            await _repo.UpdateAsync(customer); // type inferred
        }
    }
}