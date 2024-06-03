using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("goalStatus")]
[ModelNamePlural("goalStatuses")]
public class GoalStatusRepository(DapperDbContext context) : RepositoryBase<GoalStatusModel>(context) {

    public override async Task<int> CreateAsync(GoalStatusModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@tenantId", tenantId);
        parameters.Add("@code", entity.Code);
        parameters.Add("@description", entity.Description);
        parameters.Add("@userId", userId);
        return await CreateAsync(parameters);
    }

}
