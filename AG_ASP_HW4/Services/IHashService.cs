namespace AP_project.Services
{
    public interface IHashService
    {
        string ComputeHash(string salt, string password);
        string GenerateSalt();
    }
}