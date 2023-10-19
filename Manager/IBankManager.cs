using BankAccount.Model;
using BankAccount.Model.response;

namespace BankAccount.Manager
{
    public interface IBankManager
    {
        //ACOUNT METHODS

        Task<IEnumerable<AccountResponseDTO>> GetAllAccountAsync();

        Task<Accounts?> IsAccountNumberExistAsync(string accountNumber);

        Task<Accounts?> CreateAccountAsync(Accounts account);

        Task<Accounts> UpdateAccountAsync(Accounts accounts);

        Task<Accounts> DeleteAccountAsync(string accountNumber);

        Task<IEnumerable<AccountResponseDTO>> GetAccountByCustomerAsync(int id);



        //CUSTOMER METHODS
        Task<IEnumerable<Customer>> GetAllCustomerAsync();

        Task<Customer> CreateCustomerAsync(Customer customer);
        
        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<Customer?> GetCustByIdAsync(int id);

        Task<Customer?> DeleteCustomerAsync(int id);

    }
}
