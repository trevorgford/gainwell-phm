namespace Gainwell.Models.Dapper;

public class UserModel : ModelBase {

    public string? UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? PasswordSalt { get; set; }

    public bool? Active { get; set; }

    public List<TenantModel> Tenants { get; set; } = [];

}
