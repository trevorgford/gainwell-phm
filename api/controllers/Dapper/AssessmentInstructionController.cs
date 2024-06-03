using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/instructions")]
[ApiController]
public class AssessmentInstructionController(IRepository<AssessmentInstructionModel> repository) : BaseController<AssessmentInstructionModel>(repository) {}
