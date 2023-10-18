using BankAccount.Model;

namespace BankAccount.Manager
{
    public interface IBankManager
    {
        //ACOUNT METHODS

        Task<IEnumerable<Accounts>> GetAllAccountAsync();

        Task<Accounts?> IsAccountNumberExistAsync(string accountNumber);

        Task<Accounts?> CreateAccountAsync(Accounts account);

        Task<Accounts> UpdateAccountAsync(Accounts accounts);

        Task<Accounts> DeleteAccountAsync(string accountNumber);

        Task<IEnumerable<Accounts>> GetAccountByCustomerAsync(int id);



        //CUSTOMER METHODS
        Task<IEnumerable<Customer>> GetAllCustomerAsync();

        Task<Customer> CreateCustomerAsync(Customer customer);
        
        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<Customer?> GetCustByIdAsync(int id);

        Task<Customer?> DeleteCustomerAsync(int id);

    }
}
