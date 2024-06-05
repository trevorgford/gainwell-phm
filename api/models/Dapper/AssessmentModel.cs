namespace Gainwell.Models.Dapper;

public class AssessmentModel : TenantCodedModelBase {

    public string? VersionName { get; set; }
    public bool? IsCurrent { get; set; }
    public List<AssessmentSectionModel> Sections { get; set; } = [];

}