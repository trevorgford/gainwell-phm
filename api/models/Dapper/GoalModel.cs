namespace Gainwell.Models.Dapper;

public class GoalModel : TenantCodedModelBase {

    public string? GoalStatus { get; set; }

    public GoalStatusModel? Status { get; set; }

    public string? DueDateString { get; set; }

}
