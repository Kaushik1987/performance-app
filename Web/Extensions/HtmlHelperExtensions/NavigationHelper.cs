﻿using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Web.Extensions.HtmlHelperExtensions
{
    public static class NavigationHelper
    {
        public static IHtmlString GenerateRibbon(this HtmlHelper helper)
        {
            return helper.Raw(GetNavbar());
        }

        private static string GetNavbar()
        {
            var sitemap = XDocument.Load(HttpContext.Current.Server.MapPath("~/Web.sitemap"));
            var navbarStringBuilder = new StringBuilder();

            var siteMapRoot = sitemap.Root;
            if (siteMapRoot != null)
            {
                navbarStringBuilder.AppendLine(GetNavigationItem(siteMapRoot));
            }

            foreach (var navbarLinkNode in sitemap.Descendants().Where(x => (string) x.Attribute("navigation") == "SubNavbar").ToList())
            {
                navbarStringBuilder.AppendLine(GetNavigationItem(navbarLinkNode));
            }

            return navbarStringBuilder.ToString();
        }

        public static string GetNavigationItem(XElement node)
        {
            var title = node.Attribute("title")?.Value;
            var action = node.Attribute("action")?.Value;
            var controller = node.Attribute("controller")?.Value;
            var cssClass = node.Attribute("cssClass")?.Value;

            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var url = urlHelper.AbsoluteAction(action, controller);

            return "<li><a href=\"" + url + "\" class=\"" + cssClass + "\">" + title + "</a></li>";
        }
    }
}