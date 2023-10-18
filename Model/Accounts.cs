using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace BankAccount.Model
{
    public class Accounts
    {
        //ef for sqlite will automatically create the primary key as autoincrement
        public int Id { get; set; }

        [MaxLength(12)]
        public string AccountNumber { get; set; } = string.Empty;

        [MaxLength(20)]
        public string AccountType { get; set; } = string.Empty;

        [MaxLength(50)]
        public string BranchAddress { get; set; } = string.Empty;

        public Decimal InitialDeposit { get; set; } = new Decimal(0);

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

    }
}
