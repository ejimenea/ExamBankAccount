namespace BankAccount.Model.response
{
    public class CustomerResponseDTO
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DOB { get; set; }

        public int Age { get; set; }

        public Boolean isFilipino { get; set; }


    }
}
