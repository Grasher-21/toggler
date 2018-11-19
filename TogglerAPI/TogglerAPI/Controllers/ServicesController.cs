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
    public class ServicesController : ControllerBase
    {
        private readonly IHeaderValidation HeaderValidation;
        private readonly IServiceService ServiceService;
        private readonly string Username = "Username";
        private readonly string Password = "Password";

        public ServicesController(IHeaderValidation headerValidation, IServiceService serviceService)
        {
            HeaderValidation = headerValidation;
            ServiceService = serviceService;
        }

        [HttpPost]
        public ActionResult<Guid> CreateService([FromBody] ServiceRequestModel serviceRequestModel)
        {
            if (serviceRequestModel == null || string.IsNullOrWhiteSpace(serviceRequestModel.Name) || string.IsNullOrWhiteSpace(serviceRequestModel.Version))
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

            Guid guid = ServiceService.CreateService(serviceRequestModel);

            if (guid == Guid.Empty)
            {
                return StatusCode(500);
            }

            return StatusCode(201, guid);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteService(Guid id)
        {
            if (id == Guid.Empty)
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

            if (!ServiceService.DeleteService(id))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }

        [HttpGet("{id}")]
        public ActionResult<ServiceResponseModel> GetService(Guid id)
        {
            if (id == Guid.Empty)
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

            ServiceResponseModel serviceResponseModel = ServiceService.GetService(id);

            if (serviceResponseModel == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, serviceResponseModel);
        }

        [HttpGet]
        public ActionResult<List<ServiceResponseModel>> GetServiceList()
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

            List<ServiceResponseModel> serviceList = ServiceService.GetServiceList();

            if (serviceList == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, serviceList);
        }

        [HttpPut]
        public ActionResult UpdateService([FromBody] ServiceRequestModel service)
        {
            if (service == null || service.ServiceId == Guid.Empty || string.IsNullOrWhiteSpace(service.Name) || string.IsNullOrWhiteSpace(service.Version))
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

            if (!ServiceService.UpdateService(service))
            {
                return StatusCode(404);
            }

            return StatusCode(204);
        }
    }
}
