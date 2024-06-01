using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/interventions")]
[ApiController]
public class InterventionController(IRepository<InterventionModel> repository) : BaseController<InterventionModel>(repository) {}
