namespace YourNameSpace.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ContactFormModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Message")]
        public string Message { get; set; }
    }
}