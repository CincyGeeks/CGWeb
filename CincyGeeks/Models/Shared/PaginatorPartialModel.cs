using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Shared
{
    public class PaginatorPartialModel
    {
        public string ActionName;
        public object FirstPageParamerterObject;
        public object PrevPageParamerterObject;
        public object NextPageParamerterObject;
        public object LastPageParamerterObject;
        //parameter objects for each numbered link in order
        public object[] PageLinkParameterObjects;
        public string[] PageLinkValues;
    }
}