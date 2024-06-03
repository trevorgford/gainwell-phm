using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/choices")]
[ApiController]
public class AssessmentChoiceController(IRepository<AssessmentChoiceModel> repository) : BaseController<AssessmentChoiceModel>(repository) {}
