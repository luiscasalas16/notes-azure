using System.ComponentModel.DataAnnotations;

namespace NetApplicationServiceWebMvc.Api
{
    public class UserDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "FistName is required.")]
        public string FistName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}
