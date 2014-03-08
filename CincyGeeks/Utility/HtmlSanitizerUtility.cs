using Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Utility
{
    public static class HtmlSanitizerUtility
    {
        private static HtmlSanitizer _sanitizerReducedSet;
        private static HtmlSanitizer _noHtmlSanitizer;
        private static string _baseSite;

        static HtmlSanitizerUtility()
        {
            _sanitizerReducedSet = new HtmlSanitizer();
            _sanitizerReducedSet.AllowedTags = new List<string>()
            {
                "b",
                "blockquote",
                "code",
                "del",
                "dd",
                "dl",
                "dt",
                "em",
                "h1",
                "h2",
                "h3",
                "i",
                "kbd",
                "li",
                "ol",
                "p",
                "pre",
                "s",
                "sup",
                "sub",
                "strong",
                "strike",
                "ul",
                "br",
                "hr",
            };

            _noHtmlSanitizer = new HtmlSanitizer();
            _baseSite = VirtualPathUtility.ToAbsolute("~/");
        }

        public static string SanitizeInputStringReducedSet(string str)
        {
            return _sanitizerReducedSet.Sanitize(str, _baseSite);
        }

        public static string SanitizeInputStringNoHTML(string str)
        {
            return _noHtmlSanitizer.Sanitize(str, _baseSite);
        }
    }
}