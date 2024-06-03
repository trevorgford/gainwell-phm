using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentSectionInstruction")]
public class AssessmentSectionInstructionRepository(DapperDbContext context) : RepositoryBase<AssessmentSectionInstructionModel>(context) {

    public override async Task<int> CreateAsync(AssessmentSectionInstructionModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@sectionId", entity.Section?.Id);
        parameters.Add("@description", entity.Description);
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}