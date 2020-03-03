using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "First name has to contain at least 2 characters and more")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Last name has to contain at least 2 characters and more")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Username")]
        [MinLength(3, ErrorMessage = "Username has to contain at least 3 characters and more")]
        [MaxLength(15, ErrorMessage = "Username can not contain more than 15 characters")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        [Display(Name = "Password Confirmation")]
        public string CPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<AddedToPersonalHobbies> MyHobbies { get; set; }

        public List<Hobby> Hobby { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}