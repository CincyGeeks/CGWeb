using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CincyGeeksWebsite.Models.Forum.PartialModels
{
    public class CreateNewThreadViewModel
    {
        public Guid TopicId { get; set; }
        [AllowHtml]
        public string ThreadTitle { get; set; }
        [AllowHtml]
        public string ThreadContent { get; set; }
        public bool IsSticky { get; set; }
    }
}