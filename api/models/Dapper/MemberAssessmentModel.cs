namespace Gainwell.Models.Dapper;

public class MemberAssessmentModel : ModelBase {

    public required AssessmentModel Assessment { get; set; }
    public required MemberModel Member { get; set; }
    public DateTime? CreatedTimestamp { get; set; }
    public DateTime? ModifiedTimestamp { get; set; }
    public DateTime? CompletedTimestamp { get; set; }
    public List<MemberAssessmentAnswerModel>? Answers { get; set; }

}
