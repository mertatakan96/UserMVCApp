using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<User> _validator;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddUserDto userDto)
        {
            try
            {
                _userService.Add(userDto);

                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(userDto);
            }
        }
    }
}
