using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/goals")]
[ApiController]
public class GoalController(IRepository<GoalModel> repository) : BaseController<GoalModel>(repository) {}
