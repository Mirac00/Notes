using Microsoft.EntityFrameworkCore;

using Notes.API.Data;
using Notes.API.Models.Entities;

namespace Notes.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TableDbContext tableDbContext;

    public UserRepository(TableDbContext tableDbContext)
    {
        this.tableDbContext = tableDbContext;
    }
    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await tableDbContext.Users
            .FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);

        if (user == null)
        {
            return null;
        }

        var userRoles = await tableDbContext.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();

        if(userRoles.Any())
        {
            foreach(var userRole in userRoles)
            {
                user.Roles = new List<string>();
               var role = await tableDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.Id);
                if(role != null) 
                {
                    user.Roles.Add(role.Name);
                }
            }
        }
        user.Password = null;
        return user;
    }
}
