using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHeaderValidation HeaderValidation;
        private readonly IUserService UserService;
        private readonly string Username = "Username";
        private readonly string Password = "Password";

        public UsersController(IHeaderValidation headerValidation, IUserService userService)
        {
            HeaderValidation = headerValidation;
            UserService = userService;
        }

        [HttpPost]
        public ActionResult<int> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            if (userRequestModel == null || string.IsNullOrWhiteSpace(userRequestModel.Username) || string.IsNullOrWhiteSpace(userRequestModel.Password))
            {
                return StatusCode(400);
            }

            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            result = UserService.CreateUser(userRequestModel);

            if (result == -1)
            {
                return StatusCode(500);
            }

            return StatusCode(201, result);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            if (!UserService.DeleteUserById(id))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }

        [HttpDelete]
        [Route("username/{username}")]
        public ActionResult DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return StatusCode(400);
            }

            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            if (!UserService.DeleteUserByUsername(username))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseModel> GetUser(int id)
        {
            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            UserResponseModel userResponseModel = UserService.GetUserById(id);

            if (userResponseModel == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, userResponseModel);
        }

        [HttpGet]
        [Route("username/{username}")]
        public ActionResult<UserResponseModel> GetUser(string username)
        {
            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            UserResponseModel userResponseModel = UserService.GetUserByUsername(username);

            if (userResponseModel == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, userResponseModel);
        }

        [HttpGet]
        public ActionResult<List<UserResponseModel>> GetUserList()
        {
            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            List<UserResponseModel> userList = UserService.GetUserList();

            if (userList == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, userList);
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserRequestModel user)
        {
            if (user?.UserId == null || string.IsNullOrWhiteSpace(user.Password))
            {
                return StatusCode(400);
            }

            int result = HeaderValidation.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!HeaderValidation.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            if (!UserService.UpdateUser(user))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }
    }
}
