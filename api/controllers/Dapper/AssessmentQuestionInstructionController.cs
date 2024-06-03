using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/questions/instructions")]
[ApiController]
public class AssessmentQuesitonInstructionController(IRepository<AssessmentQuestionInstructionModel> repository) : BaseController<AssessmentQuestionInstructionModel>(repository) {}
