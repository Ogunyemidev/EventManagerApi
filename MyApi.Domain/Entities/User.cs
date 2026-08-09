using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Domain.BaseEntities;

namespace MyApi.Domain
{
    public class User : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string HashedPassword { get; set; } = default!;


        public Role Role { get; set; } = default!;



    }
}