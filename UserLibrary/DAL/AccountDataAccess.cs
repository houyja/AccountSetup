using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using UserLibrary.ViewModel;
using UserLibrary.BLL;

namespace UserLibrary.DAL
{
    class AccountDataAccess
    {
        //Register
        public static Boolean Register(AccountViewModel_Registration view, string ConnectionString, SecurityLogic security)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_Registration");
                dataAccess.SetParamater_Input("@Username", view.username, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Email", view.email, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@FirstName", view.firstname, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@LastName", view.lastname, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Hash", security.GenerateSaltedHash(view.password, Encoding.ASCII.GetBytes(view.salt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Salt", view.salt, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@AccountID", SqlDbType.Int);
                dataAccess.SetParamater_Output("@EmailVerificationToken", SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@EmailVerificationKey", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.AccountID = (int?)(dataAccess.GetParamater("@AccountID"));
                view.EmailVerificationToken = (string)(dataAccess.GetParamater("@EmailVerificationToken"));
                view.EmailVerificationKey = (string)(dataAccess.GetParamater("@EmailVerificationKey"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }


        //Verify Email
        public static Boolean ResendVerificationEmail(AccountViewModel_GenerateEmailVerificationToken view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GenerateEmailVerificationTokenWithLogin");
                dataAccess.SetParamater_Input("@LoginID", view.Login, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outGUID", SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outKey", SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outEmail", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.EmailVerificationToken = (string)(dataAccess.GetParamater("@outGUID"));
                view.EmailVerificationKey = (string)(dataAccess.GetParamater("@outKey"));
                view.Email = (string)(dataAccess.GetParamater("@outEmail"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean VerifyEmail(AccountViewModel_VerifyEmail view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_VerifyEmail");
                dataAccess.SetParamater_Input("@TokenID", view.EmailVerificationToken, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@TokenKey", view.EmailVerificationKey, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outAccountID", SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                view.AccountID = (int?)(dataAccess.GetParamater("@outAccountID"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }


        //Login
        public static Boolean Login(AccountViewModel_Login view, AccountViewModel AccountView, string ConnectionString, SecurityLogic security)
        {
            if (!GetSalt(view, ConnectionString))
            {
                AccountView.Errors.Add(view.Errors.First());
                return false;
            }
            else
            {
                if (view.salt != null)
                {
                    try
                    {
                        DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_Login");


                        dataAccess.SetParamater_Input("@LoginID", view.Login, SqlDbType.VarChar, 100);
                        dataAccess.SetParamater_Input("@Hash", security.GenerateSaltedHash(view.password, Encoding.ASCII.GetBytes(view.salt)), SqlDbType.VarChar, 100);

                        dataAccess.SetParamater_Output("@outAccountID", SqlDbType.Int);
                        dataAccess.SetParamater_Output("@outUsername", SqlDbType.VarChar, 100);
                        dataAccess.SetParamater_Output("@outEmail", SqlDbType.VarChar, 100);
                        dataAccess.SetParamater_Output("@outFirstName", SqlDbType.VarChar, 100);
                        dataAccess.SetParamater_Output("@outLastName", SqlDbType.VarChar, 100);
                        dataAccess.SetParamater_Output("@outCretedOn", SqlDbType.DateTime);
                        dataAccess.SetParamater_Output("@outLastEditedOn", SqlDbType.DateTime);

                        dataAccess.ExecuteNonQuery();

                        AccountView.AccountID = (int?)(dataAccess.GetParamater("@outAccountID"));
                        AccountView.username = (string)(dataAccess.GetParamater("@outUsername"));
                        AccountView.email = (string)(dataAccess.GetParamater("@outEmail"));
                        AccountView.firstname = (string)(dataAccess.GetParamater("@outFirstName"));
                        AccountView.lastname = (string)(dataAccess.GetParamater("@outLastName"));
                        AccountView.CreatedOn = (DateTime?)(dataAccess.GetParamater("@outCretedOn"));
                        AccountView.LastEditedOn = (DateTime?)(dataAccess.GetParamater("@outLastEditedOn"));

                        return true;
                    }
                    catch (Exception ex)
                    {
                        AccountView.Errors.Add(ex.Message);
                        return false;
                    }
                }
                else
                {
                    AccountView.Errors.Add(view.Errors.First());
                    return false;
                }
            }
        }

        public static Boolean GetSalt(AccountViewModel_Login view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GetLoginSalt");
                dataAccess.SetParamater_Input("@LoginID", view.Login, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outSalt", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.salt = (string)(dataAccess.GetParamater("@outSalt"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetSalt(AccountViewModel_UpdatePassword view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GetLoginSalt");
                dataAccess.SetParamater_Input("@LoginID", view.Email, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outSalt", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.cursalt = (string)(dataAccess.GetParamater("@outSalt"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }


        //Reset Password
        public static Boolean GeneratePasswordResetToken(AccountViewModel_GeneratePasswordResetToken view, string ConnectionString, SecurityLogic security)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GeneratePasswordResetToken");
                dataAccess.SetParamater_Input("@LoginID", view.LoginID, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Key", security.GenerateSaltedHash(view.key, Encoding.ASCII.GetBytes(view.salt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Salt", view.salt, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outAccountID", SqlDbType.Int);
                dataAccess.SetParamater_Output("@outEmail", SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outGUID", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.AccountID = (int?)(dataAccess.GetParamater("@outAccountID"));
                view.Email = (string)(dataAccess.GetParamater("@outEmail"));
                view.GUID = (string)(dataAccess.GetParamater("@outGUID"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean VerifyPasswordResetToken(AccountViewModel_VerifyPasswordResetToken view, string ConnectionString, SecurityLogic security)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_VerifyPasswordResetToken");
                dataAccess.SetParamater_Input("@TokenID", view.TokenID, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@TokenKey", security.GenerateSaltedHash(view.TokenKey, Encoding.ASCII.GetBytes(view.TokenSalt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outAccountID", SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                view.AccountID = (int?)(dataAccess.GetParamater("@outAccountID"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean VerifyPasswordResetToken_GetSalt(AccountViewModel_VerifyPasswordResetToken view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GetPasswordResetSalt");
                dataAccess.SetParamater_Input("@GUID", view.TokenID, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outSalt", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.TokenSalt = (string)(dataAccess.GetParamater("@outSalt"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean ResetPassword(AccountViewModel_ResetPassword resetPasswordView, AccountViewModel_VerifyPasswordResetToken verifyTokenView, string ConnectionString, SecurityLogic security)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_ResetPassword");
                dataAccess.SetParamater_Input("@TokenID", verifyTokenView.TokenID, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@TokenKey", security.GenerateSaltedHash(verifyTokenView.TokenKey, Encoding.ASCII.GetBytes(verifyTokenView.TokenSalt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Password", security.GenerateSaltedHash(resetPasswordView.password, Encoding.ASCII.GetBytes(resetPasswordView.passwordSalt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Salt", resetPasswordView.passwordSalt, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outAccountID", SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                resetPasswordView.AccountID = (int?)(dataAccess.GetParamater("@outAccountID"));
                return true;
            }
            catch (Exception ex)
            {
                resetPasswordView.Errors.Add(ex.Message);
                return false;
            }
        }


        //UpdateAccount
        public static Boolean EditAccountInfo(AccountViewModel_EditAccountInfo view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_UpdateInfo");
                dataAccess.SetParamater_Input("@AccountID", view.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@FirstName", view.firstname, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@LastName", view.lastname, SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean UpdateAccountEmail(AccountViewModel_EditAccountInfo view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GenerateEmailVerificationToken");
                dataAccess.SetParamater_Input("@AccountID", view.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@UpdatedEmail", view.email, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outGUID", SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@outKey", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.EmailVerificationToken = (string)(dataAccess.GetParamater("@outGUID"));
                view.EmailVerificationKey = (string)(dataAccess.GetParamater("@outKey"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean UpdatePassword(AccountViewModel_UpdatePassword view, string ConnectionString, SecurityLogic security)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_UpdatePassword");
                dataAccess.SetParamater_Input("@AccountID", view.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@CurPassword", security.GenerateSaltedHash(view.curpassword, Encoding.ASCII.GetBytes(view.cursalt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@NewPassword", security.GenerateSaltedHash(view.newpassword, Encoding.ASCII.GetBytes(view.newsalt)), SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@NewSalt", view.newsalt, SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetUpdatePasswordSalt(AccountViewModel_UpdatePassword view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_GetLoginSaltByAccountID");
                dataAccess.SetParamater_Input("@AccountID", view.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Output("@outSalt", SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                view.cursalt = (string)(dataAccess.GetParamater("@outSalt"));
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        //Roles
        public static Boolean GetUserPermissions(AccountViewModel view, RoleViewModel_Roles roles, string ConnectionString)
        {
            List<RoleViewModel_Permission> permissions = new List<RoleViewModel_Permission>();

            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_GetUserPermissions");
                dataAccess.SetParamater_Input("@AccountID", view.AccountID, SqlDbType.Int);
                DataTable dt = dataAccess.ExcuteQuery();


                foreach (DataRow row in dt.Rows)
                {
                    if (roles.roles.FindLast(x => x.RoleID == (int?)row["RoleID"]) == null)
                        roles.roles.Add(new RoleViewModel_Role() { RoleID = (int?)row["RoleID"], RoleName = (string)row["RoleName"], ExpirationDate = (DateTime)row["ExperationDate"] });

                    roles.roles.FindLast(x => x.RoleID == (int?)row["RoleID"]).Permissions.Add(new RoleViewModel_Permission()
                    {
                        PermissionID = (int?)row["PermissionID"],
                        PermissionName = (string)row["PermissionName"],
                        Controller = (string)row["Controller"],
                        Action = (string)row["Action"],
                        Priority = (int?)row["PermissionPriority"],
                        PermissionGroup = new RoleViewModel_PermissionGroup()
                        {
                            PermissionGroupID = (int?)row["PermissionGroupID"],
                            PermissionGroupName = (string)row["PermissionGroupName"],
                            Priority = (int?)row["PermissionGroupPriority"],
                        }
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean AssignRole(AccountViewModel account, RoleViewModel_Role role, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_AssignRole");
                dataAccess.SetParamater_Input("@AccountID", account.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@RoleID", role.RoleID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@ExpirationDate", role.ExpirationDate, SqlDbType.DateTime);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                account.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean EditRole(AccountViewModel account, RoleViewModel_Role role, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_EditAssignedRole");
                dataAccess.SetParamater_Input("@AccountID", account.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@RoleID", role.RoleID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@ExpirationDate", role.ExpirationDate, SqlDbType.DateTime);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                account.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean DeleteRole(AccountViewModel account, RoleViewModel_Role role, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spAccounts_DeleteAssignedRole");
                dataAccess.SetParamater_Input("@AccountID", account.AccountID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@RoleID", role.RoleID, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                account.Errors.Add(ex.Message);
                return false;
            }
        }
    }
}
