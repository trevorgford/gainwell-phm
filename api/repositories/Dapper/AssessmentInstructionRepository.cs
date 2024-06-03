using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentInstruction")]
public class AssessmentInstructionRepository(DapperDbContext context) : RepositoryBase<AssessmentInstructionModel>(context) {

    public override async Task<int> CreateAsync(AssessmentInstructionModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@assessmentId", entity.Assessment?.Id);
        parameters.Add("@description", entity.Description);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}