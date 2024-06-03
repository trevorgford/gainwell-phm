namespace Gainwell.Models.Dapper;

public class AssessmentSectionModel : ModelBase {

    public AssessmentModel? Assessment { get; set; }
    public string? Description { get; set; }
    public int? SortOrder { get; set; } 
    public bool? Active { get; set; }

}