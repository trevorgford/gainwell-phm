using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("intervention")]
public class InterventionRepository(DapperDbContext context) : RepositoryBase<InterventionModel>(context) {

    public override async Task<int> CreateAsync(InterventionModel entity, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", entity.TenantId);
        parameters.Add("@code", entity.Code);
        parameters.Add("@description", entity.Description);
        return await CreateAsync(parameters);
    }

}