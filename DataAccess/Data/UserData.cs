using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public UserData(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
        _sqlDataAccess.LoadData<UserModel, dynamic>("dbo.sp_user_GetAll", new { });

    public async Task<UserModel?> GetUser(int id)
    {
        var result = await _sqlDataAccess.LoadData<UserModel, dynamic>("dbo.sp_user_GetUser",
            new
            {
                Id = id
            });

        return result.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) => _sqlDataAccess.SaveData("dbo.sp_user_AddUser",
        new
        {
            user.UserName
        });

    public Task UpdateUser(UserModel user) => _sqlDataAccess.SaveData("dbo.sp_user_UpdateUser", user);

    public Task DeleteUser(int id) => _sqlDataAccess.SaveData("dbo.sp_user_DeleteUser",
        new
        {
            Id = id
        });
}
