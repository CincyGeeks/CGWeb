using CGDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace CgWebDataGenerator.BuildJob
{
    public class UserSchemaGenerationJob : IGenerationJob
    {
        private const string DEVELOPER_PASSWORD = "123";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //id, username, email, timezone, phone, steam, list, sig, avatarFileName
        private List<UserProfileStruct> _userProfileList;
        private Dictionary<int, string> _roles;
        private List<KeyValuePair<int, int>> _usersInRoles;

        public void InitalizeGenerationJob()
        {
            log.Info("Init UserSchemaGenerationJob");
            TimeZoneInfo eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            TimeZoneInfo mountain = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");
            TimeZoneInfo hawaiian = TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time");
            TimeZoneInfo central = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            log.Info("Created TimeZoneInfo resources.");

            #region Roles
            _roles = new Dictionary<int, string>()
            {
                {1, "Root"},
                {2, "Administrator"},
                {3, "Moderator"},
                {4, "User"},
                {5, "Announcer"}
            };
            log.Info("Created Roles Dictionary:");
            foreach (KeyValuePair<int, string> kvPair in _roles)
                log.Info(String.Format("{0} -> {1}", kvPair.Key, kvPair.Value));
            #endregion

            #region User Profile Config
            _userProfileList = new List<UserProfileStruct>(){
                new UserProfileStruct(){
                    Username =          "rootUser", 
                    Email =             "root@example.com", 
                    Timezone =          eastern.Id, 
                    Phone =             "(513)555-1234",
                    SteamHandle =       "superUserSteam",
                    ListInDirectory =   true,
                    AvatarFileName =    null,
                    BanExpireDate =     null
                },
                new UserProfileStruct(){
                   Username =        "Administrator",
                   Email =           "admin@example.com",
                   Timezone =        mountain.Id,
                   Phone =           "(513)555-9876",
                   SteamHandle =     "adminUserSteam",
                   ListInDirectory = true,
                   AvatarFileName =  null,
                   BanExpireDate =  null
                },
                new UserProfileStruct(){
                    Username =       "Moderator",
                    Email =          "mod@example.com",
                    Timezone =       hawaiian.Id,
                    Phone =          "(513)555-1234",
                    SteamHandle =    "modUserSteam",
                    ListInDirectory =true,
                    AvatarFileName = null,
                    BanExpireDate =  null
                },
                new UserProfileStruct(){
                    Username =       "AUser",
                    Email =          "user@example.com",
                    Timezone =       central.Id,
                    Phone =          "(513)555-1234",
                    SteamHandle =    "userSteam",
                    ListInDirectory =true,
                    AvatarFileName = null,
                    BanExpireDate =  null
                },
                new UserProfileStruct(){
                    Username =       "bannedUser",
                    Email =          "banned@example.com",
                    Timezone =       eastern.Id,
                    Phone =          "(513)555-1234",
                    SteamHandle =    "bannedSteam",
                    ListInDirectory =true,
                    AvatarFileName = null,
                    BanExpireDate =  DateTime.MaxValue
                }
            };
            log.Info("Created UserProfile List:");
            foreach (UserProfileStruct profile in _userProfileList)
                log.Info(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", 
                    profile.Username, 
                    profile.Email, 
                    profile.Timezone, 
                    profile.Phone, 
                    profile.SteamHandle, 
                    profile.ListInDirectory, 
                    profile.AvatarFileName, 
                    profile.BanExpireDate));
            #endregion

            #region Users In Roles
            _usersInRoles = new List<KeyValuePair<int, int>>()
            {
                //index in above list and key of role dict
                new KeyValuePair<int, int>(0,1),
                new KeyValuePair<int, int>(0,2),
                new KeyValuePair<int, int>(0,3),
                new KeyValuePair<int, int>(0,4),
                new KeyValuePair<int, int>(0,5),
                new KeyValuePair<int, int>(1,2),
                new KeyValuePair<int, int>(1,3),
                new KeyValuePair<int, int>(1,4),
                new KeyValuePair<int, int>(2,3),
                new KeyValuePair<int, int>(2,4),
                new KeyValuePair<int, int>(2,5),
                new KeyValuePair<int, int>(3,4),
                new KeyValuePair<int, int>(4,4)
            };
            log.Info("Created UsersInRoles List:");
            foreach (KeyValuePair<int, int> kvPair in _usersInRoles)
                log.Info(String.Format("{0} -> {1}", _userProfileList[kvPair.Key].Username, _roles[kvPair.Value]));
            #endregion
        }

        public void PerformGenerationJob(CGDataEntities.CGWebEntities webEntities)
        {
            log.Info("Starting UserSchemaGenerationJob...");
            CreateNewRoles(webEntities);
            CreateNewUsers();
            AsignUserRoles(webEntities);
            log.Info("UserSchemaGenerationJob Complete!");
        }


        private void CreateNewRoles(CGDataEntities.CGWebEntities webEntities)
        {
            //wont work without HttpContext :(
            //SimpleRoleProvider provider = new SimpleRoleProvider();
            //foreach (KeyValuePair<int, string> roleKV in _roles)
            //    provider.CreateRole(roleKV.Value);

            foreach (KeyValuePair<int, string> roleKV in _roles)
                webEntities.webpages_Roles.Add(new webpages_Roles()
                {
                    RoleName = roleKV.Value
                });
            webEntities.SaveChanges();
            log.Info("Added roles to system.");
        }

        private void CreateNewUsers()
        {
            for (int i = 0; i < _userProfileList.Count(); i++)
            {
                UserProfileStruct currentProfile = _userProfileList[i];
                WebSecurity.CreateUserAndAccount(
                    currentProfile.Username, 
                    DEVELOPER_PASSWORD,
                    new
                    {
                        Email = currentProfile.Email,
                        Timezone = currentProfile.Timezone,
                        PhoneNumber = currentProfile.Phone,
                        SteamHandle = currentProfile.SteamHandle,
                        ListInDirectory = currentProfile.ListInDirectory,
                        AvatarFileName = currentProfile.AvatarFileName,
                        BanExpireDate = currentProfile.BanExpireDate
                    });
            }
            log.Info("Added Users to system.");
        }

        private void AsignUserRoles(CGDataEntities.CGWebEntities webEntities)
        {
            SimpleRoleProvider provider = new SimpleRoleProvider();

            List<int> roleKeyList = _usersInRoles.Select(T => T.Value).Distinct().ToList();
            foreach (int roleKey in roleKeyList)
            {
                string roleName = _roles[roleKey];
                int[] usersInRoleIndexes = _usersInRoles.Where(kv => kv.Value.Equals(roleKey)).Select(kv => kv.Key).ToArray();
                string[] userNames = new string[usersInRoleIndexes.Count()];

                for (int i = 0; i < usersInRoleIndexes.Count(); i++)
                {
                    userNames[i] = _userProfileList[usersInRoleIndexes[i]].Username;
                }

                //wont work without HttpContext :(
                //provider.AddUsersToRoles(userNames, roleNames);
                webpages_Roles currentRole = webEntities.webpages_Roles.Where(R => R.RoleName.Equals(roleName)).Single();
                foreach (UserProfile profile in webEntities.UserProfiles.Where(UP => userNames.Contains(UP.UserName)))
                    profile.webpages_Roles.Add(currentRole);
                webEntities.SaveChanges();
            }

            log.Info("Added Roles to system.");
        }
    }

    public struct UserProfileStruct
    {
        public string Username;
        public string Email;
        public string Timezone;
        public string Phone;
        public string SteamHandle;
        public bool ListInDirectory;
        public string AvatarFileName;
        public DateTime? BanExpireDate;
    }
}
