using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/sections")]
[ApiController]
public class AssessmentSectionController(IRepository<AssessmentSectionModel> repository) : BaseController<AssessmentSectionModel>(repository) {

    private readonly IRepository<AssessmentSectionModel> _repository = repository;

    [HttpGet]
    [Route("/api/assessments/{assessmentId}/sections")]
    [SwaggerOperation(Summary = "Gets Sections by Assessment Id")]
    public async Task<ActionResult<IEnumerable<AssessmentSectionModel>>> GetAssessmentSections(int assessmentId) {
        var result = await ((AssessmentSectionRepository)_repository).GetAssessmentSections(assessmentId);
        return Ok(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<AssessmentSectionModel>>> GetAll() {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<AssessmentSectionModel>> GetById(int id) {
        throw new NotImplementedException();
    }

}
