using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace BankAccount.Model
{
    public class Customer
    {
        //ef for sqlite will automatically create the primary key as autoincrement
        public int Id { get; set; }

        [MaxLength(20)]
        public string? FirstName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string MiddleName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DOB { get; set; }

        public int Age { get; set; } = int.MinValue;

        public Boolean isFilipino { get; set; } = false;

        public ICollection<Accounts> Accounts { get; set; }

    }
}
