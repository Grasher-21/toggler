using Microsoft.AspNetCore.Mvc;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IHeaderValidation HeaderValidation;

        public RolesController(IHeaderValidation headerValidation)
        {
            HeaderValidation = headerValidation;
        }

        //[HttpPost]
        //[Route("createrole")]
        //public ActionResult CreateRole([FromBody] string name, [FromBody] string description)
        //{
        //    var tmp = Request.Headers[""];


        //    //if (!HeaderValidation.ValidateUserCredentials(username, password))
        //    //{
        //    //    return Forbid();
        //    //}

        //    return Ok();
        //}

        [HttpPost]
        [Route("createRole")]
        public ActionResult<RoleModel> CreateRole([FromBody] RoleModel role)
        {
            var tmp = Request.Headers["Username"];

            return Ok(role);
        }
    }
}
