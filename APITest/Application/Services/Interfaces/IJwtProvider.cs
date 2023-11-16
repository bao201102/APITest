namespace APITest.Application.Services.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(string name);
    }
}
