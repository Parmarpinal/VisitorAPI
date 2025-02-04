using FluentValidation;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentModel>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.DepartmentName).NotEmpty().WithMessage("Department name can not be empty");
            RuleFor(d => d.OrganizationID).NotEmpty().WithMessage("Organization can not be empty");
        }
    }
}
