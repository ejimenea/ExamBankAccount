﻿using BankAccount.DB;
using BankAccount.Model;
using BankAccount.Model.response;
using BankAccount.Utility;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Manager
{
    public class BankManager : IBankManager
    {
        private readonly AccountDBContext dBContext;

        public BankManager(AccountDBContext  dBContext) {
            this.dBContext = dBContext;
        }

        //ACCOUNT METHODS

        public async Task<IEnumerable<AccountResponseDTO>> GetAllAccountAsync()
        {

            var result = await (from a in this.dBContext.Accounts
                    join c in this.dBContext.Customers
                    on a.CustomerId equals c.Id
                    select new AccountResponseDTO()
                    {
                        AccountId = a.Id,
                        AccountNumber = a.AccountNumber,
                        AccountType = a.AccountType,
                        BranchAddress = a.BranchAddress,
                        InitialDeposit = a.InitialDeposit,
                        CustomerId = a.CustomerId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        MiddleName = c.MiddleName,
                        FullName = c.FullName,
                        DOB = (DateTime)c.DOB,
                        Age = c.Age,
                        isFilipino = c.isFilipino
                    }
            ).ToListAsync();

            return result;
        }


        public async Task<IEnumerable<AccountResponseDTO>> GetAccountByCustomerAsync(int id)
        {

            var result = await (from a in this.dBContext.Accounts
                                     join c in this.dBContext.Customers
                                     on a.CustomerId equals c.Id
                                     where a.CustomerId == id
                                     select new AccountResponseDTO()
                                     {
                                         AccountId = a.Id,
                                         AccountNumber = a.AccountNumber,
                                         AccountType = a.AccountType,
                                         BranchAddress = a.BranchAddress,
                                         InitialDeposit = a.InitialDeposit,
                                         CustomerId = a.CustomerId,
                                         FirstName = c.FirstName,
                                         LastName = c.LastName,
                                         MiddleName = c.MiddleName,
                                         FullName = c.FullName,
                                         DOB = (DateTime)c.DOB,
                                         Age = c.Age,
                                         isFilipino = c.isFilipino
                                     }
            ).ToListAsync();

            return result;
        }
        

        public async Task<Accounts?> IsAccountNumberExistAsync(string accountNumber)
        {
            return await dBContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<Accounts?> CreateAccountAsync(Accounts account)
        {
            await dBContext.Accounts.AddAsync(account);
            await dBContext.SaveChangesAsync();
            return account;
        }
        public async Task<Accounts> UpdateAccountAsync(Accounts accounts)
        {
            var existingAccountRecord = await dBContext.Accounts.FirstOrDefaultAsync(a => a.Id == accounts.Id);

            if (existingAccountRecord != null)
            {
                accounts.InitialDeposit = existingAccountRecord.InitialDeposit;
                dBContext.Entry(existingAccountRecord).CurrentValues.SetValues(accounts);
                await dBContext.SaveChangesAsync();

                return accounts;
            }

            return null;
        }

        public async Task<Accounts> DeleteAccountAsync(string accountNumber)
        {
            var existingAccountRecord = await dBContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (existingAccountRecord is null)
            {
                return null;
            }

            dBContext.Accounts.Remove(existingAccountRecord);
            await dBContext.SaveChangesAsync();
            return existingAccountRecord;
        }



        //CUSTOMER METHODS
        public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
        {
            return await dBContext.Customers.ToListAsync();
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            customer.FullName = $"{customer.FirstName}, {customer.LastName} {customer.MiddleName}";
            customer.Age = BankUtility.CalculateAgeFromDOB(customer.DOB);

            await dBContext.Customers.AddAsync(customer);
            await dBContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            customer.FullName = $"{customer.FirstName}, {customer.LastName} {customer.MiddleName}";
            customer.Age = BankUtility.CalculateAgeFromDOB(customer.DOB);

            var existingCustomerRecord = await dBContext.Customers.FirstOrDefaultAsync(a => a.Id == customer.Id);

            if (existingCustomerRecord != null)
            {
                dBContext.Entry(existingCustomerRecord).CurrentValues.SetValues(customer);
                await dBContext.SaveChangesAsync();

                return customer;
            }

            return null;
        }

        public async Task<Customer?> GetCustByIdAsync(int id)
        {
           return await dBContext.Customers.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Customer?> DeleteCustomerAsync(int id)
        {
            var existingCustomerRecord = await dBContext.Customers.FirstOrDefaultAsync(a => a.Id == id);

            if (existingCustomerRecord is null)
            {
                return null;
            }

            dBContext.Customers.Remove(existingCustomerRecord);
            await dBContext.SaveChangesAsync();
            return existingCustomerRecord;
        }
    }
}
