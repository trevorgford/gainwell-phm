using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/members")]
[ApiController]
public class MemberController(IRepository<MemberModel> repository) : BaseController<MemberModel>(repository) {}
