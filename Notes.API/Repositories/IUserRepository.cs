using Microsoft.IdentityModel.Tokens;

using Notes.API.Models.Entities;

namespace Notes.API.Repositories;

public interface IUserRepository
{
    Task<User> AuthenticateAsync(string username, string password);
}
