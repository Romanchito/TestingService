using System;
using System.Linq;
using System.Web.Security;
using TestingService.Models.ContextModels;
using TestingService.Models.EntityModels;

namespace TestingService.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

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

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (Context db = new Context())
            {
                User user = db.Users.FirstOrDefault(x => x.Email == username);
                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if (userRole != null) roles = new string[] { userRole.Name };
                }
            }

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;

            using (Context db = new Context())
            {
                User user = db.Users.FirstOrDefault(x => x.Email == username);
                if(user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if (userRole != null && userRole.Name == roleName) outputResult = true;
                }
            }

            return outputResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}