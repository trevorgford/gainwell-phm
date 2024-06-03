using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/questions/types")]
[ApiController]
public class AssessmentQuestionTypeController(IRepository<AssessmentQuestionTypeModel> repository) : BaseController<AssessmentQuestionTypeModel>(repository) {}
