using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
    {
        public class User
        {
            [Key]
            public Guid IDUser { get; set; }


            [Required(ErrorMessage = "Name is required")]
            public string NameUser { get; set; }


            [Required(ErrorMessage = "Surname is required")]
            public string SurnameUser { get; set; }


            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Email adresa nije validna")]
            public string EmailUser { get; set; }


            [Required(ErrorMessage = "Password is required")]
            public string PasswordUser { get; set; }

            public string Salt {  get; set; }

            public Guid TeamId { get; set; }
            public Guid IDRole { get; set; }
        }
    }
