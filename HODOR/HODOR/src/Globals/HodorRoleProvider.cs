using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HODOR.src.DAO;
using HODOR.src.Globals;

namespace HODOR.src.Globals
{
    public sealed class HodorRoleProvider : System.Web.Security.RoleProvider
    {
        private const string NAME = "HodorRoleProvider";

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(NAME, config);
        }

        public override string ApplicationName
        {
            get { return NAME;}
            set { /*empty by design*/ }
        }

        public override void AddUsersToRoles(string[] customerNumbers, string[] rolenames)
        {
            if ((rolenames == null) || (customerNumbers == null) || (rolenames.Length != 1))
                throw new ArgumentException("One of the values supplied was invalid");

            Rolle role = RolleDAO.getRoleByNameOrNull(rolenames[0]);

            foreach (string customerNumber in customerNumbers)
            {
                Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(customerNumber);
                if (user == null) continue;

                user.Rolle = role;
            }

            HodorGlobals.save();
        }

        public override void CreateRole(string rolename)
        {
            RolleDAO.createAndGetRolle(rolename);
        }

        //pun is hiding in here
        public override bool DeleteRole(string rolename, bool throwUpOnPopulatedRole)
        {
            if (GetUsersInRole(rolename).Length != 0)
            {
                if (throwUpOnPopulatedRole)
                {
                    throw new ArgumentException("Role still contains users!");
                }
            }
            else
            {
                RolleDAO.deleteRole(RolleDAO.getRoleByNameOrNull(rolename));
                return true;
            }

            return false;
        }

        public override string[] GetAllRoles()
        {
            List<Rolle> roles = RolleDAO.getAllRoles();
            string[] resultRoleArray = new string[roles.Count];

            for (int i = 0; i < roles.Count; i++)
            {
                resultRoleArray[i] = roles[i].Rollenname;
            }

            return resultRoleArray;
        }

        public override string[] GetRolesForUser(string customernumber)
        {
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(customernumber);

            if (user == null)
            {
                return null;
            }

            string[] result = { user.Rolle.Rollenname };

            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            Rolle role = RolleDAO.getRoleByNameOrNull(roleName);

            List<Benutzer> usersInRole = RolleDAO.getAllUsersWithRole(role);

            string[] resultUsersInRole = new string[usersInRole.Count];

            int i = 0;
            foreach (Benutzer user in usersInRole)
            {
                resultUsersInRole[i] = user.NutzerNr;
                i++;
            }

            return resultUsersInRole;
        }

        public override bool IsUserInRole(string customernumber, string roleName)
        {
            Benutzer user = BenutzerDAO.getUserByKundenNrOrNull(customernumber);

            return (user != null && user.Rolle.Rollenname.Equals(roleName));
        }

        public override void RemoveUsersFromRoles(string[] customernumbers, string[] roleNames)
        {
            //by design: Because users should always have a role
            throw new NotImplementedException("A user has to have a role!");
        }

        public override bool RoleExists(string roleName)
        {
            return RolleDAO.getRoleByNameOrNull(roleName) != null;
        }

        public override string[] FindUsersInRole(string roleName, string customernumberToMatch)
        {
            Rolle role = RolleDAO.getRoleByNameOrNull(roleName);
            if (role == null)
            {
                return null;
            }

            List<Benutzer> users = RolleDAO.findUsersInRole(role, customernumberToMatch);
            string[] resultUserArray = new string[users.Count];

            int i = 0;
            foreach (Benutzer user in users)
            {
                resultUserArray[i] = user.NutzerNr;
                i++;
            }

            return resultUserArray;
        }

        public static bool isSupportAllowed(Benutzer user)
        {
            if (user.Rolle.Rollenname.Equals("Administrator") || user.Rolle.Rollenname.Equals("Support") || user.Rolle.Rollenname.Equals("Useradmin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isAdminAllowed(Benutzer user)
        {
            if (user.Rolle.Rollenname.Equals("Administrator") || user.Rolle.Rollenname.Equals("Useradmin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}