using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments")]
[ApiController]
public class AssessmentController(IRepository<AssessmentModel> repository) : BaseController<AssessmentModel>(repository) {}
