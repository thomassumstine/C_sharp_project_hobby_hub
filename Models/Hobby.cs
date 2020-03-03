using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HobbyHub.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }
        [Required]
        [Display(Name = "Hobby Type")]
        [MinLength(2, ErrorMessage = "Field has to contain at least 2 characters and more")]
        public string HobbyType { get; set; }
        public string Description { get; set; }
        public User HobbyCreator {get; set;}
        public int HobbyCreatorId {get; set;}
        public List<AddedToPersonalHobbies> Enthusiasts {get; set;}

    }
}