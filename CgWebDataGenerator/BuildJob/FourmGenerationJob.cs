using CGDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CgWebDataGenerator.BuildJob
{
    public class FourmGenerationJob : IGenerationJob
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<UserProfile> _currentUsers;
        private List<ForumStruct> _forums;
        private List<ForumTopicStruct> _forumTopics;
        private List<ForumThreadStruct> _forumThreads;
        private List<ForumReplyStruct> _forumReplys;

        public void InitalizeGenerationJob()
        {
            log.Info("Init FourmGenerationJob");

            Random rand = new Random((int)DateTime.UtcNow.Ticks);

            using (CGWebEntities entities = new CGWebEntities())
            {
                _currentUsers = entities.UserProfiles.ToList();
            }

            _forums = new List<ForumStruct>{
                new ForumStruct()
                {
                    ForumId = Guid.NewGuid(),
                    ForumTitle = "Public Forum",
                    IsPublic = true
                },
                new ForumStruct()
                {
                    ForumId = Guid.NewGuid(),
                    ForumTitle = "Gaming",
                    IsPublic = false
                },
                new ForumStruct()
                {
                    ForumId = Guid.NewGuid(),
                    ForumTitle = "Meta",
                    IsPublic = false
                },
                new ForumStruct()
                {
                    ForumId = Guid.NewGuid(),
                    ForumTitle = "Website Support",
                    IsPublic = true
                }
            };
            log.Info("Created Forum List:");
            foreach (ForumStruct forum in _forums)
            {
                log.Info(
                    String.Format("{0},{1},{2}",
                    forum.ForumId,
                    forum.ForumTitle,
                    forum.IsPublic)
                    );
            }

            _forumTopics = new List<ForumTopicStruct>()
            {
                new ForumTopicStruct(){
                   CreatedBy =  _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                   CreatedOn = DateTime.UtcNow,
                   Description = "Discussion for EvE Online goes here.",
                   ForumId = _forums[1].ForumId,
                   IsPublic = false,
                   Title = "EvE Online",
                   TopicId = Guid.NewGuid()
                },
                new ForumTopicStruct(){
                   CreatedBy =  _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                   CreatedOn = DateTime.UtcNow,
                   Description = "Discussion for Counter Strike:Global Offensive goes here.",
                   ForumId = _forums[1].ForumId,
                   IsPublic = false,
                   Title = "Counter Strike:Global Offensive",
                   TopicId = Guid.NewGuid()
                },
                new ForumTopicStruct(){
                   CreatedBy =  _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                   CreatedOn = DateTime.UtcNow,
                   Description = "Discussion for the public goes here.",
                   ForumId = _forums[0].ForumId,
                   IsPublic = true,
                   Title = "Public Threads",
                   TopicId = Guid.NewGuid()
                },
                new ForumTopicStruct(){
                   CreatedBy =  _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                   CreatedOn = DateTime.UtcNow,
                   Description = "Discussion for the BOINC Team goes here.",
                   ForumId = _forums[2].ForumId,
                   IsPublic = false,
                   Title = "BOINC Team",
                   TopicId = Guid.NewGuid()
                },
                new ForumTopicStruct(){
                   CreatedBy =  _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                   CreatedOn = DateTime.UtcNow,
                   Description = "Report website bugs here.",
                   ForumId = _forums[3].ForumId,
                   IsPublic = true,
                   Title = "Bugs",
                   TopicId = Guid.NewGuid()
                }
            };
            log.Info("Created Forum Topic List:");
            foreach (ForumTopicStruct topic in _forumTopics)
            {
                log.Info(
                    String.Format("{0},{1},{2},{3},{4},{5},{6}",
                    topic.TopicId,
                    topic.ForumId,
                    topic.Title,
                    topic.Description,
                    topic.CreatedBy,
                    topic.CreatedOn,
                    topic.IsPublic)
                    );
            }

            _forumThreads = new List<ForumThreadStruct>()
            {
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = false,
                    ModifiedOn = null,
                    ThreadContent = "An Eve thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Eve Thread 01",
                    TopicId = _forumTopics[0].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = false,
                    ModifiedOn = null,
                    ThreadContent = "An Eve thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Eve Thread 02",
                    TopicId = _forumTopics[0].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = false,
                    ModifiedOn = null,
                    ThreadContent = "A Counter Strike:Global Offensive thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Counter Strike:Global Offensive 01",
                    TopicId = _forumTopics[1].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = false,
                    ModifiedOn = null,
                    ThreadContent = "A Counter Strike:Global Offensive thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Counter Strike:Global Offensive 02",
                    TopicId = _forumTopics[1].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = false,
                    ModifiedOn = null,
                    ThreadContent = "A public thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Public Thread",
                    TopicId = _forumTopics[2].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = false,
                    ModifiedOn = null,
                    ThreadContent = "A BOINC thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "BOINC Thread",
                    TopicId = _forumTopics[3].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    IsSticky = true,
                    ModifiedOn = null,
                    ThreadContent = "A bug thread thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Bug Thread",
                    TopicId = _forumTopics[4].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow.AddSeconds(1),
                    IsSticky = true,
                    ModifiedOn = null,
                    ThreadContent = "A bug thread thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Bug Thread 02",
                    TopicId = _forumTopics[4].TopicId
                },
                new ForumThreadStruct(){
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow.AddSeconds(2),
                    IsSticky = true,
                    ModifiedOn = null,
                    ThreadContent = "A bug thread thread blah blah blah...",
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = "Bug Thread 03",
                    TopicId = _forumTopics[4].TopicId
                }
            };
            log.Info("Created Forum Thread List:");
            foreach (ForumThreadStruct thread in _forumThreads)
            {
                log.Info(
                    String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    thread.ThreadId,
                    thread.TopicId,
                    thread.ThreadTitle,
                    thread.ThreadContent,
                    thread.CreatedBy,
                    thread.ModifiedOn,
                    thread.CreatedOn,
                    thread.IsSticky)
                    );
            }

            _forumReplys = new List<ForumReplyStruct>()
            {
                new ForumReplyStruct(){
                    Content = "A reply, there is a bug dumbass",
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = null,
                    ParentThreadId = _forumThreads[6].ThreadId,
                    ReplyId = Guid.NewGuid()
                },
                new ForumReplyStruct(){
                    Content = "Found another one",
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = null,
                    ParentThreadId = _forumThreads[6].ThreadId,
                    ReplyId = Guid.NewGuid()
                }
            };

            for (int i = 0; i < 80; i++)
            {
                _forumReplys.Add(new ForumReplyStruct()
                {
                    Content = "A reply, there is a bug dumbass # " + i.ToString(),
                    CreatedBy = _currentUsers[rand.Next(0, _currentUsers.Count - 1)].UserId,
                    CreatedOn = DateTime.UtcNow.AddSeconds(i),
                    ModifiedOn = null,
                    ParentThreadId = _forumThreads[6].ThreadId,
                    ReplyId = Guid.NewGuid()
                });
            }

            log.Info("Created Forum Reply List:");
            foreach (ForumReplyStruct reply in _forumReplys)
            {
                log.Info(
                    String.Format("{0},{1},{2},{3},{4},{5}",
                    reply.ReplyId,
                    reply.ParentThreadId,
                    reply.Content,
                    reply.CreatedBy,
                    reply.CreatedOn,
                    reply.ModifiedOn)
                    );
            }
        }

        public void PerformGenerationJob(CGDataEntities.CGWebEntities webEntities)
        {
            log.Info("Starting FourmGenerationJob...");
            using (CGWebEntities entities = new CGWebEntities())
            {
                foreach (ForumStruct forum in _forums)
                {
                    entities.Forums.Add(new Forum()
                    {
                        ForumId = forum.ForumId,
                        ForumTitle = forum.ForumTitle,
                        IsPublic = forum.IsPublic
                    });
                }
                entities.SaveChanges();
                log.Info("Wrote Forums data.");

                foreach (ForumTopicStruct topic in _forumTopics)
                {
                    entities.ForumTopics.Add(new ForumTopic()
                    {
                        CreatedBy = topic.CreatedBy,
                        CreatedOn = topic.CreatedOn,
                        TopicDescription = topic.Description,
                        ForumId = topic.ForumId,
                        IsPublic = topic.IsPublic,
                        TopicTitle = topic.Title,
                        TopicId = topic.TopicId
                    });
                }
                entities.SaveChanges();
                log.Info("Wrote Forums Topic data.");

                foreach (ForumThreadStruct thread in _forumThreads)
                {
                    entities.ForumThreads.Add(new ForumThread()
                    {
                        CreatedBy = thread.CreatedBy,
                        CreatedOn = thread.CreatedOn,
                        IsSticky = thread.IsSticky,
                        ModifiedOn = thread.ModifiedOn,
                        ThreadContent = thread.ThreadContent,
                        ThreadId = thread.ThreadId,
                        ThreadTitle = thread.ThreadTitle,
                        ForumTopic = thread.TopicId
                    });
                }
                entities.SaveChanges();
                log.Info("Wrote Forums Theads data.");

                foreach (ForumReplyStruct reply in _forumReplys)
                {
                    entities.ForumReplies.Add(new ForumReply()
                    {
                        ReplyContent = reply.Content,
                        CreatedBy = reply.CreatedBy,
                        CreatedOn = reply.CreatedOn,
                        ModifiedOn = reply.ModifiedOn,
                        ParentThreadId = reply.ParentThreadId,
                        ReplyId = reply.ReplyId
                    });
                }
                entities.SaveChanges();
                log.Info("Wrote Forums Reply data.");
            }
        }

        
    }

    public struct ForumStruct
    {
        public Guid ForumId;
        public string ForumTitle;
        public bool IsPublic;
    }

    public struct ForumTopicStruct
    {
        public Guid TopicId;
        public Guid ForumId;
        public string Title;
        public string Description;
        public bool IsPublic;
        public int CreatedBy;
        public DateTime CreatedOn;
    }

    public struct ForumThreadStruct
    {
        public Guid ThreadId;
        public Guid TopicId;
        public string ThreadTitle;
        public string ThreadContent;
        public int CreatedBy;
        public DateTime CreatedOn;
        public DateTime? ModifiedOn;
        public bool IsSticky;
    }

    public struct ForumReplyStruct
    {
        public Guid ReplyId;
        public Guid ParentThreadId;
        public string Content;
        public int CreatedBy;
        public DateTime CreatedOn;
        public DateTime? ModifiedOn;
    }
}
