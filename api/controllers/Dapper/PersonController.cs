using Microsoft.AspNetCore.Mvc;
using Gainwell.Models.Dapper;
using Gainwell.Repositories.Dapper;

namespace Gainwell.Controllers.Dapper;

[Route("api/people")]
[ApiController]
public class PersonController(IRepository<PersonModel> repository) : BaseController<PersonModel>(repository) {}
