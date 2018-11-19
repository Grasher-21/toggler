using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.BusinessCore
{
    public class HeaderValidation : IHeaderValidation
    {
        private readonly IUserRepository UserRepository;
        private readonly IRoleRepository RoleRepository;
        private readonly int Invalid = -1;

        public HeaderValidation(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        public int ValidateUserCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Invalid;
            }

            UserModel user = UserRepository.GetUserByUsername(username);
            
            if (user == null || !user.Password.Equals(password))
            {
                return Invalid;
            }

            return user.RoleId;
        }

        public bool ValidateUserPermissions(int roleId)
        {
            RoleModel role = RoleRepository.GetRole(roleId);

            if (role == null || !role.Name.Equals("Administrator"))
            {
                return false;
            }

            return true;
        }
    }
}
