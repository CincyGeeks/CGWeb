using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Account
{
    public class ResetPasswordModel
    {
        public string Username { get; set; }
        public bool TokenIsValid { get; set; }
    }
}