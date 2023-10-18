using BankAccount.Constants;
using BankAccount.Model;
using FluentValidation;



namespace BankAccount.Validations
{
    //Validation for Account Models
    public class AccountValidator: AbstractValidator<Accounts>
    {
            
        public AccountValidator()
        {
            RuleFor(c => c.AccountType).NotNull().NotEmpty().Must(IsValidAccountType);
            RuleFor(c => c.BranchAddress).NotNull().NotEmpty();
            RuleFor(c => c.AccountNumber).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(c => c.InitialDeposit).NotNull().NotEmpty().LessThanOrEqualTo(BankConstantValues.MaxInitialDeposit).When(c => c.Id.Equals(null) );
        }

        public bool IsValidAccountType(string accountType)
        {
            return Enum.IsDefined(typeof(BankConstantValues.AccountType), accountType.ToString().ToLower());
        }

    }
}
