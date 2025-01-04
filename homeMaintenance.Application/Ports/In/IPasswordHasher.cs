namespace homeMaintenance.Application.Ports.In
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, byte[] salt);
        bool VerifyPassword(string password, string storedPasswordHash);
        byte[] GenerateSalt();
        bool IsValidStrongPassword(string password);
    }
}
