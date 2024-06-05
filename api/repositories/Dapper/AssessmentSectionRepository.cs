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
        parameters.Add("@userId", userId);
        return await CreateAsync(parameters);
    }

    public async Task<IEnumerable<AssessmentSectionModel>> GetAssessmentSections(int assessmentId) {
        using var db = _context.CreateConnection();
        return await db.QueryStoredProcedureAsync<AssessmentSectionModel>("assessmentSections_load", new DynamicParameters(new { assessmentId }));
    }

}