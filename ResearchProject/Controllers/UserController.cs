using Microsoft.AspNetCore.Mvc;
using ResearchProject.IServices;
using ResearchProject.Models;

namespace ResearchProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/user
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // POST api/user
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            var created = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("ID in the URL does not match the body.");

            var updated = _userService.UpdateUser(user);
            if (!updated)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _userService.DeleteUser(id);
            if (!deleted)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }
    }
}
