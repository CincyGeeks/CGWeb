using CGDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgWebDataGenerator.BuildJob
{
    class ClearCurrentDataGenerationJob : IGenerationJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void InitalizeGenerationJob()
        {
            
        }

        public void PerformGenerationJob(CGDataEntities.CGWebEntities webEntities)
        {
            log.Info("Starting data purge...");

            List<ForumReply> replysToRemove = webEntities.ForumReplies.ToList();
            webEntities.ForumReplies.RemoveRange(replysToRemove);
            webEntities.SaveChanges();

            List<ForumThread> threadsToRemove = webEntities.ForumThreads.ToList();
            webEntities.ForumThreads.RemoveRange(threadsToRemove);
            webEntities.SaveChanges();

            List<ForumTopic> topicsToRemove = webEntities.ForumTopics.ToList();
            webEntities.ForumTopics.RemoveRange(topicsToRemove);
            webEntities.SaveChanges();

            List<Forum> forumsToRemove = webEntities.Forums.ToList();
            webEntities.Forums.RemoveRange(forumsToRemove);
            webEntities.SaveChanges();

            List<Announcement> announcementsToRemove = webEntities.Announcements.ToList();
            webEntities.Announcements.RemoveRange(announcementsToRemove);
            webEntities.SaveChanges();
            
            foreach (UserProfile profile in webEntities.UserProfiles)
            {
                List<webpages_Roles> roles = profile.webpages_Roles.ToList();
                foreach (webpages_Roles role in roles)
                    profile.webpages_Roles.Remove(role);
            }
            webEntities.SaveChanges();

            List<Service_Roles> serviceRoleToRemove = webEntities.Service_Roles.ToList();
            webEntities.Service_Roles.RemoveRange(serviceRoleToRemove);

            webEntities.SaveChanges();

            List<UserProfile> profilesToRemove = webEntities.UserProfiles.ToList();
            webEntities.UserProfiles.RemoveRange(profilesToRemove);

            List<webpages_OAuthMembership> oAuthToRemove = webEntities.webpages_OAuthMembership.ToList();
            webEntities.webpages_OAuthMembership.RemoveRange(oAuthToRemove);

            List<webpages_Membership> membershipToRemove = webEntities.webpages_Membership.ToList();
            webEntities.webpages_Membership.RemoveRange(membershipToRemove);

            List<webpages_Roles> rolesToRemove = webEntities.webpages_Roles.ToList();
            webEntities.webpages_Roles.RemoveRange(rolesToRemove);

            List<Service> servicesToRemove = webEntities.Services.ToList();
            webEntities.Services.RemoveRange(servicesToRemove);

            webEntities.SaveChanges();

            log.Info("Current user data purged! Hope you didn't need that :O");
        }
    }
}
