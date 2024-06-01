using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

public class RepositoryBase<T>: IRepository, IRepository<T> where T : ModelBase{

    protected readonly DapperDbContext _context;

    public RepositoryBase(DapperDbContext context) {
        _context = context;

        Type type = GetType();
        Attribute? attribute = Attribute.GetCustomAttribute(type, typeof(ModelNameAttribute));
        if(attribute == null) return;
        ModelNameAttribute modelNameAttribute = (ModelNameAttribute)attribute;

        attribute = Attribute.GetCustomAttribute(type, typeof(ModelNamePluralAttribute));
        ModelNamePluralAttribute? modelNamePluralAttribute = attribute != null ? (ModelNamePluralAttribute)attribute : null;

        EntityName = modelNameAttribute.Name;
        EntityPluralName = modelNamePluralAttribute != null ? modelNamePluralAttribute.PluralName : $"{EntityName}s";
        GetAllStoredProcedure = $"{EntityPluralName}_load";
        GetByIdStoredProcedure = $"{EntityName}_load";
        CreateStoredProcedure = $"{EntityName}_create";
        DeleteStoredProcedure = $"{EntityName}_delete";
        ToggleActiveStoredProcedure = $"{EntityName}_active_toggle";
    }

    protected virtual string? EntityName { get; set; }
    protected virtual string? EntityPluralName { get; set; }

    protected virtual string? GetAllStoredProcedure { get; set; }
    protected virtual string? GetByIdStoredProcedure { get; set; }
    protected virtual string? CreateStoredProcedure { get; set; }
    protected virtual string? DeleteStoredProcedure { get; set; }
    protected virtual string? ToggleActiveStoredProcedure { get; set; }

    public virtual async Task<IEnumerable<T>> GetAllAsync() {
        if (string.IsNullOrEmpty(GetAllStoredProcedure)) throw new InvalidOperationException("GetAllStoredProcedure is not set");
        using var db = _context.CreateConnection();
        return await db.QueryStoredProcedureAsync<T>(GetAllStoredProcedure);
    }

    public virtual async Task<T?> GetByIdAsync(int id) {
        if(string.IsNullOrEmpty(GetByIdStoredProcedure)) throw new InvalidOperationException("GetByIdStoredProcedure is not set");
        using var db = _context.CreateConnection();
        var result = await db.QueryStoredProcedureAsync<T>(GetByIdStoredProcedure, new DynamicParameters(new { id }));
        return result.Any() ? result.First() : default;
    }

    public virtual async Task<int> CreateAsync(DynamicParameters? parameters) {
        if(string.IsNullOrEmpty(CreateStoredProcedure)) throw new InvalidOperationException("CreateStoredProcedure is not set");
        using var db = _context.CreateConnection();
        return await db.ExecuteStoredProcedureAsync(CreateStoredProcedure, parameters);
    }

    public virtual async Task<int> DeleteAsync(DynamicParameters? parameters) {
        if(string.IsNullOrEmpty(DeleteStoredProcedure)) throw new InvalidOperationException("DeleteStoredProcedure is not set");
        using var db = _context.CreateConnection();
        return await db.ExecuteStoredProcedureAsync(DeleteStoredProcedure, parameters);
    }

    public virtual async Task<int> DeactivateAsync(DynamicParameters parameters) {
        if(string.IsNullOrEmpty(ToggleActiveStoredProcedure)) throw new InvalidOperationException("ToggleActiveStoredProcedure is not set");
        using var db = _context.CreateConnection();
        parameters.Add("@active", false);
        return await db.ExecuteStoredProcedureAsync(ToggleActiveStoredProcedure, parameters);
    }

    public virtual async Task<int> ActivateAsync(DynamicParameters parameters) {
        if(string.IsNullOrEmpty(ToggleActiveStoredProcedure)) throw new InvalidOperationException("ToggleActiveStoredProcedure is not set");
        using var db = _context.CreateConnection();
        parameters.Add("@active", true);
        return await db.ExecuteStoredProcedureAsync(ToggleActiveStoredProcedure, parameters);
    }

    public virtual Task<int> CreateAsync(T entity, int userId) {
        throw new NotImplementedException();
    }

}
