using FluentValidation;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserTypeID).NotEmpty().WithMessage("User type can not be empty");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("User name can not be empty").MaximumLength(100);
            RuleFor(u => u.Gender).NotEmpty().WithMessage("Gender can not be empty");
            RuleFor(u => u.MobileNo).NotEmpty().WithMessage("Mobile number can not be empty").Length(10).WithMessage("Mobile number should be 10 digit").Matches("^[6-9][0-9]{9}$");
            RuleFor(u => u.Email).NotEmpty().WithMessage("User type can not be empty").EmailAddress().WithMessage("Email should be in correct format");
            RuleFor(u => u.Age).NotEmpty().WithMessage("Age can not be empty").InclusiveBetween(10,100);
            RuleFor(u => u.City).NotEmpty().WithMessage("City can not be empty").MaximumLength(100);
        }
    }
}
