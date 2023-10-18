using BankAccount.Model;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.DB
{
    public class AccountDBContext: DbContext
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options): base(options)
        { 
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Accounts> Accounts => Set<Accounts>();

    }
}
