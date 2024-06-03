using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessment")]
public class AssessmentRepository(DapperDbContext context) : RepositoryBase<AssessmentModel>(context) {

    public override async Task<int> CreateAsync(AssessmentModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@description", entity.Description);
        parameters.Add("@code", entity.Code);
        parameters.Add("@versionName", entity.VersionName);
        parameters.Add("@isCurrent", entity.IsCurrent);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}