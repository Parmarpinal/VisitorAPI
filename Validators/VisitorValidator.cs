using FluentValidation;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Validators
{
    public class VisitorValidator : AbstractValidator<VisitorModel>
    {
        public VisitorValidator() {
            RuleFor(u => u.UserID).NotEmpty().WithMessage("User can not be empty");
            RuleFor(u => u.OrganizationID).NotEmpty().WithMessage("Organization can not be empty");
            RuleFor(u => u.DepartmentID).NotEmpty().WithMessage("Department can not be empty");
            RuleFor(u => u.Purpose).NotEmpty().WithMessage("Purpose can not be empty").MaximumLength(500);
            RuleFor(u => u.HostID).NotEmpty().WithMessage("Host should not be empty");
            RuleFor(u => u.NoOfPerson).NotEmpty().WithMessage("No of person must be defined").InclusiveBetween(1,50);
            RuleFor(u => u.IDProof).NotEmpty().WithMessage("ID Proof must be defined");
            RuleFor(u => u.IDProofNumber).NotEmpty().WithMessage("ID proof number can not be empty");        
        }
    }
}
