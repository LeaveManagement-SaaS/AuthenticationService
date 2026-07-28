using AuthenticationService.Domain.Interfaces;

namespace AuthenticationService.Infrastructure.Security
{
    /// <summary>
    /// BCrypt implementation of password hashing.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        // You can adjust the work factor (cost).
        // Default is 11. Higher values increase security but require more CPU time.
        private const int WorkFactor = 11;

        /// <summary>
        /// Hashes a plain text password.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <returns>BCrypt hashed password.</returns>
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        /// <summary>
        /// Verifies a password against a stored BCrypt hash.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <param name="hashedPassword">Stored BCrypt hash.</param>
        /// <returns>True if passwords match.</returns>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}