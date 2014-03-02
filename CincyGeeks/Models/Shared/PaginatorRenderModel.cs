using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Shared
{
    public class PaginatorRenderModel
    {
        public int CurrentPage {get;set;}
        public int MaxPage { get; set; }
        public string ActionName { get; set; }
        public dynamic ActionParamerType { get; set; }
        public string PageValueMember { get; set; }
        //memberName, value, index
        public Dictionary<string, object> ConstantValues { get; set; }

        public dynamic GetParameterObjectForPage(int desieredPage)
        {
            Type actionParameterType = ActionParamerType.GetType();
            var actionParamInstance = Activator.CreateInstance(actionParameterType);

            actionParameterType.GetProperty(PageValueMember).SetValue(actionParamInstance, desieredPage, null);
            foreach (KeyValuePair<string, object> constant in ConstantValues)
            {
                actionParameterType.GetProperty(constant.Key).SetValue(actionParamInstance, constant.Value, null);
            }

            return actionParamInstance;
        }
    }
}