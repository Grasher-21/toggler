namespace TogglerAPI.Interfaces
{
    public interface IHeaderValidation
    {
        int ValidateUserCredentials(string username, string password);
        bool ValidateUserPermissions(int roleId);
    }
}
