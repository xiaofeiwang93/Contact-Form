namespace YourNameSpace.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ContactFormModel
    {
<<<<<<< HEAD
        [Required(ErrorMessage = "Please provide your first name")]
        [DisplayName("First Name")]
=======
        [Required(ErrorMessage = "Please provide your first name")] //Error message content when user left this required text box empty
        [DisplayName("First Name")]                                 // Displayed name content in the form
>>>>>>> b7df25cbf6a6344f0cf04342c0a14086343a1788
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please provide your email")]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

<<<<<<< HEAD
        [Required(ErrorMessage = "Please write a message")]
=======
        [Required(ErrorMessage = "Please provide a message")]
>>>>>>> b7df25cbf6a6344f0cf04342c0a14086343a1788
        [DisplayName("Message")]
        public string Message { get; set; }
    }
}
