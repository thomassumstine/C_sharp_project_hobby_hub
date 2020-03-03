using System.ComponentModel.DataAnnotations;

namespace HobbyHub.Models
{
    public class LogModel{
        [Required]
        public string LUsername {get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string LPassword {get;set;}
    }

}