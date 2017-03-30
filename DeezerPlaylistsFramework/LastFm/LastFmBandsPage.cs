using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.LastFm
{
    public class LastFmBandsPage : PageBase
    {
        internal LastFmBandsPage()
        {
        }

        [FindsBy(How = How.CssSelector, Using = ".header-title")]
        private IWebElement HeaderTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a.js-partial-library-link")]
        private IWebElement MoreTracks { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.navlist-item.secondary-nav-item.secondary-nav-item--overview")]
        private IWebElement Overview { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.navlist-item.secondary-nav-item.secondary-nav-item--tracks")]
        private IWebElement TracksBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.navlist-item.secondary-nav-item.secondary-nav-item--albums")]
        private IWebElement Albums { get; set; }


        public bool IsThisBandPage(string bandName)
        {
            return string.Equals(HeaderTitle.Text, bandName, StringComparison.InvariantCultureIgnoreCase);
        }

        public TracksPage OpenTracks()
        {
            TracksBtn.Click();
            return new TracksPage();
        }

        //public Tracks Tracks { get; set; }
        //public TYPE Albums { get; set; }
        //public TYPE Overview { get; set; }

    }
}