using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationService.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public bool IsEmailVerified { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }

}
