using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IValidatorService HeaderValidation;
        private readonly IRoleService RoleService;
        private readonly string Username = "Username";
        private readonly string Password = "Password";

        public RolesController(IValidatorService headerValidation, IRoleService roleService)
        {
            HeaderValidation = headerValidation;
            RoleService = roleService;
        }

        [HttpPost]
        public ActionResult<int> CreateRole([FromBody] RoleRequestModel roleRequestModel)
        {
            if (roleRequestModel == null || string.IsNullOrWhiteSpace(roleRequestModel.Name) || string.IsNullOrWhiteSpace(roleRequestModel.Description))
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

            result = RoleService.CreateRole(roleRequestModel);

            if (result == -1)
            {
                return StatusCode(500);
            }

            return StatusCode(201, result);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRole(int id)
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

            if (!RoleService.DeleteRole(id))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public ActionResult<RoleResponseModel> GetRole(int id)
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

            RoleResponseModel roleResponseModel = RoleService.GetRole(id);

            if (roleResponseModel == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, roleResponseModel);
        }

        [HttpGet]
        public ActionResult<List<RoleResponseModel>> GetRoleList()
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

            List<RoleResponseModel> roleList = RoleService.GetRoleList();

            if (roleList == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, roleList);
        }

        [HttpPut]
        public ActionResult UpdateRole([FromBody] RoleRequestModel role)
        {
            if (role?.RoleId == null || string.IsNullOrWhiteSpace(role.Name) || string.IsNullOrWhiteSpace(role.Description))
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

            if (!RoleService.UpdateRole(role))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }
    }
}
