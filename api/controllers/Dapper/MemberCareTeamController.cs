using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/members/{memberId}/careteam")]
[ApiController]
public class MemberCareTeamController(IRepository<MemberCareTeamModel> repository) : BaseController<MemberCareTeamModel>(repository) {

    private readonly IRepository<MemberCareTeamModel> _repository = repository;

    [HttpPost("addPerson")]
    [SwaggerOperation(Summary = "Adds a person to a member's care team")]
    public async Task<IActionResult> AddPersonToCareTeamAsync([FromBody] MemberCareTeamPersonModel careTeam) {
        var result = await ((MemberCareTeamRepository)_repository).AddPersonToCareTeamAsync(careTeam.MemberId, careTeam.FirstName, careTeam.LastName, careTeam.Title, TenantId);
        return Ok(result);
    }

    [HttpPost("removePerson")]
    [SwaggerOperation(Summary = "Removes a person from a member's care team")]
    public async Task<IActionResult> RemovePersonFromCareTeamAsync([FromBody] MemberCareTeamPersonModel careTeam) {
        var result = await ((MemberCareTeamRepository)_repository).RemovePersonFromCareTeamAsync(careTeam.MemberId, careTeam.PersonId);
        return Ok(result);
    }

    [HttpGet("load")]
    [SwaggerOperation(Summary = "Gets a member's care team")]
    public async Task<ActionResult<IEnumerable<MemberCareTeamModel>>> GetCareTeam(int memberId) {
        var result = await ((MemberCareTeamRepository)_repository).GetCareTeamAsync(memberId);
        return Ok(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<MemberCareTeamModel>>> GetAll() {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<MemberCareTeamModel>> GetById(int id) {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<int>> Create(MemberCareTeamModel entity) {
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
