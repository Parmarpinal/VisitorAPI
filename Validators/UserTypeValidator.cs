using FluentValidation;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Validators
{
    public class UserTypeValidator : AbstractValidator<UserTypeModel>
    {
        public UserTypeValidator()
        {
            RuleFor(u => u.UserTypeName).NotEmpty().WithMessage("User type can not be empty").MaximumLength(100).WithMessage("Length must be less than 100");
        }
    }
}
