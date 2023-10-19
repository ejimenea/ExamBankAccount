namespace BankAccount.Model.response
{
    public class AccountResponseDTO
    {
        public int AccountId { get; set; }

        public string AccountNumber { get; set; }

        public string AccountType { get; set; }

        public string BranchAddress { get; set; }

        public Decimal InitialDeposit { get; set; }

        public int CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? FullName { get; set; }

        public DateTime DOB { get; set; }

        public int Age { get; set; }

        public Boolean isFilipino { get; set; }

    }
}
