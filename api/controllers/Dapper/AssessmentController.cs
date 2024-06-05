using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Gainwell.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments")]
[ApiController]
[RequiresTenant(true)]
public class AssessmentController(IRepository<AssessmentModel> repository) : BaseController<AssessmentModel>(repository) {

    private readonly IRepository<AssessmentModel> _repository = repository;

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Loads full Assessment")]
    public async Task<ActionResult<AssessmentModel>> LoadFullAssessment(int id) {
        var result = await ((AssessmentRepository)_repository).LoadFullAssessment(id);
        return Ok(result);
    }

    [HttpGet("getById")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<AssessmentModel>> GetById(int id) {
        throw new NotImplementedException();
    }

    // [HttpGet("current")]
    // [SwaggerOperation(Summary = "Gets all current Assessment records")]
    // public async Task<ActionResult<IEnumerable<AssessmentModel>>> GetCurrentAssessments() {
    //     var result = await ((AssessmentRepository)_repository).GetCurrentAssessments(TenantId);
    //     return Ok(result);
    // }

    // [HttpPost("setCurrent")]
    // [SwaggerOperation(Summary = "Sets the Assessment as current")]
    // public async Task<IActionResult> SetCurrent([FromBody] AssessmentModel assessment) {
    //     var result = await ((AssessmentRepository)_repository).SetCurrent(assessment, UserId, TenantId);
    //     return Ok(result);
    // }
    
}
