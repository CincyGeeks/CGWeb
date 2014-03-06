using CGDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgWebDataGenerator.BuildJob
{
    public class AnnouncementGenerationJob : IGenerationJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<AnnouncementStruct> _announcements;
        private List<UserProfile> _currentUsers;
        private List<webpages_Roles> _currentRoles;
        
        public void InitalizeGenerationJob()
        {
            log.Info("Init AnnouncementGenerationJob");
            using (CGWebEntities entities = new CGWebEntities())
            {
                _currentUsers = entities.UserProfiles.ToList();
                _currentRoles = entities.webpages_Roles.ToList();

                _announcements = new List<AnnouncementStruct>()
                {
                    new AnnouncementStruct(){
                        Content = "This is the content of announcement #1",
                        CreateDate = DateTime.UtcNow,
                        CreatedByUser = _currentUsers.First().UserId,
                        IsPublic = true,
                        ModifiedByUser = null,
                        ModifiedDate = null,
                        RestrictToRole = null,
                        Title = "Announcement #1"
                    }
                };

                int iterator = 2;
                UserProfile createdBy = _currentUsers.Where(u => u.webpages_Roles.Any(r => r.RoleName.Equals("Announcer"))).First();

                foreach (webpages_Roles role in _currentRoles)
                {
                    _announcements.Add(new AnnouncementStruct()
                    {
                        Content = String.Format("This is the content of announcement #{0}", iterator),
                        CreateDate = DateTime.UtcNow,
                        CreatedByUser = createdBy.UserId,
                        IsPublic = false,
                        ModifiedByUser = null,
                        ModifiedDate = null,
                        RestrictToRole = role.RoleId,
                        Title = role.RoleName + " Announcement."
                    });

                    iterator++;
                }

                _announcements.Add(new AnnouncementStruct()
                {
                    Content = "A modified announcement content!",
                    CreateDate = DateTime.UtcNow.AddMinutes(-5),
                    CreatedByUser = createdBy.UserId,
                    IsPublic = true,
                    ModifiedByUser = createdBy.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    RestrictToRole = null,
                    Title = "A modified announcement."
                });
            }

            log.Info("Created Announcement List:");
            foreach (AnnouncementStruct announce in _announcements)
            {
                log.Info(
                    String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    announce.Title,
                    announce.Content, 
                    announce.CreateDate,
                    announce.CreatedByUser,
                    announce.ModifiedDate,
                    announce.ModifiedByUser,
                    announce.IsPublic,
                    announce.RestrictToRole)
                    );
            }
        }

        public void PerformGenerationJob(CGDataEntities.CGWebEntities webEntities)
        {
            log.Info("Starting AnnouncementGenerationJob...");
            using (CGWebEntities entities = new CGWebEntities())
            {
                foreach (AnnouncementStruct announce in _announcements)
                {
                    entities.Announcements.Add(new Announcement()
                    {
                        Content = announce.Content,
                        CreatedBy = announce.CreatedByUser,
                        CreatedDate = announce.CreateDate,
                        IsPublic = announce.IsPublic,
                        ModifiedBy = announce.ModifiedByUser,
                        ModifiedDate = announce.ModifiedDate,
                        RestrictToRole = announce.RestrictToRole,
                        Title = announce.Title
                    });
                }

                entities.SaveChanges();
            }
            log.Info("AnnouncementGenerationJob Complete...");
        }
    }

    public struct AnnouncementStruct
    {
        public DateTime CreateDate;
        public int CreatedByUser;
        public DateTime? ModifiedDate;
        public int? ModifiedByUser;
        public string Title;
        public string Content;
        public bool IsPublic;
        public int? RestrictToRole;
    }
}
