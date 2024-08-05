using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Retrieves an entity by its unique ID asynchronously
        Task<T> GetByIdAsync(int id);

        // Retrieves all entities asynchronously
        Task<IEnumerable<T>> GetAllAsync();

        // Adds a new entity to the repository asynchronously
        Task AddAsync(T entity);

        // Updates an existing entity in the repository asynchronously
        Task UpdateAsync(T entity);

        // Deletes an entity by its unique ID asynchronously
        Task DeleteAsync(int id);

        // Checks if an entity exists by its unique ID asynchronously
        Task<bool> ExistsAsync(int id);
    }
}
