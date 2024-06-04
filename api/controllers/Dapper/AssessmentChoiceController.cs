using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/choices")]
[ApiController]
public class AssessmentChoiceController(IRepository<AssessmentChoiceModel> repository) : BaseController<AssessmentChoiceModel>(repository) {



    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<IEnumerable<AssessmentChoiceModel>>> GetAll() {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<AssessmentChoiceModel>> GetById(int id) {
        throw new NotImplementedException();
    }

}
