using CGDataEntities;
using CincyGeeksWebsite.Models;
using CincyGeeksWebsite.Models.Administrative;
using CincyGeeksWebsite.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CincyGeeksWebsite.Controllers
{
    public class AdministrationController : CGBaseController
    {
        [Authorize(Roles="Administrator")]
        public ActionResult Index()
        {
            ViewBag.Title = "Administration Home";
            ViewBag.Message = "Sweating the small stuff so they don't have to.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UserDashboard()
        {
            List<CGDataEntities.UserProfile> viewRecords;
            using (CGWebEntities entities = new CGWebEntities())
            {
                viewRecords = entities.UserProfiles.ToList();
            }
            return View(viewRecords);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ManageUser(int userId)
        {
            using(CGWebEntities entities = new CGWebEntities())
            {
                
                UserProfile profile = entities.UserProfiles.Where(up => up.UserId.Equals(userId)).Single();

                return View(new CincyGeeksWebsite.Models.Shared.UserProfile(profile));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUser(CincyGeeksWebsite.Models.Shared.UserProfile postedProfile)
        {
            using (CGWebEntities entities = new CGWebEntities())
            {

                UserProfile profile = entities.UserProfiles.Where(up => up.UserId.Equals(postedProfile.UserId)).Single();
                profile.AvatarFileName = postedProfile.AvatarFileName;
                profile.BanExpireDate = postedProfile.BanExpireDate;
                profile.Email = postedProfile.Email;
                profile.ListInDirectory = postedProfile.ListInDirectory;
                profile.PhoneNumber = postedProfile.PhoneNumber;
                profile.Signature = postedProfile.Signature;
                profile.SteamHandle = postedProfile.SteamHandle;
                profile.Timezone = postedProfile.Timezone;
                profile.UserName = postedProfile.UserName;

                try
                {
                    entities.SaveChanges();
                    ViewBag.SaveMessage = "Updated Okay.";
                }
                catch (Exception ex)
                {
                    ViewBag.SaveMessage = String.Format("There was a problem : {0}", ex.Message);
                }

                return View(postedProfile);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult ResetPassword(string username)
        {
            RandomString randomPassword = new RandomString(8);
            string resetToken = WebSecurity.GeneratePasswordResetToken(username, 1);
            WebSecurity.ResetPassword(resetToken, randomPassword.ToString());

            return Content(randomPassword.ToString());
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Root")]
        public ActionResult DeleteUser(int userId)
        {
            using (CGWebEntities entities = new CGWebEntities())
            {
                UserProfile profile = entities.UserProfiles.Where(up => up.UserId.Equals(userId)).Single();
                List<webpages_Roles> roleDropList = profile.webpages_Roles.ToList();

                foreach (webpages_Roles role in roleDropList)
                    entities.webpages_Roles.Remove(role);
                entities.SaveChanges();

                entities.UserProfiles.Remove(profile);
                entities.SaveChanges();
            }

            return Redirect("~/Administration/UserDashboard");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ManageUserRoles(int userId)
        {
            using (CGWebEntities entities = new CGWebEntities())
            {
                UserProfile selectedProfile = entities.UserProfiles.Where(up => up.UserId.Equals(userId)).Single();

                RoleManagementModel returnValue = new RoleManagementModel();
                returnValue.UserId = selectedProfile.UserId;
                foreach (webpages_Roles role in selectedProfile.webpages_Roles)
                {
                    switch (role.RoleName)
                    {
                        case "Root":
                            returnValue.Root = true;
                            break;
                        case "Administrator":
                            returnValue.Administrator = true;
                            break;
                        case "Moderator":
                            returnValue.Moderator = true;
                            break;
                        case "User":
                            returnValue.User = true;
                            break;
                        case "Announcer":
                            returnValue.Announcer = true;
                            break;
                    }
                }

                return View(returnValue);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRoles(RoleManagementModel model)
        {
            using (CGWebEntities entities = new CGWebEntities())
            {
                UserProfile selectedProfile = entities.UserProfiles.Where(up => up.UserId.Equals(model.UserId)).Single();

                if (model.Root)
                {
                    if (!selectedProfile.webpages_Roles.Any(r => r.RoleName.Equals("Root")))
                    {
                        selectedProfile.webpages_Roles.Add(entities.webpages_Roles.Where(r => r.RoleName.Equals("Root")).Single());
                    }
                }
                else
                {
                    webpages_Roles roleTest = selectedProfile.webpages_Roles.Where(r => r.RoleName.Equals("Root")).SingleOrDefault();
                    if (roleTest != null)
                    {
                        selectedProfile.webpages_Roles.Remove(roleTest);
                    }
                }

                if (model.Administrator)
                {
                    if (!selectedProfile.webpages_Roles.Any(r => r.RoleName.Equals("Administrator")))
                    {
                        selectedProfile.webpages_Roles.Add(entities.webpages_Roles.Where(r => r.RoleName.Equals("Administrator")).Single());
                    }
                }
                else
                {
                    webpages_Roles roleTest = selectedProfile.webpages_Roles.Where(r => r.RoleName.Equals("Administrator")).SingleOrDefault();
                    if (roleTest != null)
                    {
                        selectedProfile.webpages_Roles.Remove(roleTest);
                    }
                }

                if (model.Moderator)
                {
                    if (!selectedProfile.webpages_Roles.Any(r => r.RoleName.Equals("Moderator")))
                    {
                        selectedProfile.webpages_Roles.Add(entities.webpages_Roles.Where(r => r.RoleName.Equals("Moderator")).Single());
                    }
                }
                else
                {
                    webpages_Roles roleTest = selectedProfile.webpages_Roles.Where(r => r.RoleName.Equals("Moderator")).SingleOrDefault();
                    if (roleTest != null)
                    {
                        selectedProfile.webpages_Roles.Remove(roleTest);
                    }
                }

                if (model.User)
                {
                    if (!selectedProfile.webpages_Roles.Any(r => r.RoleName.Equals("User")))
                    {
                        selectedProfile.webpages_Roles.Add(entities.webpages_Roles.Where(r => r.RoleName.Equals("User")).Single());
                    }
                }
                else
                {
                    webpages_Roles roleTest = selectedProfile.webpages_Roles.Where(r => r.RoleName.Equals("User")).SingleOrDefault();
                    if (roleTest != null)
                    {
                        selectedProfile.webpages_Roles.Remove(roleTest);
                    }
                }

                if (model.Announcer)
                {
                    if (!selectedProfile.webpages_Roles.Any(r => r.RoleName.Equals("Announcer")))
                    {
                        selectedProfile.webpages_Roles.Add(entities.webpages_Roles.Where(r => r.RoleName.Equals("Announcer")).Single());
                    }
                }
                else
                {
                    webpages_Roles roleTest = selectedProfile.webpages_Roles.Where(r => r.RoleName.Equals("Announcer")).SingleOrDefault();
                    if (roleTest != null)
                    {
                        selectedProfile.webpages_Roles.Remove(roleTest);
                    }
                }

                try
                {
                    entities.SaveChanges();
                    ViewBag.SaveMessage = "Save Completed";
                }
                catch (Exception ex)
                {
                    ViewBag.SaveMessage = String.Format("There was a problem saving : {0}", ex.Message);
                }

                return View("ManageUserRoles", model);
            }
        }
    }
}
