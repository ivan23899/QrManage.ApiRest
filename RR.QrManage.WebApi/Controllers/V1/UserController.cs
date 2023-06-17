using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RR.QrManage.DataAccess.BD_QRMANAGE.V1;
using RR.QrManage.Domain.Models;
using RR.QrManage.Domain.Models.V1.Requests;
using RR.QrManage.Framework;

namespace RR.QrManage.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [AllowAnonymous]
        [Route("User/Register")]
        [HttpPost]
        public IActionResult Register([FromBody] UserRequest request)
        {
            try
            {
                Domain.Entities.User user = new()
                {
                    Name = request.Name,
                    Alias = request.Alias,
                    PictureProfile = request.PictureProfile,
                    CreationUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                };
                var responseUserInsert = _user.Insert(user);
                return Ok(responseUserInsert);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
