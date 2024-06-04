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
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

    // public async Task<IEnumerable<AssessmentModel>> GetCurrentAssessments(int tenantId) {
    //     using var db = _context.CreateConnection();
    //     return await db.QueryStoredProcedureAsync<AssessmentModel>("assessments_current_load", new DynamicParameters(new { tenantId }));
    // }

    // public async Task<int> SetCurrent(AssessmentModel entity, int userId, int tenantId) {
    //     var parameters = new DynamicParameters();
    //     parameters.Add("@id", entity.Id);
    //     parameters.Add("@userId", userId);
    //     return await _context.CreateConnection().ExecuteStoredProcedureAsync("assessment_setCurrent", parameters);
    // }

}