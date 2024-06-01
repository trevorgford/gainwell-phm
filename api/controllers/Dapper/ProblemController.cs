using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/problems")]
[ApiController]
public class ProblemController(IRepository<ProblemModel> repository) : BaseController<ProblemModel>(repository) {}
