using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary.ViewModel;
using UserLibrary.DAL;

namespace UserLibrary.BLL
{
    class PermissionLogic
    {
        //Roles
        public static Boolean CreateRole(RoleViewModel_Role view, string ConnectionString)
        {
            if (view.RoleName == null)
                view.Errors.Add("Invalid Role Name");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.CreateRole(view, ConnectionString);
        }

        public static Boolean EditRole(RoleViewModel_Role view, string ConnectionString)
        {
            if (view.RoleID == null)
                view.Errors.Add("Invalid Role ID");

            if (view.RoleName == null)
                view.Errors.Add("Invalid Role Name");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.EditRole(view, ConnectionString);
        }

        public static Boolean DeleteRole(RoleViewModel_Role view, string ConnectionString)
        {
            if (view.RoleID == null)
                view.Errors.Add("Invalid Role ID");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.DeleteRole(view, ConnectionString);
        }

        public static Boolean GetRoless(RoleViewModel_Roles view, string ConnectionString)
        {
            return PermissionDataAccess.GetRoless(view, ConnectionString);
        }

        public static Boolean AddPermissionToRole(RoleViewModel_Role role, RoleViewModel_Permission permission, string ConnectionString)
        {
            if (role.RoleID == null)
                role.Errors.Add("Invalid Role ID");

            if (permission.PermissionID == null)
                permission.Errors.Add("Invalid Permission ID");

            return role.Errors.Count > 0 || permission.Errors.Count > 0 ? false : PermissionDataAccess.AddPermissionToRole(role, permission, ConnectionString);
        }

        public static Boolean GetPermissionsByRole(RoleViewModel_Role view, string ConnectionString)
        {
            if (view.RoleID == null)
            {
                view.Errors.Add("Invalid Role ID");
                return false;
            }

            return view.Errors.Count > 0 ? false : PermissionDataAccess.GetPermissionsByRole(view, ConnectionString);
        }

        public static Boolean RemovePermissionFromRole(RoleViewModel_Role role, RoleViewModel_Permission permission, string ConnectionString)
        {
            if (role.RoleID == null)
            {
                role.Errors.Add("Invalid Role ID");
                return false;
            }

            if (permission.PermissionID == null)
            {
                permission.Errors.Add("Invalid Permission ID");
                return false;
            }

            return role.Errors.Count > 0 || permission.Errors.Count > 0 ? false : PermissionDataAccess.RemovePermissionFromRole(role, permission, ConnectionString);
        }


        //Permissions
        public static Boolean CreatePermission(RoleViewModel_Permission view, string ConnectionString)
        {
            if (view.PermissionName == null)
                view.Errors.Add("Invalid Permission Name");

            if (view.Action == null)
                view.Errors.Add("Invalid Action");

            if (view.Controller == null)
                view.Errors.Add("Invalid Controller");

            if (view.PermissionGroup.PermissionGroupID == null)
                view.Errors.Add("Invalid Permission Group ID");

            if (view.Priority == null)
                view.Errors.Add("Invalid Priority");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.CreatePermission(view, ConnectionString);
        }

        public static Boolean EditPermission(RoleViewModel_Permission view, string ConnectionString)
        {
            if (view.PermissionID == null)
                view.Errors.Add("Invalid Permission ID");

            if (view.PermissionGroup.PermissionGroupID == null)
                view.Errors.Add("Invalid Permission Group ID");

            if (view.PermissionName == null)
                view.Errors.Add("Invalid Permission Name");

            if (view.Action == null)
                view.Errors.Add("Invalid Action");

            if (view.Controller == null)
                view.Errors.Add("Invalid Controller");

            if (view.PermissionGroup.PermissionGroupID == null)
                view.Errors.Add("Invalid Permission Group ID");

            if (view.Priority == null)
                view.Errors.Add("Invalid Priority");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.EditPermission(view, ConnectionString);
        }

        public static Boolean DeletePermission(RoleViewModel_Permission view, string ConnectionString)
        {
            if (view.PermissionID == null)
                view.Errors.Add("Invalid Permission ID");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.DeletePermission(view, ConnectionString);
        }

        public static Boolean GetPermissions(RoleViewModel_Permissions view, string ConnectionString)
        {
            return PermissionDataAccess.GetPermissions(view, ConnectionString);
        }

        //PermissionGroups
        public static Boolean CreatePermissionGroup(RoleViewModel_PermissionGroup view, string ConnectionString)
        {
            if (view.PermissionGroupName == null)
                view.Errors.Add("Invalid Permission Group Name");

            if (view.Priority == null)
                view.Errors.Add("Invalid Priority");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.CreatePermissionGroup(view, ConnectionString);
        }

        public static Boolean EditPermissionGroup(RoleViewModel_PermissionGroup view, string ConnectionString)
        {
            if (view.PermissionGroupID == null)
                view.Errors.Add("Invalid Permission Group ID");

            if (view.PermissionGroupName == null)
                view.Errors.Add("Invalid Permission Group Name");

            if (view.Priority == null)
                view.Errors.Add("Invalid Priority");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.EditPermissionGroup(view, ConnectionString);
        }

        public static Boolean DeletePermissionGroup(RoleViewModel_PermissionGroup view, string ConnectionString)
        {
            if (view.PermissionGroupID == null)
                view.Errors.Add("Invalid Permission Group ID");

            return view.Errors.Count > 0 ? false : PermissionDataAccess.DeletePermissionGroup(view, ConnectionString);
        }

        public static Boolean GetPermissionGroups(RoleViewModel_PermissionGroups view, string ConnectionString)
        {
            return PermissionDataAccess.GetPermissionGroups(view, ConnectionString);
        }
    }
}
