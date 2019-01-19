#pragma warning disable 649
// ReSharper disable CollectionNeverUpdated.Local

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.LastFm
{
    public class SearchResultsPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "ol.grid-items")] private IList<IWebElement> _sections;
        private IWebElement Artists => _sections.FirstOrDefault();
        private IWebElement Albums => _sections.LastOrDefault();

        internal SearchResultsPage()
        {
        }

        internal LastFmBandsPage FindTheBand(string bandName)
        {
            if (Artists == null)
                throw new NotFoundException("No search results");

            var bandLink = Artists.FindElements(By.CssSelector("a.link-block-target"))
                .FirstOrDefault(el => el.Text.ToLower().Equals(bandName));

            if (bandLink == null)
                throw new NotFoundException($"{bandName} was not found");

            bandLink.Click();
            return new LastFmBandsPage();
        }
    }
}