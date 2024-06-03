namespace Gainwell.Models.Dapper;

public class CarePlanCreateModel {

    public int? MemberId { get; set; }
    public int? ProblemId { get; set; }
    public int? GoalId { get; set; }
    public List<InterventionModel> Interventions { get; set; } = [];
    public List<BarrierModel> Barriers { get; set; } = [];
    public string? CarePlanName { get; set; }

}