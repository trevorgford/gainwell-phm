using Dapper;
using Gainwell.Data;
using Gainwell.Models.Dapper;

namespace Gainwell.Services;

public class UserValidationService(DapperDbContext context) {
    
    protected readonly DapperDbContext _context = context;

    public UserModel? ValidateLogin(LoginModel model) {
        if (model.UserName == "tford" && model.Password == "password") {
            return new UserModel { Id = 1, UserName = "tford" };
        }

        return null;  
    }

    public void CreateTempSession(string sessionId, int userId) {
        var parameters = new DynamicParameters();
        parameters.Add("@sessionId", sessionId);
        parameters.Add("@userId", userId);

        using var db = _context.CreateConnection();
        db.Execute("tempSession_create", parameters);
    }

    public async Task<UserModel> LoadTempSession(string sessionId, int tenantId) {
        var parameters = new DynamicParameters();
        parameters.Add("@sessionId", sessionId);
        parameters.Add("@tenantId", tenantId);

        using var db = _context.CreateConnection();
        int? userId = await db.ExecuteScalarIntAsync("tempSession_load", parameters);

        return new UserModel { Id = userId };
    }
    
}
