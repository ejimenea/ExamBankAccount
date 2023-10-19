namespace BankAccount.Model.response
{
    public class AccountUpdateResponseDTO
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public string AccountType { get; set; }

        public string BranchAddress { get; set; }

        public Decimal InitialDeposit { get; set; }

        public int CustomerId { get; set; }
    }
}
