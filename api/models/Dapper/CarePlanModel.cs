namespace Gainwell.Models.Dapper;

public class CarePlanModel : ModelBase {

    public MemberModel? Member { get; set; }
    public ProblemModel? Problem { get; set; }
    public List<CarePlanGoalModel> CarePlanGoals { get; set; } = [];
    public List<InterventionModel> Interventions { get; set; } = [];
    public List<BarrierModel> Barriers { get; set; } = [];
    public string? CarePlanName { get; set; }

}