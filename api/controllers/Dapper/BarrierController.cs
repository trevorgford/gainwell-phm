using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/barriers")]
[ApiController]
public class BarrierController(IRepository<BarrierModel> repository) : BaseController<BarrierModel>(repository) {}
