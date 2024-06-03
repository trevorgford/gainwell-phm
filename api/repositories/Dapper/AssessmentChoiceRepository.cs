using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentChoice")]
public class AssessmentChoiceRepository(DapperDbContext context) : RepositoryBase<AssessmentChoiceModel>(context) {

    public override async Task<int> CreateAsync(AssessmentChoiceModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@questionId", entity.Question?.Id);
        parameters.Add("@description", entity.Description);
        parameters.Add("@sortOrder", entity.SortOrder);
        parameters.Add("@score", entity.Score);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}