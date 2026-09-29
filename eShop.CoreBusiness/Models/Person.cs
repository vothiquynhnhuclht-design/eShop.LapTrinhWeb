using System.ComponentModel.DataAnnotations;

namespace eShop.CoreBusiness.Models
{
    public class Person
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Employee Number is required.")]
        [Range(1, 1000, ErrorMessage = "Employee Number must be between 1 and 1000.")]
        public int EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string Email { get; set; }
    }
}
