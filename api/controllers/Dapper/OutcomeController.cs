using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/outcomes")]
[ApiController]
public class OutcomeController(IRepository<OutcomeModel> repository) : BaseController<OutcomeModel>(repository) {}
