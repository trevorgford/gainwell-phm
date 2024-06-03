using Microsoft.AspNetCore.Mvc;
using Gainwell.Repositories.Dapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Gainwell.Filters;
using Gainwell.Attributes;

namespace Gainwell.Controllers.Dapper;

[Authorize]
[ServiceFilter(typeof(UserJwtClaimsFilter))]
public class BaseController<T> : ControllerBase, IController {

    public BaseController(IRepository<T> repository) {
        _repository = repository;

        Type type = GetType();
        Attribute? attribute = Attribute.GetCustomAttribute(type, typeof(RequiresTenantAttribute));
        if(attribute == null) return;
        RequiresTenantAttribute requiresTenantAttribute = (RequiresTenantAttribute)attribute;
        RequiresTenant = requiresTenantAttribute.RequiresTenant;
    }

    private readonly IRepository<T> _repository;
    public int UserId { get; set; }
    public int TenantId { get; set; } = 5;
    public bool RequiresTenant { get; set; }

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> GetAll() {
        if (RequiresTenant) {
            var entities = await _repository.GetAllAsync(TenantId);
            return Ok(entities);
        }

        var entities2 = await _repository.GetAllAsync();
        return Ok(entities2);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetById(int id) {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<ActionResult<int>> Create(T entity) {
        var result = await _repository.CreateAsync(entity, UserId, TenantId);
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
