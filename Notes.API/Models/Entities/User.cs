using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace Notes.API.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<IdentityUserRole<Guid>> Roles { get; set; }
    }
}
