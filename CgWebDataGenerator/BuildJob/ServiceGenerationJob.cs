using CGDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgWebDataGenerator.BuildJob
{
    public class ServiceGenerationJob : IGenerationJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<string, Guid> _servicesDict;
        private List<KeyValuePair<string, string>> _serviceRoleRelationships;

        public void InitalizeGenerationJob()
        {
            log.Info("Init ServiceGenerationJob");

            _servicesDict = new Dictionary<string, Guid>()
            {
                {"Administration", Guid.NewGuid()},
                {"Announcements", Guid.NewGuid()},
                {"Forums", Guid.NewGuid()},
            };
            log.Info("Created Services Dictionary:");
            foreach (KeyValuePair<string, Guid> kvPair in _servicesDict)
                log.Info(String.Format("{0} -> {1}", kvPair.Key, kvPair.Value));


            _serviceRoleRelationships = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Administration", "Root"),
                new KeyValuePair<string, string>("Administration", "Administrator"),
                new KeyValuePair<string, string>("Announcements", "Root"),
                new KeyValuePair<string, string>("Announcements", "Announcer"),
                new KeyValuePair<string, string>("Forums", "User"),
            };
            log.Info("Created ServiceRoleRelationships List:");
            foreach (KeyValuePair<string, string> kvPair in _serviceRoleRelationships)
                log.Info(String.Format("{0} -> {1}", kvPair.Key, kvPair.Value));
        }

        public void PerformGenerationJob(CGDataEntities.CGWebEntities webEntities)
        {
            log.Info("Starting ServiceGenerationJob...");
            AddServices(webEntities);
            AddServiceRoleRelationships(webEntities);
            log.Info("ServiceGenerationJob Complete...");
        }

        private void AddServices(CGDataEntities.CGWebEntities webEntities)
        {
            foreach (KeyValuePair<string, Guid> service in _servicesDict)
                webEntities.Services.Add(new CGDataEntities.Service()
                {
                    ServiceId = service.Value,
                    ServiceName = service.Key
                });
            webEntities.SaveChanges();
            log.Info("Added services to system.");
        }

        private void AddServiceRoleRelationships(CGDataEntities.CGWebEntities webEntities)
        {
            foreach (KeyValuePair<string, Guid> service in _servicesDict)
            {
                Service currentService = webEntities.Services.Where(S => S.ServiceId.Equals(service.Value)).Single();
                foreach (KeyValuePair<string, string> relationship in _serviceRoleRelationships)
                {
                    webpages_Roles currentRole = webEntities.webpages_Roles.Where(R => R.RoleName.Equals(relationship.Value)).Single();
                    currentService.Service_Roles.Add(new Service_Roles()
                    {
                        RoleId = currentRole.RoleId,
                        ServiceId = currentService.ServiceId,
                        ServiceRoleId = Guid.NewGuid()
                    });
                }
            }
            webEntities.SaveChanges();
            log.Info("Added services role relationships to system.");
        }
    }
}
