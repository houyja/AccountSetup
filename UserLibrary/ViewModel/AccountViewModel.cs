using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserLibrary.ViewModel
{
    public class AccountViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string username { get; set; }

        [Required]
        [DisplayName("Email")]
        public string email { get; set; }

        [Required]
        [DisplayName("Firstname")]
        public string firstname { get; set; }

        [Required]
        [DisplayName("Lastname")]
        public string lastname { get; set; }

        [DisplayName("Created On")]
        public DateTime? CreatedOn { get; set; }

        [DisplayName("Last Edited On")]
        public DateTime? LastEditedOn { get; set; }

        public int? AccountID { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    //Register
    public class AccountViewModel_Registration
    {
        [Required(ErrorMessage = "Please enter in a Valid Username")]
        [DisplayName("Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please enter in a Valid Email")]
        [DisplayName("Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter your First Name")]
        [DisplayName("Firstname")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        [DisplayName("Lastname")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Please enter in a Password")]
        [DisplayName("Password")]
        [PasswordPropertyText]
        public string password { get; set; }

        [Required(ErrorMessage = "Please confirm Password")]
        [Compare(nameof(password), ErrorMessage = "Passwords don't match.")]
        [PasswordPropertyText]
        [DisplayName("Confirm Password")]
        public string confirmpassword { get; set; }

        public string salt { get; set; }

        public int? AccountID { get; set; }
        public string EmailVerificationToken { get; set; }
        public string EmailVerificationKey { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    //Login
    public class AccountViewModel_Login
    {
        [Required(ErrorMessage = "Please enter in a Valid Login")]
        [DisplayName("Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter in a Password")]
        [DisplayName("Password")]
        [PasswordPropertyText]
        public string password { get; set; }

        public string salt { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }

    //Verify Email
    public class AccountViewModel_VerifyEmail
    {
        public string EmailVerificationToken { get; set; }
        public string EmailVerificationKey { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public int? AccountID { get; set; }
    }
    public class AccountViewModel_GenerateEmailVerificationToken
    {
        [Required(ErrorMessage = "Please enter in a Valid Email To Resend Verification EMail")]
        [DisplayName("Email/Username")]
        public string Login { get; set; }
        public int? AccountID { get; set; }
        public string EmailVerificationToken { get; set; }
        public string EmailVerificationKey { get; set; }
        public string Email { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    //Password Reset
    public class AccountViewModel_GeneratePasswordResetToken
    {
        [Required(ErrorMessage = "Please enter in a Valid Email")]
        [DisplayName("Email/Username")]
        public string LoginID { get; set; }


        public string salt { get; set; }

        public string key { get; set; } = System.Guid.NewGuid().ToString();
        public string GUID { get; set; }
        public string Email { get; set; }
        public int? AccountID { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
    public class AccountViewModel_VerifyPasswordResetToken
    {
        public string TokenID { get; set; }
        public string TokenKey { get; set; }
        public string TokenSalt { get; set; }

        public int? AccountID { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
    public class AccountViewModel_ResetPassword
    {
        [Required(ErrorMessage = "Please enter in a Password")]
        [DisplayName("Password")]
        [PasswordPropertyText]
        public string password { get; set; }

        [Required(ErrorMessage = "Please confirm Password")]
        [Compare(nameof(password), ErrorMessage = "Passwords don't match.")]
        [PasswordPropertyText]
        [DisplayName("Confirm Password")]
        public string confirmpassword { get; set; }

        public string passwordSalt { get; set; }
        public int? AccountID { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }

    //Edit Account INfo
    public class AccountViewModel_EditAccountInfo
    {
        [Required(ErrorMessage = "Please enter in a Valid Email")]
        [DisplayName("Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter your First Name")]
        [DisplayName("Firstname")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        [DisplayName("Lastname")]
        public string lastname { get; set; }


        public int? AccountID { get; set; }
        public string EmailVerificationToken { get; set; }
        public string EmailVerificationKey { get; set; }


        public List<string> Errors { get; set; } = new List<string>();
    }
    public class AccountViewModel_UpdatePassword
    {
        [Required(ErrorMessage = "Please enter in existing Password")]
        [DisplayName("Current Password")]
        [PasswordPropertyText]
        public string curpassword { get; set; }

        [Required(ErrorMessage = "Please enter in the new Password")]
        [DisplayName("New Password")]
        [PasswordPropertyText]
        public string newpassword { get; set; }

        [Required(ErrorMessage = "Please confirm new Password")]
        [Compare(nameof(newpassword), ErrorMessage = "Passwords don't match.")]
        [PasswordPropertyText]
        [DisplayName("Confirm New Password")]
        public string confirmnewpassword { get; set; }



        public string cursalt { get; set; }
        public string newsalt { get; set; }
        public int? AccountID { get; set; }
        public string Email { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
