namespace Gainwell.Models.Dapper;

public class CarePlanGoalModel : ModelBase {

    public required GoalModel Goal { get; set; }

    public CarePlanModel? CarePlan { get; set; }

}
