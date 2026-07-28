namespace AuthenticationService.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Generates a secure hash for the given password.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <returns>Hashed password.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies whether the supplied password matches the stored hash.
        /// </summary>
        /// <param name="password">Plain text password.</param>
        /// <param name="hashedPassword">Stored hashed password.</param>
        /// <returns>True if the password is valid; otherwise false.</returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}