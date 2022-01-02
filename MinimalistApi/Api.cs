using DataAccess.Data;
using DataAccess.Models;

namespace MinimalistApi;

public static class Api
{
    public static void ConfigureApi(this WebApplication application)
    {
        application.MapGet("/Users", GetUsers);
        application.MapGet("/Users/{id}", GetUser);
        application.MapPost("/Users", InsertUser);
        application.MapPut("/Users", UpdateUser);
        application.MapDelete("/Users", DeleteUser);
    }

    private static async Task<IResult> GetUsers(IUserData userData)
    {
        try
        {
            return Results.Ok(await userData.GetUsers());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUser(int id, IUserData userData)
    {
        try
        {
            var user = await userData.GetUser(id);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertUser(UserModel user, IUserData userData)
    {
        try
        {
            await userData.InsertUser(user);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateUser(UserModel user, IUserData userData)
    {
        try
        {
            await userData.UpdateUser(user);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, IUserData userData)
    {
        try
        {
            await userData.DeleteUser(id);

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}