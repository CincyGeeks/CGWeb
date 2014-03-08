using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Forum.PartialModels
{
    public class QuoteReplyRequestModel
    {
        public string ContainerName { get; set; }
        public Guid ThreadId { get; set; }
        public Guid ReplyId { get; set; }
    }
}