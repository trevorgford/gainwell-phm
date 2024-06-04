using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/questions")]
[ApiController]
public class AssessmentQuestionController(IRepository<AssessmentQuestionModel> repository) : BaseController<AssessmentQuestionModel>(repository) {

    private readonly IRepository<AssessmentQuestionModel> _repository = repository;

    [HttpGet]
    [Route("/api/assessments/{assessmentId}/questions")]
    [SwaggerOperation(Summary = "Gets Questions by Assessment Id")]
    public async Task<ActionResult<IEnumerable<AssessmentQuestionModel>>> GetAssessmentQuestions(int assessmentId) {
        var result = await ((AssessmentQuestionRepository)_repository).GetAssessmentQuestions(assessmentId);
        return Ok(result);
    }

    [HttpGet]
    [Route("/api/assessments/{assessmentId}/sections/{sectionId}/questions")]
    [SwaggerOperation(Summary = "Gets Questions by Section Id")]
    public async Task<ActionResult<IEnumerable<AssessmentQuestionModel>>> GetAssessmentQuestionsBySection(int sectionId) {
        var result = await ((AssessmentQuestionRepository)_repository).GetAssessmentQuestionsBySection(sectionId);
        return Ok(result);
    }    

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<AssessmentQuestionModel>>> GetAll() {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<AssessmentQuestionModel>> GetById(int id) {
        throw new NotImplementedException();
    }

}
