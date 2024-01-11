using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public abstract class ViewModel
    {
        private protected const string _errorMessage = "Required field";

        [Required(ErrorMessage = $"{_errorMessage}")]
        [Display(Name = "Логин:")]
        public abstract string Login { get; set; }
        
        [Display(Name = "Пароль:")]
        public virtual string? Password { get; set; }

        [Display(Name = "Email:")]
        public virtual string? Email { get; set; }
    }
}
