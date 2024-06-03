namespace Gainwell.Models.Dapper;

public class AssessmentChoiceModel : ModelBase {

    public AssessmentQuestionModel? Question { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
    public decimal? Score { get; set; }
    public bool? Active { get; set; }

}