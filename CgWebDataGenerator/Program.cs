using CGDataEntities;
using CgWebDataGenerator.BuildJob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace CgWebDataGenerator
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly IGenerationJob[] _generationActions = new IGenerationJob[]
        {
            new ClearCurrentDataGenerationJob(),
            new UserSchemaGenerationJob(),
            new ServiceGenerationJob(),
            new AnnouncementGenerationJob(),
            new FourmGenerationJob()
        };

        static void Main(string[] args)
        {
            log.Info("Checking developer lacking caffinee...");
            Console.WriteLine("This will clear and factory reset the database.");
            Console.WriteLine("!!!Any change you have made manually will disapear!!!");
            Console.WriteLine("You sure? (y to DELETE ALL THE THINGS!)");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                log.Info("Caffinee levels confirmed. Starting CgWebDataGenerator...");

                Stopwatch generationTimer = new Stopwatch();
                generationTimer.Start();

                log.Info("Calling WebSecurity.InitializeDatabaseConnection()...");
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: false);

                log.Info("Opening database context.");
                using (CGWebEntities entities = new CGWebEntities())
                {
                    log.Info("Stepping generation actions...");
                    for (int i = 0; i < _generationActions.Count(); i++)
                    {
                        log.Info(String.Format("Trying initalization for type {0}", _generationActions[i].GetType()));
                        try
                        {
                            _generationActions[i].InitalizeGenerationJob();
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("There was a problem initalizing type {0}", _generationActions[i].GetType()), ex);
                            HangForUserAndExit(1);
                        }

                        log.Info(String.Format("Trying generationAction for type {0}", _generationActions[i].GetType()));
                        try
                        {
                            _generationActions[i].PerformGenerationJob(entities);
                        }
                        catch (Exception ex)
                        {
                            log.Error(String.Format("There was a problem in generationAction for type {0}", _generationActions[i].GetType()), ex);
                            HangForUserAndExit(1);
                        }
                    }
                    log.Info("All generation modules performed without error.");
                }
                log.Info("Disposed of database context");
                generationTimer.Stop();

                log.Info(String.Format("CgWebDataGenerator has ended. Generation took: {0}", generationTimer.Elapsed.ToString()));
                
                HangForUserAndExit(0);
            }
            else
            {
                Console.WriteLine("I'll get you next time Gadget...");
                Environment.Exit(0);
            }
        }

        private static void HangForUserAndExit(int exitCode)
        {
            log.Info("Prompting for user interaction...");
            Console.WriteLine("Data generated press any key to close...");
            Console.ReadKey();
            Environment.Exit(exitCode);
        }
    }
}
