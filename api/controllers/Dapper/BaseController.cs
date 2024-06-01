using Microsoft.AspNetCore.Mvc;
using Gainwell.Repositories.Dapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Gainwell.Filters;

namespace Gainwell.Controllers.Dapper;

[Authorize]
[ServiceFilter(typeof(UserJwtClaimsFilter))]
public class BaseController<T>(IRepository<T> repository) : ControllerBase, IController {

    private readonly IRepository<T> _repository = repository;
    public int UserId { get; set; }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetAll() {
        var entities = await _repository.GetAllAsync();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetById(int id) {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<ActionResult<int>> Create(T entity) {
        var result = await _repository.CreateAsync(entity, UserId);
        return Ok(result);
    }

    [HttpPost("deactivate/{id}")]
    public virtual async Task<ActionResult<int>> Deactivate(int id) {
        var result = await _repository.DeactivateAsync(new DynamicParameters(new { id, active = false }));
        return Ok(result);
    }

    [HttpPost("activate/{id}")]
    public virtual async Task<ActionResult<int>> Activate(int id) {
        var result = await _repository.ActivateAsync(new DynamicParameters(new { id, active = true }));
        return Ok(result);
    }

}
