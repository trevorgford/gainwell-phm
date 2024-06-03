using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/questions")]
[ApiController]
public class AssessmentQuestionController(IRepository<AssessmentQuestionModel> repository) : BaseController<AssessmentQuestionModel>(repository) {}
