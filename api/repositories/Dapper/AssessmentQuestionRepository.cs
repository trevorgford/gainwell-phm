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
        parameters.Add("@active", entity.Active);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

}