using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/tenants")]
[ApiController]
public class TenantController(IRepository<TenantModel> repository) : BaseController<TenantModel>(repository) {}
