using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Gainwell.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/members/{memberId}/careplans")]
[ApiController]
[RequiresTenant(true)]
public class CarePlanController(IRepository<CarePlanModel> repository) : BaseController<CarePlanModel>(repository) {

    private readonly IRepository<CarePlanModel> _repository = repository;

    [HttpGet("load")]
    [SwaggerOperation(Summary = "Gets a member's care plans")]
    public async Task<ActionResult<IEnumerable<CarePlanModel>>> GetCarePlans(int memberId) {
        var result = await ((CarePlanRepository)_repository).GetCarePlansAsync(memberId);
        return Ok(result);
    }

    [HttpPost("addGoal")]
    [SwaggerOperation(Summary = "Adds a goal to a care plan")]
    public async Task<ActionResult<int>> AddGoal(CarePlanGoalModel entity) {
        var result = await ((CarePlanRepository)_repository).AddGoalAsync(entity, UserId, TenantId);
        return Ok(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<CarePlanModel>>> GetAll() {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<CarePlanModel>> GetById(int id) {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<int>> Deactivate(int id) {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<int>> Activate(int id) {
        throw new NotImplementedException();
    }

}
