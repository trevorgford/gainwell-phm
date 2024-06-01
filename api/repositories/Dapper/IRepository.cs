using Dapper;

namespace Gainwell.Repositories.Dapper;

public interface IRepository {}

public interface IRepository<T> { 

    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<int> CreateAsync(T entity, int userId);

    Task<int> DeactivateAsync(DynamicParameters parameters);

    Task<int> ActivateAsync(DynamicParameters parameters);

}
