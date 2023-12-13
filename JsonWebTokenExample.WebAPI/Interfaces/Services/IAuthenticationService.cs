namespace JsonWebTokenExample.WebAPI.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> IssueTokenAsync(string userName);
    }
}