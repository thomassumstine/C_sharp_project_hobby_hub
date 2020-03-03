using System;
using System.ComponentModel.DataAnnotations;

namespace HobbyHub.Models
{
    public class AddedToPersonalHobbies{
        [Key]
        public int AddedToPersonalHobbiesId {get;set;}
        public int UserId {get;set;}
        public int HobbyId {get;set;}
        public Hobby hobby {get;set;}
        public User user {get;set;}
    }
}