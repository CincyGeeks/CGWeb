using CincyGeeksWebsite.Utility;
using MarkdownSharp;
using System.Web;
using System.Web.Mvc;

public static class MarkdownHelper
{
    static Markdown _markdownTransformer = new Markdown();

    public static IHtmlString Markdown(this HtmlHelper helper, string text)
    {
        string returnHtml = _markdownTransformer.Transform(text);
        string santizedHtml = HtmlSanitizerUtility.SanitizeInputStringReducedSet(returnHtml);
        return new MvcHtmlString(santizedHtml);
    }
}
