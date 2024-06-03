using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/assessments/sections")]
[ApiController]
public class AssessmentSectionController(IRepository<AssessmentSectionModel> repository) : BaseController<AssessmentSectionModel>(repository) {}
