using Microsoft.AspNetCore.Mvc;
using WannaBePrincipal.Models;

namespace WannaBePrincipal.Controllers
{
    /// <summary>
    /// API controller for managing users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userModel;

        public UserController(string repoProject = "able-source-200515")
        {
            _userModel = new(repoProject);
        }

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {            
            return Ok(await _userModel.GetUsersFromDB());
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">The user data to create.</param>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserData user)
        {
            if (!ModelState.IsValid)  // check body
            {
                return BadRequest(ModelState);
            }

            var createResult = await _userModel.AddUser(user);

            return Created(createResult, user);
        }

        /// <summary>
        /// Give back one user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest("Please provide the user id.");
            }
            if (null == await _userModel.GetUser(id))
            {
                return NotFound("Problem occurred while editing the {id} user.");
            }
            return Ok(id + " user was edited.");
        }

        /// <summary>
        /// Edit an existing user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <param name="user">The user data to update.</param>
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] UserData user)
        {
            if (id == null)
            {
                return BadRequest("Please provide the user id.");
            }

            if (!ModelState.IsValid) // check body
            {
                return BadRequest(ModelState);
            }

            if (await _userModel.EditUser(id, user))
            {
                return NotFound("Problem occurred while editing the {id} user.");
            }
            return Ok(id + " user was edited.");
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest("Please provide the user id.");
            }
            if (await _userModel.DeleteUser(id))
            {
                return NotFound("Problem occurred while editing the {id} user.");
            }
            return NoContent();
        }
    }
}
