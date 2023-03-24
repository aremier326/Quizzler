using System.ComponentModel.DataAnnotations;

namespace Quizzler.Mvc.PL.Models
{
    public class UserInputModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string UserName => GetUserName();

        private string GetUserName()
        {
            if (Email == null) return string.Empty;
            Span<char> emailSpan = new Span<char>(Email.ToCharArray());
            return emailSpan.Slice(0, emailSpan.IndexOf("@")).ToString();
        }
    }
}
