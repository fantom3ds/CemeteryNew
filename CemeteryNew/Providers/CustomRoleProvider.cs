using CemeteryNew.DataAccessLayer;
using CemeteryNew.Models;
using System;
using System.Linq;
using System.Web.Security;

namespace CemeteryNew.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        UserDal Dao = new UserDal();

        public override string[] GetRolesForUser(string username)
        {
            return Dao.GetRolesForUser(username);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return Dao.IsUserInRole(username, roleName);
        }

        #region NotImplemented

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}