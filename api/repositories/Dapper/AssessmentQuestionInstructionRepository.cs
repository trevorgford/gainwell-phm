using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentQuestionInstruction")]
public class AssessmentQuestionInstructionRepository(DapperDbContext context) : RepositoryBase<AssessmentQuestionInstructionModel>(context) {

    public override async Task<int> CreateAsync(AssessmentQuestionInstructionModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@questionId", entity.Question?.Id);
        parameters.Add("@description", entity.Description);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}