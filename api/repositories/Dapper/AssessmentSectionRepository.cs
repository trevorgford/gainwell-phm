using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentSection")]
public class AssessmentSectionRepository(DapperDbContext context) : RepositoryBase<AssessmentSectionModel>(context) {

    public override async Task<int> CreateAsync(AssessmentSectionModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@assessmentId", entity.Assessment?.Id);
        parameters.Add("@description", entity.Description);
        parameters.Add("@sortOrder", entity.SortOrder);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}