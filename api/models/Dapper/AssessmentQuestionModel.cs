namespace Gainwell.Models.Dapper;

public class AssessmentQuestionModel : ModelBase {
    
    public AssessmentSectionModel? Section { get; set; }
    public AssessmentQuestionTypeModel? QuestionType { get; set; }
    public AssessmentChoiceModel? ParentChoice { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; }
    public bool? Active { get; set; }
    public List<AssessmentChoiceModel> Choices { get; set; } = [];

}