using CGDataEntities;
using CincyGeeksWebsite.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CincyGeeksWebsite.Data;
using WebMatrix.WebData;

namespace CincyGeeksWebsite.Controllers
{
    public class HomeController : CGBaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Announcements";
            ViewBag.Message = "The information you need to game with you buddies.";

            IndexModel newIndexModel = new IndexModel();
            using (CGWebEntities cgweb = new CGWebEntities())
            {
                List<Announcement> announcements;



                if (Request.IsAuthenticated)
                {
                    
                    UserProfile currentProfile = cgweb.UserProfiles.Where(UP => UP.UserName.Equals(User.Identity.Name)).Single();
                    List<int> roleIds = currentProfile.webpages_Roles.Select(WR => WR.RoleId).ToList();
                    announcements = cgweb.Announcements.Where(A => !A.RestrictToRole.HasValue || roleIds.Contains(A.RestrictToRole.Value)).ToList();
                }
                else
                {
                    announcements = cgweb.Announcements.Where(A => !A.RestrictToRole.HasValue).ToList();
                }
                newIndexModel.Announcements = new List<Models.Shared.AnnouncementPartialModel>();
                foreach (Announcement announce in announcements)
                    newIndexModel.Announcements.Add(announce.BuildAnnouncementPartialModel());
            }

            return View(newIndexModel);
        }
    }
}
