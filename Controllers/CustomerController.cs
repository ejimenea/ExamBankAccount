using BankAccount.Manager;
using BankAccount.Model;
using BankAccount.Model.request;
using BankAccount.Model.response;
using BankAccount.Model.updaterequest;
using Microsoft.AspNetCore.Mvc;
using BankAccount.Utility;
using BankAccount.Validations;
using FluentValidation;

namespace BankAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly IBankManager bankManager;

        public CustomerController(IBankManager bankManager) {
            this.bankManager = bankManager;
        }

        // GET - Get All Customer
        [HttpGet]
        [Route("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await bankManager.GetAllCustomerAsync();
            var result = new List<CustomerResponseDTO>();
            foreach (var customer in customers)
            {
                var custDOB = customer.DOB.HasValue ? customer.DOB : DateTime.Now;
                result.Add(new CustomerResponseDTO
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    MiddleName = customer.MiddleName,
                    DOB = (DateTime)customer.DOB,
                    Age = customer.Age,
                    isFilipino = customer.isFilipino
                });
            }

            return Ok(result);

        }


        // GET - Create Customer
        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerDTO req)
        {
            /*if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }*/

            Customer request = new Customer
            {
                LastName = req.LastName,
                FirstName = req.FirstName,
                MiddleName = req.MiddleName,
                DOB = req.DOB,
                isFilipino = req.isFilipino
            };

            CustomerValidator custValidator = new CustomerValidator();
            var resultValidation = custValidator.Validate(request);

            if (!resultValidation.IsValid) {
                return BadRequest(resultValidation.Errors);
            }

            await bankManager.CreateCustomerAsync(request);


            return new ObjectResult(req)
            {
                StatusCode = StatusCodes.Status201Created
            };

        }

        // GET - Get Customer by Customer ID
        [HttpGet]
        [Route("GetCustomer{id:int}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customerRecord = await bankManager.GetCustByIdAsync(id);
            if (customerRecord is null) { 
                return BadRequest("The customer does not exist.");
            }
            var custDOB = customerRecord.DOB.HasValue ? customerRecord.DOB: DateTime.Now;
            CustomerResponseDTO response = new CustomerResponseDTO
            {
                Id = customerRecord.Id,
                LastName = customerRecord.LastName,
                FirstName = customerRecord.FirstName,
                MiddleName = customerRecord.MiddleName,
                DOB = (DateTime)custDOB,
                Age = customerRecord.Age,
                isFilipino = customerRecord.isFilipino
            };

            return Ok(response);
        }


        // PUT - Edit Customer with Customer ID
        [HttpPut]
        [Route("EditCustomer{id:int}")]
        public async Task<IActionResult> EditCustomer([FromRoute] int id,CustomerUpdateRequestDTO req ) 
        {

            Customer request = new Customer
            {
                Id = id,
                LastName = req.LastName,
                FirstName = req.FirstName,
                MiddleName = req.MiddleName,
                DOB = req.DOB,
                isFilipino = req.isFilipino
            };

            CustomerValidator custValidator = new CustomerValidator();
            var resultValidation = custValidator.Validate(request);

            if (!resultValidation.IsValid)
            {
                return BadRequest(resultValidation.Errors);
            }

            var result = await bankManager.UpdateCustomerAsync(request);
            if (result == null ) {
                return BadRequest();
            }

            var custDOB = result.DOB.HasValue ? result.DOB : DateTime.Now;
            CustomerResponseDTO response = new CustomerResponseDTO
            {
                Id = result.Id,
                LastName = result.LastName,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                DOB = (DateTime)custDOB,
                Age = result.Age,
                isFilipino = result.isFilipino
            };

            return Ok(response);

        }


        [HttpDelete]
        [Route("DeleteCustomer{id:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var result = await bankManager.DeleteCustomerAsync(id);

            if (result == null ) {
                return BadRequest("");
            }

            var custDOB = result.DOB.HasValue ? result.DOB : DateTime.Now;
            CustomerResponseDTO response = new CustomerResponseDTO
            {
                Id = result.Id,
                LastName = result.LastName,
                FirstName = result.FirstName,
                MiddleName = result.MiddleName,
                DOB = (DateTime)custDOB,
                Age = result.Age,
                isFilipino = result.isFilipino
            };

            return Ok(response);
        }


    }
}
