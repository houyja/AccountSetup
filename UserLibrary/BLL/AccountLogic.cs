using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary.ViewModel;
using UserLibrary.DAL;

namespace UserLibrary.BLL
{
    class AccountLogic
    {
        //Register
        public static Boolean Registration(AccountViewModel_Registration view, string ConnectionString, SecurityLogic security)
        {
            if (view.Errors.Count > 0)
                return false;

            //Generate Salt
            view.salt = security.GenerateSalt();

            //Registers Account
            if (AccountDataAccess.Register(view, ConnectionString, security))
            {
                //Post DAL Verifications
                if (view.AccountID == null || view.AccountID <= 0)
                    view.Errors.Add("An Unexpected error occured when Setting up the Account. Please Contact a System Admin for Further information (Invalid Account ID)");

                if (view.EmailVerificationKey == null)
                    view.Errors.Add("An Unexpected error occured when Setting up the Account. Please Contact a System Admin for Further information (Invalid Key)");

                if (view.EmailVerificationToken == null)
                    view.Errors.Add("An Unexpected error occured when Setting up the Account. Please Contact a System Admin for Further information (Invalid Token)");

                if (view.Errors.Count > 0)
                    return false;

                return true;
            }

            return false;
        }

        //Verify Email
        public static Boolean VerifyEmail(AccountViewModel_VerifyEmail view, string ConnectionString)
        {
            if (view.EmailVerificationKey == null)
                view.Errors.Add("No Token was Provided");
            if (view.EmailVerificationKey == null)
                view.Errors.Add("No Key was Provided");

            if (view.Errors.Count > 0)
                return false;

            if (AccountDataAccess.VerifyEmail(view, ConnectionString))
            {
                if (view.AccountID == null || view.AccountID <= 0)
                    view.Errors.Add("Error Validating Email Verification Token");

                if (view.Errors.Count > 0)
                    return false;

                return true;
            }

            return false;
        }

        public static Boolean ResendPasswordVerificationEmail(AccountViewModel_GenerateEmailVerificationToken view, string ConnectionString)
        {
            if (view.Login == null)
                view.Errors.Add("Please insert a valid Login");

            if (view.Errors.Count > 0)
                return false;

            if (AccountDataAccess.ResendVerificationEmail(view, ConnectionString))
            {
                return true;
            }

            return false;
        }

        //Login
        public static Boolean Login(AccountViewModel_Login view, AccountViewModel account, string ConnectionString, SecurityLogic security)
        {
            if (view.Login == null)
                view.Errors.Add("No Login Name Provided");
            if (view.password == null)
                view.Errors.Add("No Password Provided");

            if (view.Errors.Count > 0)
                return false;

            if (AccountDataAccess.GetSalt(view, ConnectionString))
            {
                if (view.salt == null)
                    return false;

                if (view.Errors.Count > 0)
                    return false;

                if (AccountDataAccess.Login(view, account, ConnectionString, security))
                {
                    if (account.AccountID == null || account.AccountID <= 0)
                        view.Errors.Add("No Login Name Provided");
                    if (account.username == null)
                        view.Errors.Add("Error Retrieving Username");
                    if (account.email == null)
                        view.Errors.Add("Error Retrieving Email");

                    if (view.Errors.Count > 0)
                        return false;

                    if (account.Errors.Count > 0)
                        return false;

                    return true;
                }
            }
            return false;
        }

        //Reset Password
        public static Boolean GeneratePasswordResetToken(AccountViewModel_GeneratePasswordResetToken view, string ConnectionString, SecurityLogic security)
        {
            //Pre DAL Check
            if (view.LoginID == null)
            {
                view.Errors.Add("Please insert a valid Email");
            }

            if (view.Errors.Count > 0)
                return false;

            //Set Salt and Generate Password Reset Token
            view.salt = security.GenerateSalt();
            AccountDataAccess.GeneratePasswordResetToken(view, ConnectionString, security);

            //Post DAl Validation Check 
            if (view.AccountID == null)
                return false;
            if (view.Email == null)
                return false;
            if (view.Errors.Count > 0)
                return false;
            return true;
        }

        public static Boolean VerifyPasswordResetToken(AccountViewModel_VerifyPasswordResetToken view, string ConnectionString, SecurityLogic security)
        {
            //Checks if a TOken GUID was provided
            if (view.TokenID == null)
            {
                view.Errors.Add("Invalid Reset TokenID");
            }

            if (view.TokenKey == null)
            {
                view.Errors.Add("Invalid Reset TokenKey");
            }

            if (view.Errors.Count > 0)
                return false;

            //Gets salt for token if available 
            if (AccountDataAccess.VerifyPasswordResetToken_GetSalt(view, ConnectionString))
            {
                //Get Salt Validations
                if (view.Errors.Count > 0)
                    return false;

                if (view.TokenSalt == null)
                {
                    view.Errors.Add("Invalid Reset Token");
                    return false;
                }

                //Verifies whether a valid token existed
                if (AccountDataAccess.VerifyPasswordResetToken(view, ConnectionString, security))
                {
                    //Verify Salt Validations
                    if (view.Errors.Count > 0)
                        return false;

                    if (view.AccountID == null || view.AccountID <= 0)
                    {
                        view.Errors.Add("Invalid Reset Token");
                        return false;
                    }

                    return true;
                }
            }
            return false;
        }

        public static Boolean ResetPassword(AccountViewModel_ResetPassword resetPasswordView, AccountViewModel_VerifyPasswordResetToken verifyTokenView, string ConnectionString, SecurityLogic security)
        {
            //Checks if a valid password was provided
            if (resetPasswordView.password == null || resetPasswordView.password == "")
            {
                resetPasswordView.Errors.Add("Please Provide a Password");
            }

            if (resetPasswordView.Errors.Count > 0)
                return false;

            //Checks for a valid Reset Password Token (AccountID > 0 signifies a successful VerifyPasswordResetToken Execution)
            if (verifyTokenView.AccountID > 0 || verifyTokenView.Errors.Count() == 0)
            {
                //Generates a Salt and Resets the Password
                resetPasswordView.passwordSalt = security.GenerateSalt();
                if (AccountDataAccess.ResetPassword(resetPasswordView, verifyTokenView, ConnectionString, security))
                {
                    //Post Validation Checks
                    if (resetPasswordView.AccountID == null)
                        return false;
                    if (resetPasswordView.AccountID <= 0)
                        return false;
                    if (resetPasswordView.Errors.Count > 0)
                        return false;
                    return true;
                }
            }

            return false;
        }

        //Update Account Info
        public static Boolean UpdateAccountInfo(AccountViewModel_EditAccountInfo view, AccountViewModel accountView, string ConnectionString)
        {
            Boolean Status = true;
            view.AccountID = accountView.AccountID;

            if (view.firstname == null)
            {
                view.Errors.Add("Please Input a First Name");
            }

            if (view.lastname == null)
            {
                view.Errors.Add("Please Input a Last Name");
            }

            if (view.AccountID == null)
            {
                view.Errors.Add("An Unexpected Error Occured");
            }

            if (view.Errors.Count > 0)
                return false;

            if (view.firstname != accountView.firstname || view.lastname != accountView.lastname)
            {
                Status = AccountDataAccess.EditAccountInfo(view, ConnectionString) ? Status : false;
            }

            if (view.email != accountView.email)
            {
                Status = AccountDataAccess.UpdateAccountEmail(view, ConnectionString) ? Status : false;
            }

            if (Status)
            {
                accountView.firstname = view.firstname;
                accountView.lastname = view.lastname;
                accountView.email = view.email;
            }

            return Status;
        }

        public static Boolean UpdatePassword(AccountViewModel_UpdatePassword view, string ConnectionString, SecurityLogic security)
        {
            view.newsalt = security.GenerateSalt();
            AccountDataAccess.GetSalt(view, ConnectionString);

            if (view.AccountID == null)
            {
                view.Errors.Add("Invalid Account Provided");
            }

            if (view.Email == null)
            {
                view.Errors.Add("Invalid Email Provided");
            }

            if (view.newsalt == null)
            {
                view.Errors.Add("Salt Not Provided");
            }

            if (view.cursalt == null)
            {
                view.Errors.Add("Current Salt not Provided");
            }

            if (view.Errors.Count > 0)
                return false;

            if (AccountDataAccess.UpdatePassword(view, ConnectionString, security))
            {
                return true;
            }
            return false;
        }

        //Roles
        public static Boolean GetUserPermissions(AccountViewModel account, RoleViewModel_Roles roles, string ConnectionString)
        {
            if (account.AccountID == null)
            {
                account.Errors.Add("Invalid Account ID");
            }

            return roles.Errors.Count > 0 || account.Errors.Count > 0 ? false : AccountDataAccess.GetUserPermissions(account, roles, ConnectionString);
        }

        public static Boolean AssignRole(AccountViewModel account, RoleViewModel_Role role, string ConnectionString)
        {
            if (account.AccountID == null)
                account.Errors.Add("Invalid Account ID");

            if (role.RoleID == null)
                role.Errors.Add("Invalid Role ID");

            return role.Errors.Count > 0 || account.Errors.Count > 0 ? false : AccountDataAccess.AssignRole(account, role, ConnectionString);
        }

        public static Boolean EditRole(AccountViewModel account, RoleViewModel_Role role, string ConnectionString)
        {
            if (account.AccountID == null)
                account.Errors.Add("Invalid Account ID");

            if (role.RoleID == null)
                role.Errors.Add("Invalid Role ID");

            return role.Errors.Count > 0 || account.Errors.Count > 0 ? false : AccountDataAccess.EditRole(account, role, ConnectionString);
        }

        public static Boolean DeleteRole(AccountViewModel account, RoleViewModel_Role role, string ConnectionString)
        {
            if (account.AccountID == null)
                account.Errors.Add("Invalid Account ID");

            if (role.RoleID == null)
                role.Errors.Add("Invalid Role ID");

            return role.Errors.Count > 0 || account.Errors.Count > 0 ? false : AccountDataAccess.DeleteRole(account, role, ConnectionString);
        }
    }
}
