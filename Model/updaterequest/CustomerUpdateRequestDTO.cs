namespace BankAccount.Model.updaterequest
{
    public class CustomerUpdateRequestDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DOB { get; set; }

        public Boolean isFilipino { get; set; }

    }
}
