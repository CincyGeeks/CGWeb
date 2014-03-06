using CGDataEntities;
using CincyGeeksWebsite.Models;
using CincyGeeksWebsite.Models.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CincyGeeksWebsite.Controllers
{
    public class CGBaseController : Controller
    {
        //
        // GET: /CGBase/

        [AllowAnonymous]
        public PartialViewResult Navigation()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (CGWebEntities entities = new CGWebEntities())
                {
                    NavigationModel navModel = new NavigationModel()
                    {
                        CurrentUserRoles = entities.webpages_Roles.Where(R => R.UserProfiles.Any(P => P.UserName.Equals(User.Identity.Name))).Select(R => R.RoleName).ToList()
                    };
                    return PartialView("_NavigationPartial", navModel);
                }
            }
            else
            {
                NavigationModel navModel = new NavigationModel()
                {
                    CurrentUserRoles = new List<string>()
                };
                return PartialView("_NavigationPartial", navModel);
            }
        }

        [AllowAnonymous]
        public PartialViewResult Avatar(AvatarRenderModel renderModel)
        {
            AvatarPartialViewModel model;
            using (CGWebEntities entities = new CGWebEntities())
            {
                CGDataEntities.UserProfile currentProfile = entities.UserProfiles.Include("webpages_Roles").Where(UPE => UPE.UserId.Equals(renderModel.UserId)).Single();
                string fileName = String.IsNullOrEmpty(currentProfile.AvatarFileName) ? "DefaultAvatar.png" : currentProfile.AvatarFileName;
                model = new AvatarPartialViewModel()
                {
                    AltText = "[CG] |" + currentProfile.UserName + "|",
                    Height = 64,
                    Width = 64,
                    ResourcePath = "~/Content/avatars/" + fileName,
                    StatsInfo = currentProfile.BanExpireDate.HasValue ? "Banned" : "Active",
                    UserName = currentProfile.UserName,
                    UserType = currentProfile.webpages_Roles.Where(R => R.RoleId == currentProfile.webpages_Roles.Min(R2 => R2.RoleId)).Single().RoleName,
                    ImageOnly = renderModel.PictureOnly
                };
                return PartialView("_AvatarPartial", model);
            }
        }

        [AllowAnonymous]
        public PartialViewResult Paginator(PaginatorRenderModel renderModel)
        {
            int currentLinkLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PaginationLinkLimit"]);
            int currentTailingLinkCount = Convert.ToInt32(ConfigurationManager.AppSettings["PaginationTailingLinkCount"]);

            PaginatorPartialModel newPartialModel = new PaginatorPartialModel()
            {
                ActionName = renderModel.ActionName,
                FirstPageParamerterObject = renderModel.CurrentPage > 0 ? renderModel.GetParameterObjectForPage(0) : null,
                LastPageParamerterObject = renderModel.CurrentPage == (renderModel.MaxPage - 1) ? null : renderModel.GetParameterObjectForPage(renderModel.MaxPage - 1),
                PrevPageParamerterObject = renderModel.CurrentPage > 0 ? renderModel.GetParameterObjectForPage(renderModel.CurrentPage - 1) : null,
                NextPageParamerterObject = renderModel.CurrentPage < (renderModel.MaxPage - 1) ? renderModel.GetParameterObjectForPage(renderModel.CurrentPage + 1) : null
            };

            if (renderModel.MaxPage > currentLinkLimit)
            {
                if ((renderModel.MaxPage - renderModel.CurrentPage) >= currentLinkLimit)
                {
                    int leadingGroupCount = currentLinkLimit - currentTailingLinkCount;
                    int startingLeadingValue, endingLeadingValue, startEndingValue, endEndingValue;

                    if (renderModel.CurrentPage < 3)
                    {
                        startingLeadingValue = 0;
                        endingLeadingValue = leadingGroupCount - 1;
                        endEndingValue = renderModel.MaxPage;
                        startEndingValue = renderModel.MaxPage - currentTailingLinkCount;
                    }
                    else
                    {
                        startingLeadingValue = renderModel.CurrentPage - (leadingGroupCount / 2);
                        endingLeadingValue = renderModel.CurrentPage + (leadingGroupCount / 2);
                        endEndingValue = renderModel.MaxPage;
                        startEndingValue = renderModel.MaxPage - currentTailingLinkCount;
                    }

                    newPartialModel.PageLinkValues = new string[currentLinkLimit + 1];
                    newPartialModel.PageLinkParameterObjects = new object[currentLinkLimit + 1];
                    for (int i = 0; i < leadingGroupCount; i++)
                    {
                        newPartialModel.PageLinkValues[i] = ((startingLeadingValue + i) + 1).ToString();
                        if (startingLeadingValue + i != renderModel.CurrentPage)
                            newPartialModel.PageLinkParameterObjects[i] = renderModel.GetParameterObjectForPage(startingLeadingValue + i);
                    }

                    newPartialModel.PageLinkValues[leadingGroupCount] = "---";
                    newPartialModel.PageLinkParameterObjects[leadingGroupCount] = null;

                    for (int i = 1; i < (endEndingValue - startEndingValue) + 1; i++)
                    {
                        newPartialModel.PageLinkValues[leadingGroupCount + i] = ((startEndingValue + (i - 1)) + 1).ToString();
                        newPartialModel.PageLinkParameterObjects[leadingGroupCount + i] = renderModel.GetParameterObjectForPage(startEndingValue + (i - 1));
                    }
                }
                else
                {
                    int startingValue = renderModel.MaxPage - currentLinkLimit;
                    int fillInterval = renderModel.MaxPage - startingValue;
                    newPartialModel.PageLinkValues = new string[fillInterval];
                    newPartialModel.PageLinkParameterObjects = new object[fillInterval];

                    for (int i = 0; i < fillInterval; i++)
                    {
                        newPartialModel.PageLinkValues[i] = ((startingValue + i) + 1).ToString();
                        if((startingValue + i) != renderModel.CurrentPage)
                            newPartialModel.PageLinkParameterObjects[i] = renderModel.GetParameterObjectForPage(startingValue + i);
                    }
                }
            }
            else
            {

                newPartialModel.PageLinkValues = new string[renderModel.MaxPage];
                newPartialModel.PageLinkParameterObjects = new object[renderModel.MaxPage];

                for (int i = 0; i < renderModel.MaxPage; i++)
                {
                    newPartialModel.PageLinkValues[i] = (i + 1).ToString();
                    if (i != renderModel.CurrentPage)
                        newPartialModel.PageLinkParameterObjects[i] = renderModel.GetParameterObjectForPage(i);
                }
            }

            return PartialView("_PaginatorPartial", newPartialModel);
        }
    }
}
