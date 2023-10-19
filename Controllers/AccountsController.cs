using BankAccount.Manager;
using BankAccount.Model;
using BankAccount.Model.request;
using BankAccount.Model.response;
using BankAccount.Model.updaterequest;
using BankAccount.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBankManager bankManager;

        public AccountsController(IBankManager bankManager)
        {
            this.bankManager = bankManager;
        }

        // GET - Get All Accounts
        [HttpGet]
        [Route("GetallAccounts")]
        public async Task<ActionResult<List<AccountResponseDTO>>> GetAllAccounts()
        {
            var result = await bankManager.GetAllAccountAsync();
            return Ok(result); 

        }


        // GET - Get All Accounts by Customer ID
        [HttpGet]
        [Route("GetAccountsByCustomer{custId:int}")]
        public async Task<ActionResult<List<AccountResponseDTO>>> GetAccountsByCustomer([FromRoute] int custId)
        {
            var result = await bankManager.GetAccountByCustomerAsync(custId);
            return Ok(result);
        }


        // GET - Create Account Per Customer
        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount(AccountDTO req)
        {
            Accounts request = new Accounts
            {
                AccountNumber = req.AccountNumber,
                AccountType = req.AccountType,
                BranchAddress = req.BranchAddress,
                InitialDeposit = req.InitialDeposit,
                CustomerId = req.CustomerId,
            };

            var isCustomerExist = await bankManager.GetCustByIdAsync(req.CustomerId);
            if (isCustomerExist is null)
            {
                return BadRequest("Customer does not exist");
            }

            var customerAccountNumber = await bankManager.IsAccountNumberExistAsync(request.AccountNumber);
            if (customerAccountNumber is not null)
            {
                return BadRequest("Account Number Already Exist. Create Unique Account Number");
            }

            AccountValidator acctValidator = new AccountValidator();
            var resultValidation = acctValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            await bankManager.CreateAccountAsync(request);


            return new ObjectResult(req)
            {
                StatusCode = StatusCodes.Status201Created
            };

        }

        // PUT - Edit Customer with Customer ID
        [HttpPut]
        [Route("EditAccount{id:int}")]
        public async Task<IActionResult> EditAccount([FromRoute] int id, AccountUpdateRequestDTO req)
        {

            Accounts request = new Accounts
            {
                Id = id,
                AccountNumber = req.AccountNumber,
                AccountType=req.AccountType,
                BranchAddress = req.BranchAddress,
                CustomerId= req.CustomerId,
            };

            var isCustomerExist = await bankManager.GetCustByIdAsync(req.CustomerId);
            if (isCustomerExist is null)
            {
                return BadRequest("Customer does not exist");
            }

            var customerAccountNumber = await bankManager.IsAccountNumberExistAsync(request.AccountNumber);
            if (customerAccountNumber is not null && customerAccountNumber.Id != id )
            {
                return BadRequest("Already in use by another customer");
            }

            AccountValidator acctValidator = new AccountValidator();
            var resultValidation = acctValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var result = await bankManager.UpdateAccountAsync(request);
            if (result == null)
            {
                return BadRequest();
            }

            AccountUpdateResponseDTO response = new AccountUpdateResponseDTO
            {
                Id = result.Id,
                AccountNumber = result.AccountNumber,
                AccountType = result.AccountType,
                BranchAddress = result.BranchAddress,
                InitialDeposit = result.InitialDeposit,
                CustomerId = result.CustomerId,
            };

            return Ok(response);

        }

        // DELETE - Delete Account

        [HttpDelete]
        [Route("DeleteAccount{acctnumber}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string acctnumber)
        {
            var result = await bankManager.DeleteAccountAsync(acctnumber);

            if (result == null)
            {
                return BadRequest("");
            }

            AccountUpdateResponseDTO response = new AccountUpdateResponseDTO
            {
                Id = result.Id,
                AccountNumber = result.AccountNumber,
                AccountType = result.AccountType,
                BranchAddress = result.BranchAddress,
                CustomerId= result.CustomerId,
            };

            return Ok(response);
        }



    }
}
