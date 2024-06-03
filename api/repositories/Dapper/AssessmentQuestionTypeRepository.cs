using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentQuestionType")]
public class AssessmentQuestionTypeRepository(DapperDbContext context) : RepositoryBase<AssessmentQuestionTypeModel>(context) {

    public override async Task<int> CreateAsync(AssessmentQuestionTypeModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@description", entity.Description);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}