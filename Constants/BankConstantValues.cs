namespace BankAccount.Constants
{
    public static class BankConstantValues
    {
        public enum AccountType
        {
            savings,
            current
        }

        public static Decimal MaxInitialDeposit = new Decimal(1000000);
    }
}
