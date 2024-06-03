using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/members")]
[ApiController]
public class MemberController(IRepository<MemberModel> repository) : BaseController<MemberModel>(repository) {

    private readonly IRepository<MemberModel> _repository = repository;

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Searches for members")]
    public async Task<ActionResult<IEnumerable<MemberModel>>> Search(string query) {
        var result = await ((MemberRepository)_repository).Search(query, TenantId);
        return Ok(result);
    }

}
