namespace Sitecore.Support.XA.Feature.SiteMetadata.Pipelines.GetRobotsContent
{
    using Sitecore;
    using Sitecore.Web;
    using Sitecore.XA.Feature.SiteMetadata.Enums;
    using Sitecore.XA.Feature.SiteMetadata.Pipelines.GetRobotsContent;
    using Sitecore.XA.Foundation.IoC;
    using Sitecore.XA.Foundation.Multisite;
    using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
    using System;
    using System.Web;

    public class AppendSitemapUrl : Sitecore.XA.Feature.SiteMetadata.Pipelines.GetRobotsContent.AppendSitemapUrl
    {
        private string GetSitemapUrl(Uri url, SiteInfo site)
        {
            return (url.Scheme + Uri.SchemeDelimiter + (url.Host + site.VirtualFolder + "/sitemap.xml").Replace("//", "/"));
        }

        public override void Process(GetRobotsContentArgs args)
        {
            SiteInfo siteInfo = Context.Site.SiteInfo;
            Uri url = HttpContext.Current.Request.Url;
            if (((SitemapStatus)ServiceLocator.Current.Resolve<IMultisiteContext>().GetSettingsItem(Context.Database.GetItem(Context.Site.StartPath)).Fields[Sitecore.XA.Feature.SiteMetadata.Templates.Sitemap._SitemapSettings.Fields.SitemapMode].ToEnum<SitemapStatus>()) != SitemapStatus.Inactive) // changed == SitemapStatus.Inactive to != SitemapStatus.Inactive to fix 6484
            {
                args.Content.AppendLine("");
                args.Content.AppendLine("Sitemap: " + this.GetSitemapUrl(url, siteInfo));
            }
        }
    }
}