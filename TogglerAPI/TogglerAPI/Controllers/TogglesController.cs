using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TogglesController : ControllerBase
    {
        private readonly IHeaderValidation HeaderValidation;
        private readonly IToggleService ToggleService;
        private readonly string Username = "Username";
        private readonly string Password = "Password";

        public TogglesController(IHeaderValidation headerValidation, IToggleService toggleService)
        {
            HeaderValidation = headerValidation;
            ToggleService = toggleService;
        }

        [HttpPost]
        public ActionResult<int> CreateToggle([FromBody] ToggleRequestModel toggleRequestModel)
        {
            if (toggleRequestModel == null || string.IsNullOrWhiteSpace(toggleRequestModel.Name))
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

            result = ToggleService.CreateToggle(toggleRequestModel);

            if (result == -1)
            {
                return StatusCode(500);
            }

            return StatusCode(201, result);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteToggle(int id)
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

            if (!ToggleService.DeleteToggle(id))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public ActionResult<ToggleResponseModel> GetToggle(int id)
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

            ToggleResponseModel ToggleResponseModel = ToggleService.GetToggle(id);

            if (ToggleResponseModel == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, ToggleResponseModel);
        }

        [HttpGet]
        public ActionResult<List<ToggleResponseModel>> GetToggleList()
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

            List<ToggleResponseModel> ToggleList = ToggleService.GetToggleList();

            if (ToggleList == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, ToggleList);
        }

        [HttpPut]
        public ActionResult UpdateToggle([FromBody] ToggleRequestModel toggleRequestModel)
        {
            if (toggleRequestModel?.ToggleId == null || string.IsNullOrWhiteSpace(toggleRequestModel.Name))
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

            if (!ToggleService.UpdateToggle(toggleRequestModel))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }
    }
}