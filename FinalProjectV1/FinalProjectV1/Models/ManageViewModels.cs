using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FinalProjectV1.Models
{
    public class IndexViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string IsraeliIdentify { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Cars { get; set; }
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה נוכחית")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} חייבת להיות לפחות בת {2} אותיות.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה חדשה")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אימות סיסמה חדשה")]
        [Compare("NewPassword", ErrorMessage = "הסיסמה החדשה ואימות הסיסמה חייבים להיות זהים.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "מספר טלפון")]
        public string Number { get; set; }
    }

    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "כתובת מייל")]
        public string Email { get; set; }
    }

    public class ChangeAdressViewModel
    {
        [Required]
        [StringLength(100)]
        [Display( Name = "כתובת מגורים")]
        public string Adress { get; set; }
    }

    public class ChangeNameViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "שם")]
        public string Name { get; set; }
    }

    public class AddOwnCar
    {
        [Required]
        [Phone]
        [Display(Name = "מספר רכב")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}