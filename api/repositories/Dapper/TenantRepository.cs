using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("tenant")]
public class TenantRepository(DapperDbContext context) : RepositoryBase<TenantModel>(context) {

    public override async Task<int> CreateAsync(TenantModel entity, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@code", entity.Code);
        parameters.Add("@description", entity.Description);
        parameters.Add("@userId", userId);
        return await CreateAsync(parameters);
    }

}