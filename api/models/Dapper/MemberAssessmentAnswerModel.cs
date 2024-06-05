namespace Gainwell.Models.Dapper;

public class MemberAssessmentAnswerModel : ModelBase {

    public int? MemberAssessmentId { get; set; }
    public AssessmentQuestionModel? Question { get; set; }
    public AssessmentChoiceModel? Choice { get; set; }
    public string? AnswerText { get; set; }
    public DateTime? CreatedTimestamp { get; set; }
    public DateTime? ModifiedTimestamp { get; set; }

}