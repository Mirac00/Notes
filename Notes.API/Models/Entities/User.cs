using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.API.Models.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string EmailAdress { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public List<string> Roles { get; set; }
    public List<User_Role> UserRoles { get; set; }
    
}
