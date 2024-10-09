using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyofficeACPDController : ControllerBase
    {
        private readonly MyofficeACPDService _userService;

        public MyofficeACPDController(MyofficeACPDService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyofficeAcpd>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] MyofficeAcpd user)
        {
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetAllUsers), new { id = user.AcpdSid }, user);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(string id, [FromBody] MyofficeAcpd user)
        {
            user.AcpdSid = id; // 設置 ID
            _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
