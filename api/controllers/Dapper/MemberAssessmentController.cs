using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Gainwell.Controllers.Dapper;

[Route("api/members/{memberId}/assessments")]
[ApiController]
public class MemberAssessmentController(IRepository<MemberAssessmentModel> repository) : BaseController<MemberAssessmentModel>(repository) {

    private readonly IRepository<MemberAssessmentModel> _repository = repository;

    [HttpGet]
    [SwaggerOperation(Summary = "Gets a member's assessments")]
    public async Task<ActionResult<IEnumerable<MemberAssessmentModel>>> LoadMemberAssessments(int memberId) {
        var result = await ((MemberAssessmentRepository)_repository).LoadMemberAssessments(memberId);
        return Ok(result);
    }

    [HttpGet("getAll")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<MemberAssessmentModel>>> GetAll() {
        throw new NotImplementedException();
    }

    [HttpPost("{memberAssessmentId}/answers")]
    [SwaggerOperation(Summary = "Saves a member's assessment answers")]
    public async Task<ActionResult<int>> SaveAnswers(int memberAssessmentId, [FromBody] List<MemberAssessmentAnswerModel> answers) {
        var result = await ((MemberAssessmentRepository)_repository).SaveAnswers(memberAssessmentId, answers, UserId);
        return Ok(result);
    }

    // [ApiExplorerSettings(IgnoreApi = true)]
    // public override Task<ActionResult<MemberAssessmentModel>> GetById(int id) {
    //     throw new NotImplementedException();
    // }

    // [ApiExplorerSettings(IgnoreApi = true)]
    // public override Task<ActionResult<int>> Create(MemberAssessmentModel entity) {
    //     throw new NotImplementedException();
    // }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<int>> Deactivate(int id) {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<int>> Activate(int id) {
        throw new NotImplementedException();
    }

}
