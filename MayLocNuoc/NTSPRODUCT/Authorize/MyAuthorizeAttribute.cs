
using NTSPRODUCT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace NTSPRODUCT
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool check = false;
            var loginProfile = ClassExten.GetCokies();
            if (loginProfile != null)
            {
                if (!string.IsNullOrEmpty(loginProfile.SecurityKey))
                {
                    check = true;
                }
            }
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var id = httpContext.User.Identity.Name;
                check = CheckVersionLogin(id);
            }
            if (check == false) httpContext.Response.Redirect("~/Admins/Login", true);
            return check;
        }

        public bool CheckVersionLogin(string id)
        {
            var loginProfile = ClassExten.GetCokies();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            if (identity != null)
            {
                var claimVersion = identity.Claims.Where(t => t.Type.Contains("version")).FirstOrDefault();
                if (claimVersion != null)
                {
                    var version = claimVersion.Value;
                    if (loginProfile != null && !string.IsNullOrEmpty(loginProfile.SecurityKey) && id.Equals(loginProfile.Email))
                    {
                        if (loginProfile.SecurityKey.Equals(version)) return true;
                    }

                }
            }
            return false;
        }
        /// <summary>
        /// Check quyền
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="rolesUser"></param>
        /// <returns></returns>
        private bool CheckRoles(List<string> roles, List<string> rolesUser)
        {
            foreach (var item in roles)
            {
                if (rolesUser.Where(r => r.Equals(item)).Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckUsers(string[] users, string userName)
        {
            var countUser = users.Where(r => r.Equals(userName));
            if (countUser.Count() > 0)
            {
                return true;
            }
            return false;
        }

    }
}