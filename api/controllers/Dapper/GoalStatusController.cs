using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Gainwell.Attributes;

namespace Gainwell.Controllers.Dapper;

[Route("api/goals/statuses")]
[ApiController]
[RequiresTenant(true)]
public class GoalStatusController(IRepository<GoalStatusModel> repository) : BaseController<GoalStatusModel>(repository) {}
