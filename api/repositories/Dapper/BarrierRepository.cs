using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("barrier")]
public class BarrierRepository(DapperDbContext context) : RepositoryBase<BarrierModel>(context) {

    public override async Task<int> CreateAsync(BarrierModel entity, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", entity.TenantId);
        parameters.Add("@code", entity.Code);
        parameters.Add("@description", entity.Description);
        return await CreateAsync(parameters);
    }

}