using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("interventionStatus")]
[ModelNamePlural("interventionStatuses")]
public class InterventionStatusRepository(DapperDbContext context) : RepositoryBase<InterventionStatusModel>(context) {

    public override async Task<int> CreateAsync(InterventionStatusModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", tenantId);
        parameters.Add("@code", entity.Code);
        parameters.Add("@description", entity.Description);
        parameters.Add("@userId", userId);
        return await CreateAsync(parameters);
    }

}