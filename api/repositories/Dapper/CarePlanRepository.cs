using System.Data;
using Dapper;
using Gainwell.Attributes;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Repositories.Dapper;

[ModelName("carePlan")]
public class CarePlanRepository(DapperDbContext context) : RepositoryBase<CarePlanModel>(context) { 

    public override async Task<int> CreateAsync(CarePlanModel entity, int userId, int tenantId) {
        var interventionsTable = new DataTable();
        interventionsTable.Columns.Add("id", typeof(int));

        foreach (var intervention in entity.Interventions) {
            interventionsTable.Rows.Add(intervention.Id);
        }

        var barriersTable = new DataTable();
        barriersTable.Columns.Add("id", typeof(int));

        foreach (var barrier in entity.Barriers) {
            barriersTable.Rows.Add(barrier.Id);
        }

        var parameters = new DynamicParameters();
        parameters.Add("@memberId", entity.Member?.Id);
        parameters.Add("@problemId", entity.Problem?.Id);
        parameters.Add("@carePlanName", entity.CarePlanName);
        parameters.Add("@goalId", entity.CarePlanGoals[0].Goal?.Id);
        parameters.Add("@interventions", interventionsTable.AsTableParameter("idsTableType"));
        parameters.Add("@barriers", barriersTable.AsTableParameter("idsTableType"));
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        return await CreateAsync(parameters);
    }

    public async Task<int> AddGoalAsync(CarePlanGoalModel entity, int userId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@carePlanId", entity.CarePlan?.Id);
        parameters.Add("@goalId", entity.Goal?.Id);
        parameters.Add("@userId", userId);
        parameters.Add("@tenantId", tenantId);
        
        using var db = _context.CreateConnection();
        return await db.ExecuteStoredProcedureAsync("carePlan_goal_add", parameters);
    }

    public async Task<IEnumerable<CarePlanModel>> GetCarePlansAsync(int memberId) {
        var parameters = new DynamicParameters();
        parameters.Add("@memberId", memberId);

        using var db = _context.CreateConnection();
        List<DataTable> results = await db.ExecuteDataTablesAsync("carePlans_load", parameters);

        List<CarePlanModel> carePlans = [];

        DataTable carePlansTable = results[0];
        DataTable problemsTable = results[1];
        DataTable goalsTable = results[2];
        DataTable interventionsTable = results[3];
        DataTable barriersTable = results[4];

        foreach (DataRow row in carePlansTable.Rows) {
            var carePlan = new CarePlanModel {
                Id = row.Field<int>("carePlanId"),
                CarePlanName = row.Field<string>("carePlanName"),
                CarePlanGoals = [],
                Interventions = [],
                Barriers = []
            };

            var problemRow = problemsTable.Select($"carePlanId = {carePlan.Id}").FirstOrDefault();
            carePlan.Problem = new ProblemModel {
                Id = problemRow?.Field<int>("problemId"),
                Description = problemRow?.Field<string>("description")
            };

            var goalRows = goalsTable.Select($"carePlanId = {carePlan.Id}");
            foreach (DataRow goalRow in goalRows) {
                int carePlanGoalId = goalRow.Field<int>("carePlanGoalId");

                carePlan.CarePlanGoals.Add(new CarePlanGoalModel {
                    Id = carePlanGoalId,
                    Goal = new GoalModel {
                        Id = goalRow.Field<int>("goalId"),
                        Description = goalRow.Field<string>("description"),
                        GoalStatus = goalRow.Field<string>("goalStatus"),
                        DueDateString = goalRow.Field<DateTime?>("dueDate")?.ToString("MM/dd/yyyy hh:mm tt"),
                    }
                });

                var interventionRows = interventionsTable.Select($"carePlanGoalId = {carePlanGoalId}");
                foreach (DataRow interventionRow in interventionRows) {
                    carePlan.Interventions.Add(new InterventionModel {
                        Id = interventionRow.Field<int>("interventionId"),
                        Description = interventionRow.Field<string>("description")
                    });
                }

                var barrierRows = barriersTable.Select($"carePlanGoalId = {carePlanGoalId}");
                foreach (DataRow barrierRow in barrierRows) {
                    carePlan.Barriers.Add(new BarrierModel {
                        Id = barrierRow.Field<int>("barrierId"),
                        Description = barrierRow.Field<string>("description")
                    });
                }                
            }

            carePlans.Add(carePlan);
        }

        return carePlans; 
    }

    public override Task<int> DeactivateAsync(DynamicParameters parameters) {
        throw new NotImplementedException();
    }

    public override Task<int> ActivateAsync(DynamicParameters parameters) {
        throw new NotImplementedException();
    }

}
