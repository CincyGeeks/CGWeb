using CGDataEntities;
using CincyGeeksWebsite.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CincyGeeksWebsite.Data;
using System.Configuration;

namespace CincyGeeksWebsite.Controllers
{
    public class ForumController : CGBaseController
    {
        //
        // GET: /Forum/

        public ActionResult Index()
        {
            ForumIndexModel indexModel = new ForumIndexModel()
            {
                Forums = new Dictionary<string, Guid>()
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                if (Request.IsAuthenticated)
                {
                    foreach (Forum forum in entities.Forums)
                        indexModel.Forums.Add(forum.ForumTitle, forum.ForumId);
                }
                else
                {
                    foreach (Forum forum in entities.Forums.Where(F => F.IsPublic))
                        indexModel.Forums.Add(forum.ForumTitle, forum.ForumId);
                }
            }
            return View(indexModel);
        }

        public ActionResult ViewForum(Guid forumId)
        {
            ViewForumModel viewForumModel = new ViewForumModel(){
                ForumTopics = new List<ForumTopicModel>()
            };
            
            using (CGWebEntities entities = new CGWebEntities())
            {
                Forum currentForum = entities.Forums.Where(F => F.ForumId.Equals(forumId)).Single();

                if (!currentForum.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                List<ForumTopic> topicsList;
                if (Request.IsAuthenticated)
                {

                    topicsList = entities.ForumTopics.Where(FT => FT.ForumId.Equals(forumId)).ToList();
                    foreach(ForumTopic topic in topicsList.OrderBy(FT => FT.TopicTitle))
                        viewForumModel.ForumTopics.Add(topic.ConvertToForumTopicModel());
                }
                else
                {
                    topicsList = entities.ForumTopics.Where(FT => FT.ForumId.Equals(forumId) && FT.IsPublic).ToList();
                    foreach(ForumTopic topic in topicsList.OrderBy(FT => FT.TopicTitle))
                        viewForumModel.ForumTopics.Add(topic.ConvertToForumTopicModel());
                }
            }

            return View(viewForumModel);
        }

        public ActionResult ViewTopic(Guid topicId)
        {
            ViewTopicModel viewTopicModel = new ViewTopicModel(){
                Threads = new List<ForumThreadModel>()
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                ForumTopic currentTopic = entities.ForumTopics.Where(T => T.TopicId.Equals(topicId)).Single();

                if(!currentTopic.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                foreach (ForumThread thread in entities.ForumThreads.Where(FT => FT.ForumTopic.Equals(topicId)).OrderBy(FT => FT.IsSticky).OrderByDescending(FT => FT.CreatedOn))
                {
                    viewTopicModel.Threads.Add(thread.ConvertToForumThreadModel(false));
                }
            }

            return View(viewTopicModel);
        }

        public ActionResult ViewThread(ForumThreadRequestModel requestModel)
        {
            ViewThreadModel viewThreadModel = new ViewThreadModel(){
                Replies = new List<ForumReplyModel>()   
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                ForumThread currentThread = entities.ForumThreads.Where(FT => FT.ThreadId.Equals(requestModel.ThreadId)).Single();
                
                if(!currentThread.ParentForumTopic.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                int currentPagingLimit = Convert.ToInt32(ConfigurationManager.AppSettings["ForumReplyPagingLimit"]);
                viewThreadModel.CurrentPage = requestModel.CurrentPage;
                viewThreadModel.MaxPages = (int)Math.Ceiling((double)currentThread.ForumReplies.Count / (double)currentPagingLimit);

                viewThreadModel.CurrentThread = currentThread.ConvertToForumThreadModel(true);

                foreach (ForumReply reply in currentThread.ForumReplies.OrderBy(FR => FR.CreatedOn).Skip(requestModel.CurrentPage * currentPagingLimit).Take(currentPagingLimit))
                {
                    viewThreadModel.Replies.Add(reply.ConvertToThreadReplyModel(true));
                }
            }

            return View(viewThreadModel);
        }
    }
}
