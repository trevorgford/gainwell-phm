using System.Data;
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

    public async Task<AssessmentModel> LoadFullAssessment(int assessmentId) {
        using var db = _context.CreateConnection();
        List<DataTable> results = await db.ExecuteDataTablesAsync("assessment_full_load", new DynamicParameters(new { assessmentId }));

        AssessmentModel assessment = new();

        DataTable assessmentTable = results[0];
        DataTable sectionsTable = results[1];
        DataTable questionsTable = results[2];
        DataTable choicesTable = results[3];

        foreach (DataRow row in assessmentTable.Rows) {
            assessment.Id = row.Field<int>("id");
            assessment.Description = row.Field<string>("description");
            assessment.VersionName = row.Field<string>("versionName");

            foreach (DataRow sectionRow in sectionsTable.Rows) {
                var sectionId = sectionRow.Field<int>("id");

                assessment.Sections.Add(new AssessmentSectionModel {
                    Id = sectionId,
                    Description = sectionRow.Field<string>("description"),
                    SortOrder = sectionRow.Field<int>("sortOrder")
                });

                var questionRows = questionsTable.Select($"sectionId = {sectionId}");
                foreach (DataRow questionRow in questionRows) {
                    var questionId = questionRow.Field<int>("id");

                    var question = new AssessmentQuestionModel {
                        Id = questionId,
                        Description = questionRow.Field<string>("description"),
                        SortOrder = questionRow.Field<int>("sortOrder"),
                        QuestionType = new AssessmentQuestionTypeModel {
                            Id = questionRow.Field<int>("questionTypeId")
                        },
                    };

                    var choiceRows = choicesTable.Select($"questionId = {questionId}");
                    foreach (DataRow choiceRow in choiceRows) {
                        question.Choices.Add(new AssessmentChoiceModel {
                            Id = choiceRow.Field<int>("id"),
                            Description = choiceRow.Field<string>("description"),
                            SortOrder = choiceRow.Field<int>("sortOrder"),
                            Score = choiceRow.Field<decimal?>("score")
                        });
                    }

                    assessment.Sections.Last().Questions.Add(question);
                }
            }
        }

        return assessment;
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