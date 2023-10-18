using System.ComponentModel.DataAnnotations;

namespace BankAccount.Model.request
{
    public class AccountDTO
    {
 
        public string AccountNumber { get; set; } 

        public string AccountType { get; set; } 

        public string BranchAddress { get; set; } 

        public Decimal InitialDeposit { get; set; } 

        public int CustomerId { get; set; }

    }
}
