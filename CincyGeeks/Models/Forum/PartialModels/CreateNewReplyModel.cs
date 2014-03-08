using System;
using System.Web.Mvc;

namespace CincyGeeksWebsite.Models.Forum.PartialModels
{
    public class CreateNewReplyViewModel
    {
        public string ContainerName { get; set; }
        public Guid ThreadId { get; set; }
        [AllowHtml]
        public string ReplyContent { get; set; }
    }
}