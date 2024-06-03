using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;
using Gainwell.Attributes;

namespace Gainwell.Controllers.Dapper;

[Route("api/interventions/statuses")]
[ApiController]
[RequiresTenant(true)]
public class InterventionStatusController(IRepository<InterventionStatusModel> repository) : BaseController<InterventionStatusModel>(repository) {}
