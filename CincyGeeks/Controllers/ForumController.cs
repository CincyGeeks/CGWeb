using CGDataEntities;
using CincyGeeksWebsite.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CincyGeeksWebsite.Data;
using System.Configuration;
using CincyGeeksWebsite.Utility;
using CincyGeeksWebsite.Models.Forum.PartialModels;
using System.IO;

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
            ViewForumModel viewForumModel = new ViewForumModel(){};
            
            using (CGWebEntities entities = new CGWebEntities())
            {
                Forum currentForum = entities.Forums.Where(F => F.ForumId.Equals(forumId)).Single();

                if (!currentForum.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                if (Request.IsAuthenticated)
                {
                    viewForumModel = currentForum.ConvertToViewForumModel(true, false);
                }
                else
                {
                    viewForumModel = currentForum.ConvertToViewForumModel(true, true);
                }
            }

            return View(viewForumModel);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult ViewForum(Guid forumId, string topicTitle, string topicDescription, bool isPublic)
        {
            using (CGWebEntities entities = new CGWebEntities())
            {
                UserProfile currentUserProfile = entities.UserProfiles.Where(P => P.UserName.Equals(User.Identity.Name)).Single();

                ForumTopic newTopic = new ForumTopic()
                {
                    CreatedBy = currentUserProfile.UserId,
                    CreatedOn = DateTime.UtcNow,
                    ForumId = forumId,
                    IsPublic = isPublic,
                    TopicDescription = topicDescription,
                    TopicId = Guid.NewGuid(),
                    TopicTitle = HtmlSanitizerUtility.SanitizeInputStringNoHTML(topicTitle)
                };
                entities.ForumTopics.Add(newTopic);
                entities.SaveChanges();
            }

            return ViewForum(forumId);
        }

        public ActionResult ViewTopic(ForumTopicRequestModel requestModel)
        {
            ViewTopicModel viewTopicModel = new ViewTopicModel(){
                Threads = new List<ForumThreadModel>()
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                ForumTopic currentTopic = entities.ForumTopics.Where(T => T.TopicId.Equals(requestModel.TopicId)).Single();
                viewTopicModel.CurrentTopic = currentTopic.ConvertToForumTopicModel();
                viewTopicModel.ParentForum = currentTopic.Forum.ConvertToViewForumModel(false, false);

                if(!currentTopic.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                var currentTopicGroup = entities.ForumThreads.Where(FT => FT.ForumTopic.Equals(requestModel.TopicId));

                int currentTopicViewLimit = Convert.ToInt32(ConfigurationManager.AppSettings["ForumTopicPagingLimit"]);
                viewTopicModel.CurrentPage = requestModel.CurrentPage;
                viewTopicModel.MaxPages = (int)Math.Ceiling((double)currentTopicGroup.Count() / (double)currentTopicViewLimit);

                foreach (ForumThread thread in currentTopicGroup.OrderByDescending(FT => FT.CreatedOn).OrderByDescending(FT => FT.IsSticky).Skip(currentTopicViewLimit * requestModel.CurrentPage).Take(currentTopicViewLimit))
                {
                    viewTopicModel.Threads.Add(thread.ConvertToForumThreadModel(false));
                }
            }

            return View(viewTopicModel);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult ViewTopic(Guid topicId, string threadTitle, string threadContent, bool isSticky)
        {
            ViewTopicModel viewTopicModel = new ViewTopicModel()
            {
                Threads = new List<ForumThreadModel>()
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                UserProfile currentUserProfile = entities.UserProfiles.Where(P => P.UserName.Equals(User.Identity.Name)).Single();

                ForumThread newThread = new ForumThread()
                {
                    CreatedBy = currentUserProfile.UserId,
                    CreatedOn = DateTime.UtcNow,
                    ForumTopic = topicId,
                    IsSticky = isSticky,
                    ModifiedOn = null,
                    ThreadContent = threadContent,
                    ThreadId = Guid.NewGuid(),
                    ThreadTitle = HtmlSanitizerUtility.SanitizeInputStringNoHTML(threadTitle)
                };

                entities.ForumThreads.Add(newThread);
                entities.SaveChanges();
                ModelState.Clear();

                ForumTopic currentTopic = entities.ForumTopics.Where(T => T.TopicId.Equals(topicId)).Single();
                viewTopicModel.CurrentTopic = currentTopic.ConvertToForumTopicModel();
                viewTopicModel.ParentForum = currentTopic.Forum.ConvertToViewForumModel(false, false);

                if (!currentTopic.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                var currentTopicGroup = entities.ForumThreads.Where(FT => FT.ForumTopic.Equals(topicId));

                int currentTopicViewLimit = Convert.ToInt32(ConfigurationManager.AppSettings["ForumTopicPagingLimit"]);
                viewTopicModel.MaxPages = (int)Math.Ceiling((double)currentTopicGroup.Count() / (double)currentTopicViewLimit);
                viewTopicModel.CurrentPage = viewTopicModel.MaxPages - 1;

                foreach (ForumThread thread in currentTopicGroup.OrderByDescending(FT => FT.CreatedOn).OrderByDescending(FT => FT.IsSticky).Skip(currentTopicViewLimit * viewTopicModel.CurrentPage).Take(currentTopicViewLimit))
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

                viewThreadModel.ParentTopic = currentThread.ParentForumTopic.ConvertToForumTopicModel();

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

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult ViewThread(CreateNewReplyViewModel model)
        {
            ViewThreadModel viewThreadModel = new ViewThreadModel()
            {
                Replies = new List<ForumReplyModel>()
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                UserProfile currentUserProfile = entities.UserProfiles.Where(P => P.UserName.Equals(User.Identity.Name)).Single();

                ForumReply newReply = new ForumReply()
                {
                    CreatedBy = currentUserProfile.UserId,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = null,
                    ParentThreadId = model.ThreadId,
                    ReplyContent = model.ReplyContent,
                    ReplyId = Guid.NewGuid()
                };

                entities.ForumReplies.Add(newReply);
                entities.SaveChanges();
                ModelState.Clear();

                ForumThread currentThread = entities.ForumThreads.Where(FT => FT.ThreadId.Equals(model.ThreadId)).Single();

                viewThreadModel.ParentTopic = currentThread.ParentForumTopic.ConvertToForumTopicModel();

                if (!currentThread.ParentForumTopic.IsPublic && !Request.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                int currentPagingLimit = Convert.ToInt32(ConfigurationManager.AppSettings["ForumReplyPagingLimit"]);
                viewThreadModel.MaxPages = (int)Math.Ceiling((double)currentThread.ForumReplies.Count / (double)currentPagingLimit);
                viewThreadModel.CurrentPage = viewThreadModel.MaxPages - 1;

                viewThreadModel.CurrentThread = currentThread.ConvertToForumThreadModel(true);

                foreach (ForumReply reply in currentThread.ForumReplies.OrderBy(FR => FR.CreatedOn).Skip(viewThreadModel.CurrentPage * currentPagingLimit).Take(currentPagingLimit))
                {
                    viewThreadModel.Replies.Add(reply.ConvertToThreadReplyModel(true));
                }
            }

            return View(viewThreadModel);
        }

        #region Partial Implentations
        public PartialViewResult ForumCreateNewReply(CreateNewReplyRequestModel requestModel)
        {
            return PartialView("Partials/_CreateNewReplyPartial", new CreateNewReplyViewModel(){
                ThreadId = requestModel.ThreadId,
                ContainerName = requestModel.ContainerName
            });
        }

        public PartialViewResult ForumQuoteAReply(QuoteReplyRequestModel requestModel)
        {
            CreateNewReplyViewModel replyViewModel = new CreateNewReplyViewModel(){
                ThreadId = requestModel.ThreadId,
                ContainerName = requestModel.ContainerName
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                ForumReply selectedReply = entities.ForumReplies.Where(FR => FR.ReplyId.Equals(requestModel.ReplyId)).Single();
                using (StringReader reader = new StringReader(selectedReply.ReplyContent))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        replyViewModel.ReplyContent += ">" + line;
                    }
                }
            }

            return PartialView("Partials/_CreateNewReplyPartial", replyViewModel);
        }

        public PartialViewResult ForumQuoteAThread(QuoteReplyRequestModel requestModel)
        {
            CreateNewReplyViewModel replyViewModel = new CreateNewReplyViewModel()
            {
                ThreadId = requestModel.ThreadId,
                ContainerName = requestModel.ContainerName
            };

            using (CGWebEntities entities = new CGWebEntities())
            {
                ForumThread selectedThread = entities.ForumThreads.Where(FT => FT.ThreadId.Equals(requestModel.ReplyId)).Single();
                using (StringReader reader = new StringReader(selectedThread.ThreadContent))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        replyViewModel.ReplyContent += ">" + line;
                    }
                }
            }

            return PartialView("Partials/_CreateNewReplyPartial", replyViewModel);
        }
        #endregion
    }
}
