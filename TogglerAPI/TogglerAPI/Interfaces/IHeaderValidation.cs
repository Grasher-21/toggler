namespace TogglerAPI.Interfaces
{
    public interface IHeaderValidation
    {
        bool ValidateUserCredentials(string username, string password);
    }
}
