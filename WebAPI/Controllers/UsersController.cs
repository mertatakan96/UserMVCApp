using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userService.GetAll();
            if (result != null && result.Any())
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDto userDto)
        {
            try
            {
                _userService.Add(userDto);
                return StatusCode(201);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(errors);
            }
        }
    }
}
