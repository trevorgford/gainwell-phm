using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("outcome")]
public class OutcomeRepository(DapperDbContext context) : RepositoryBase<OutcomeModel>(context) {

    public override async Task<int> CreateAsync(OutcomeModel entity, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", entity.TenantId);
        parameters.Add("@code", entity.Code);
        parameters.Add("@description", entity.Description);
        return await CreateAsync(parameters);
    }

}