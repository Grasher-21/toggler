using System;

namespace TogglerAPI.Interfaces
{
    public interface IValidatorService
    {
        int ValidateUserCredentials(string username, string password);
        bool ValidateUserPermissions(int roleId);
        bool ValidateServiceCredentials(Guid serviceId);
    }
}
