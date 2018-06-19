using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary.ViewModel;
using UserLibrary.BLL;
using System.Data;
using System.Configuration;

namespace UserLibrary.DAL
{
    class PermissionDataAccess
    {
        //Roles
        public static Boolean CreateRole(RoleViewModel_Role view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_CreateRole");
                dataAccess.SetParamater_Input("@RoleName", view.RoleName, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Output("@RoleID", SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                view.RoleID = (int?)dataAccess.GetParamater("@RoleID");

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean EditRole(RoleViewModel_Role view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_EditRole");
                dataAccess.SetParamater_Input("@RoleID", view.RoleID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@RoleName", view.RoleName, SqlDbType.VarChar, 100);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean DeleteRole(RoleViewModel_Role view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_DeleteRole");
                dataAccess.SetParamater_Input("@RoleID", view.RoleID, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetRoless(RoleViewModel_Roles view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_GetRoles");
                DataTable dt = dataAccess.ExcuteQuery();


                foreach (DataRow row in dt.Rows)
                {
                    view.roles.Add(new RoleViewModel_Role());
                    view.roles.Last().RoleID = (int?)row["RoleID"];
                    view.roles.Last().RoleName = (string)row["RoleName"];
                }
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean AddPermissionToRole(RoleViewModel_Role role, RoleViewModel_Permission permission, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_AddPermissionToRole");
                dataAccess.SetParamater_Input("@RoleID", role.RoleID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@PermissionID", permission.PermissionID, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                role.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetPermissionsByRole(RoleViewModel_Role view, string ConnectionString)
        {
            List<RoleViewModel_Permission> permissions = new List<RoleViewModel_Permission>();
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_GetPermissionsByRole");
                dataAccess.SetParamater_Input("@RoleID", view.RoleID, SqlDbType.Int);
                DataTable dt = dataAccess.ExcuteQuery();


                foreach (DataRow row in dt.Rows)
                {
                    view.Permissions.Add(new RoleViewModel_Permission()
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

        public static Boolean RemovePermissionFromRole(RoleViewModel_Role role, RoleViewModel_Permission permission, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_RemovePermissionFromRole");
                dataAccess.SetParamater_Input("@RoleID", role.RoleID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@PermissionID", permission.PermissionID, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                role.Errors.Add(ex.Message);
                return false;
            }
        }


        //Permissions
        public static Boolean CreatePermission(RoleViewModel_Permission view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_CreatePermission");
                dataAccess.SetParamater_Input("@PermissionName", view.PermissionName, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Action", view.Action, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Controller", view.Controller, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@PermissionGroupID", view.PermissionGroup.PermissionGroupID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@Priority", view.Priority, SqlDbType.Int);
                dataAccess.SetParamater_Output("@PermissionID", SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                view.PermissionID = (int?)dataAccess.GetParamater("@PermissionID");
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean EditPermission(RoleViewModel_Permission view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_EditPermission");
                dataAccess.SetParamater_Input("@PermissionID", view.PermissionID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@PermissionName", view.PermissionName, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Action", view.Action, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@Controller", view.Controller, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@PermissionGroupID", view.PermissionGroup.PermissionGroupID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@Priority", view.Priority, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean DeletePermission(RoleViewModel_Permission view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_DeletePermission");
                dataAccess.SetParamater_Input("@PermissionID", view.PermissionID, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetPermissions(RoleViewModel_Permissions view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_GetPermissions");
                DataTable dt = dataAccess.ExcuteQuery();


                foreach (DataRow row in dt.Rows)
                {
                    view.permissions.Add(new RoleViewModel_Permission()
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


        //PermissionGroups
        public static Boolean CreatePermissionGroup(RoleViewModel_PermissionGroup view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_CreatePermissionGroup");
                dataAccess.SetParamater_Input("@PermissionGroupName", view.PermissionGroupName, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@PermissionPriority", view.Priority, SqlDbType.Int);
                dataAccess.SetParamater_Output("@PermissionGroupID", SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                view.PermissionGroupID = (int?)dataAccess.GetParamater("@PermissionGroupID");

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean EditPermissionGroup(RoleViewModel_PermissionGroup view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_EditPermissionGroup");
                dataAccess.SetParamater_Input("@PermissionGroupID", view.PermissionGroupID, SqlDbType.Int);
                dataAccess.SetParamater_Input("@PermissionGroupName", view.PermissionGroupName, SqlDbType.VarChar, 100);
                dataAccess.SetParamater_Input("@PermissionGroupPriority", view.Priority, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean DeletePermissionGroup(RoleViewModel_PermissionGroup view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_DeletePermissionGroup");
                dataAccess.SetParamater_Input("@PermissionGroupID", view.PermissionGroupID, SqlDbType.Int);
                dataAccess.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetPermissionGroups(RoleViewModel_PermissionGroups view, string ConnectionString)
        {
            try
            {
                DataAccess dataAccess = new DataAccess(ConnectionString, "spRoles_GetPermissionGroups");
                DataTable dt = dataAccess.ExcuteQuery();


                foreach (DataRow row in dt.Rows)
                {
                    view.permissiongroups.Add(new RoleViewModel_PermissionGroup()
                    {
                        PermissionGroupID = (int?)row["PermissionGroupID"],
                        PermissionGroupName = (string)row["PermissionGroupName"],
                        Priority = (int?)row["Priority"]
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

    }
}
