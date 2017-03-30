// ReSharper disable CollectionNeverUpdated.Local
#pragma warning disable 649

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class DeezerBandsPage : DeezerPageBase
    {
        [FindsBy(How = How.CssSelector, Using = "ul.navbar-nav")] private IWebElement _navBar;
        [FindsBy(How = How.CssSelector, Using = "h1.heading-1.ellipsis")] private IWebElement _pageHeader;

        private IWebElement TracksTab => _navBar.FindElements(By.CssSelector("li"))[1];

        internal DeezerBandsPage()
        {
        }

        public Tracks Tracks
        {
            get
            {
                TracksTab.Click();
                return new Tracks();
            }
        }

        public bool IsThisBandPage(string band)
        {
            return string.Equals(_pageHeader.Text, band, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}