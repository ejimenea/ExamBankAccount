using BankAccount.Model;
using FluentValidation;

namespace BankAccount.Validations
{
    //Validation for Customer⌈ Models
    public class CustomerValidator: AbstractValidator<Customer>
    {
        public CustomerValidator() { 
            RuleFor(c => c.FirstName).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(c => c.LastName).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(c => c.MiddleName).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(c => c.DOB).NotNull().NotEmpty()
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of Birth is required and cannot accept future birthdate");
            RuleFor(c => c.isFilipino).NotNull();

        }
    }
}
