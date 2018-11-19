using System;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.BusinessCore
{
    public class ValidatorService : IValidatorService
    {
        private readonly IUserRepository UserRepository;
        private readonly IRoleRepository RoleRepository;
        private readonly IServiceRepository ServiceRepository;
        private readonly int Invalid = -1;

        public ValidatorService(IUserRepository userRepository, IRoleRepository roleRepository, IServiceRepository serviceRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            ServiceRepository = serviceRepository;
        }

        public int ValidateUserCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Invalid;
            }

            UserModel userModel = UserRepository.GetUserByUsername(username);
            
            if (userModel == null || !userModel.Password.Equals(password))
            {
                return Invalid;
            }

            return userModel.RoleId;
        }

        public bool ValidateUserPermissions(int roleId)
        {
            RoleModel roleModel = RoleRepository.GetRole(roleId);

            if (roleModel == null || !roleModel.Name.Equals("Administrator"))
            {
                return false;
            }

            return true;
        }

        public bool ValidateServicePermissions(Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return false;
            }

            ServiceModel serviceModel = ServiceRepository.GetService(serviceId);

            if (serviceModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
