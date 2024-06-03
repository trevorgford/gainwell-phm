using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/sections/instructions")]
[ApiController]
public class AssessmentSectionInstructionController(IRepository<AssessmentSectionInstructionModel> repository) : BaseController<AssessmentSectionInstructionModel>(repository) {}
