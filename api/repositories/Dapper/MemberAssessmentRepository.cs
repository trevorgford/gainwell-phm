using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;


[ModelName("memberAssessment")]
public class MemberAssessmentRepository(DapperDbContext context) : RepositoryBase<MemberAssessmentModel>(context) {

    public override async Task<int> CreateAsync(MemberAssessmentModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@assessmentId", entity.Assessment.Id);
        parameters.Add("@memberId", entity.Member.Id);
        parameters.Add("@userId", userId);
        return await CreateAsync(parameters);
    }

    public override async Task<MemberAssessmentModel?> GetByIdAsync(int id) {
        if(string.IsNullOrEmpty(GetByIdStoredProcedure)) throw new InvalidOperationException("GetByIdStoredProcedure is not set");
        using var db = _context.CreateConnection();
        List<DataTable> result = await db.ExecuteDataTablesAsync(GetByIdStoredProcedure, new DynamicParameters(new { id }));

        DataTable assessmentTable = result[0];
        DataTable answersTable = result[1];

        MemberAssessmentModel memberAssessment = new() {
            Id = assessmentTable.Rows[0].Field<int>("id"),
            CreatedTimestamp = assessmentTable.Rows[0].Field<DateTime?>("createdTimestamp"),
            ModifiedTimestamp = assessmentTable.Rows[0].Field<DateTime?>("modifiedTimestamp"),
            CompletedTimestamp = assessmentTable.Rows[0].Field<DateTime?>("completedTimestamp"),
            Assessment = new() { Id = assessmentTable.Rows[0].Field<int>("assessmentId") }, 
            Member = new() { Id = assessmentTable.Rows[0].Field<int>("memberId") },
            Answers = []
        };

        foreach (DataRow row in answersTable.Rows) {
            var answer = new MemberAssessmentAnswerModel {
                Id = row.Field<int>("id"),
                MemberAssessmentId = memberAssessment.Id,
                Question = new() { Id = row.Field<int>("questionId") },
                Choice = new() { Id = row.Field<int>("choiceId") },
                AnswerText = row.Field<string>("answerText")
            };

            memberAssessment.Answers.Add(answer);
        }

        return memberAssessment;
    }

    public async Task<IEnumerable<MemberAssessmentModel>> LoadMemberAssessments(int memberId) {
        using var db = _context.CreateConnection();
        List<DataTable> results = await db.ExecuteDataTablesAsync("memberAssessments_load", new DynamicParameters(new { memberId }));

        List<MemberAssessmentModel> assessments = [];

        DataTable memberAssessmentsTable = results[0];

        foreach (DataRow row in memberAssessmentsTable.Rows) {
            var assessmentId = row.Field<int>("assessmentId");

            var assessment = new AssessmentModel {
                Id = assessmentId,
                Description = row.Field<string>("description"),
                VersionName = row.Field<string>("versionName")
            };

            var member = new MemberModel {
                Id = row.Field<int>("memberId")
            };

            var memberAssessment = new MemberAssessmentModel {
                Id = row.Field<int>("id"),
                Assessment = assessment,
                Member = member,
                CreatedTimestamp = row.Field<DateTime?>("createdTimestamp"),
                ModifiedTimestamp = row.Field<DateTime?>("modifiedTimestamp"),
                CompletedTimestamp = row.Field<DateTime?>("completedTimestamp")
            };

            assessments.Add(memberAssessment);
        }

        return assessments; 
    }

    public async Task<int> SaveAnswers(int memberAssessmentId, List<MemberAssessmentAnswerModel> answers, int userId) {
        if(answers == null || answers.Count == 0) return 0;

        var answersTable = new DataTable();
        answersTable.Columns.Add("questionId", typeof(int));
        answersTable.Columns.Add("choiceId", typeof(int));
        answersTable.Columns.Add("answerText", typeof(string));

        foreach (var answer in answers) {
            answersTable.Rows.Add(answer.Question?.Id, answer.Choice?.Id, answer.AnswerText);
        }

        var parameters = new DynamicParameters();
        parameters.Add("@id", memberAssessmentId);
        parameters.Add("@answers", answersTable.AsTableParameter("selectedAnswersTableType"));
        parameters.Add("@userId", userId);

        using var db = _context.CreateConnection();
        return await db.ExecuteStoredProcedureAsync("memberAssessment_answers_save", parameters);
    }

}