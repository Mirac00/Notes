using Notes.API.Models.Entities;

namespace Notes.API.Repositories;

public interface ITokenHandler
{
    Task<string> CreateTokenAsync(User user);
}
