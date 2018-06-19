using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserLibrary.ViewModel
{
    public class RoleViewModel_Role
    {
        public List<RoleViewModel_Permission> Permissions { get; set; } = new List<RoleViewModel_Permission>();

        public int? RoleID { get; set; }
        public String RoleName { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }

    public class RoleViewModel_Roles
    {
        public List<RoleViewModel_Role> roles { get; set; } = new List<RoleViewModel_Role>();
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class RoleViewModel_Permission
    {
        public RoleViewModel_PermissionGroup PermissionGroup { get; set; } = new RoleViewModel_PermissionGroup();

        public int? PermissionID { get; set; }
        public String PermissionName { get; set; }
        public String Action { get; set; }
        public String Controller { get; set; }
        public int? Priority { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }

    public class RoleViewModel_Permissions
    {
        public List<RoleViewModel_Permission> permissions { get; set; } = new List<RoleViewModel_Permission>();
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class RoleViewModel_PermissionGroup
    {
        public int? PermissionGroupID { get; set; }
        public String PermissionGroupName { get; set; }
        public int? Priority { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }

    public class RoleViewModel_PermissionGroups
    {
        public List<RoleViewModel_PermissionGroup> permissiongroups { get; set; } = new List<RoleViewModel_PermissionGroup>();
        public List<string> Errors { get; set; } = new List<string>();
    }
}
