using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToggleServicePermissionsController : ControllerBase
    {
        private readonly IValidatorService ValidatorService;
        private readonly IToggleServicePermissionService ToggleServicePermissionService;
        private readonly string Username = "Username";
        private readonly string Password = "Password";

        public ToggleServicePermissionsController(IValidatorService headerValidation, IToggleServicePermissionService toggleServicePermissionService)
        {
            ValidatorService = headerValidation;
            ToggleServicePermissionService = toggleServicePermissionService;
        }

        [HttpPost]
        public ActionResult CreateToggleServicePermission([FromBody] ToggleServicePermissionRequestModel toggleServicePermissionRequestModel)
        {
            if (toggleServicePermissionRequestModel == null || toggleServicePermissionRequestModel.ServiceId == Guid.Empty)
            {
                return StatusCode(400);
            }

            int result = ValidatorService.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!ValidatorService.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            if (!ToggleServicePermissionService.CreatePermission(toggleServicePermissionRequestModel))
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpDelete("toggleid/{id}/serviceid/{serviceId}")]
        public ActionResult DeleteToggleServicePermission(int id, Guid serviceId)
        {
            int result = ValidatorService.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!ValidatorService.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            if (!ToggleServicePermissionService.DeletePermission(id, serviceId))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }

        [HttpGet("{serviceId}")]
        public ActionResult<List<ToggleResponseModel>> GetTogglesForServiceId(Guid serviceId)
        {
            if (!ValidatorService.ValidateServicePermissions(serviceId))
            {
                return StatusCode(403);
            }

            List<ToggleResponseModel> toggleResponseModelList = ToggleServicePermissionService.GetTogglesListForServiceId(serviceId);

            if (toggleResponseModelList == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, toggleResponseModelList);
        }

        [HttpGet("toggleid/{id}/serviceid/{serviceId}")]
        public ActionResult<ToggleServicePermissionResponseModel> GetToggleServicePermission(int id, Guid serviceId)
        {
            int result = ValidatorService.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!ValidatorService.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            ToggleServicePermissionResponseModel toggleServicePermissionResponseModel = ToggleServicePermissionService.GetToggleServicePermission(id, serviceId);

            if (toggleServicePermissionResponseModel == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, toggleServicePermissionResponseModel);
        }

        [HttpGet]
        public ActionResult<List<ToggleServicePermissionResponseModel>> GetToggleServicePermissionList()
        {
            int result = ValidatorService.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!ValidatorService.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            List<ToggleServicePermissionResponseModel> toggleServicePermissionResponseModelList = ToggleServicePermissionService.GetToggleServicePermissionList();

            if (toggleServicePermissionResponseModelList == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, toggleServicePermissionResponseModelList);
        }

        [HttpPut]
        public ActionResult UpdateToggleServicePermission([FromBody] ToggleServicePermissionRequestModel toggleServicePermissionRequestModel)
        {
            if (toggleServicePermissionRequestModel == null || toggleServicePermissionRequestModel.ServiceId == Guid.Empty)
            {
                return StatusCode(400);
            }

            int result = ValidatorService.ValidateUserCredentials(Request.Headers[Username], Request.Headers[Password]);

            if (result == -1)
            {
                return StatusCode(401);
            }

            if (!ValidatorService.ValidateUserPermissions(result))
            {
                return StatusCode(403);
            }

            if (!ToggleServicePermissionService.UpdatePermission(toggleServicePermissionRequestModel))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }
    }
}
