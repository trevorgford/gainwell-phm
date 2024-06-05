using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("assessmentQuestion")]
public class AssessmentQuestionRepository(DapperDbContext context) : RepositoryBase<AssessmentQuestionModel>(context) {

    public override async Task<int> CreateAsync(AssessmentQuestionModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@sectionId", entity.Section?.Id);
        parameters.Add("@questionTypeId", entity.QuestionType?.Id);
        parameters.Add("@parentChoiceId", entity.ParentChoice?.Id);
        parameters.Add("@description", entity.Description);
        parameters.Add("@sortOrder", entity.SortOrder);
        parameters.Add("@userId", userId);
        return await CreateAsync(parameters);
    }

    public async Task<IEnumerable<AssessmentQuestionModel>> GetAssessmentQuestions(int assessmentId) {
        using var db = _context.CreateConnection();
        return await db.QueryStoredProcedureAsync<AssessmentQuestionModel>("assessmentQuestions_load", new DynamicParameters(new { assessmentId }));
    }

    public async Task<IEnumerable<AssessmentQuestionModel>> GetAssessmentQuestionsBySection(int sectionId) {
        using var db = _context.CreateConnection();
        return await db.QueryStoredProcedureAsync<AssessmentQuestionModel>("assessmentQuestions_bySection_load", new DynamicParameters(new { sectionId }));
    }

}