using FluentValidation;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Validators
{
    public class OrganizationValidator : AbstractValidator<OrganizationModel>
    {
        public OrganizationValidator()
        {
            RuleFor(o => o.OrganizationName).NotEmpty().WithMessage("Organization name can not be empty");
            RuleFor(o => o.Head).NotEmpty().WithMessage("Head name can not be empty").MaximumLength(50);
            RuleFor(o => o.City).NotEmpty().WithMessage("City name can not be empty");
            RuleFor(o => o.Address).NotEmpty().WithMessage("Address can not be empty").MaximumLength(500);
        }
    }
}
