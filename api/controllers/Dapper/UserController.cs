using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/users")]
[ApiController]
public class UserController(IRepository<UserModel> repository) : BaseController<UserModel>(repository) {}
