using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IValidator<User> _validator;

        public UserManager(IUserDal userDal, IValidator<User> validator)
        {
            this._userDal = userDal;
            this._validator = validator;
        }

        public void Add(AddUserDto userDto)
        {

            User user = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                DateOfBirth = userDto.DateOfBirth
            };

            var result = _validator.Validate(user);

            if (result.IsValid)
            {
                _userDal.Add(user);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetById(Guid id)
        {
            return _userDal.Get(u => u.Id == id);
        }

        public void Update(User user)
        {
            var result = _validator.Validate(user);

            if (result.IsValid)
            {
                _userDal.Update(user);
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
