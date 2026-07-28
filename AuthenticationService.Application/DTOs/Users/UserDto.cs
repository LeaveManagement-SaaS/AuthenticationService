using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Application.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public bool IsEmailVerified { get; set; }

        public byte Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
