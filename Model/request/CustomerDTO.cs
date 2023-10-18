using System.ComponentModel.DataAnnotations;

namespace BankAccount.Model.request
{
    public class CustomerDTO
    {

        public string FirstName { get; set; }

        public string LastName { get; set; } 

        public string MiddleName { get; set; }
        
        public DateTime DOB { get; set; }

        public Boolean isFilipino { get; set; } 

    }
}
