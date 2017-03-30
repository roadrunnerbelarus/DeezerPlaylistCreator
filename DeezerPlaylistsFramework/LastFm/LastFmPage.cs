#pragma warning disable 649
// ReSharper disable CollectionNeverUpdated.Local

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.LastFm
{
    public class LastFmPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "a.masthead-search-toggle")] private IWebElement _searchBtn;
        [FindsBy(How = How.CssSelector, Using = "input#masthead-search-field")] private IWebElement _searchBox;

        protected override string PageTitle
            => "Last.fm - Listen to free music and watch videos with the largest music catalogue online";

        public static string Url => "http://last.fm";

        internal LastFmPage() : base(Url)
        {
        }

        //private SearchResultsPage SearchResultsPage => new SearchResultsPage();

        public LastFmBandsPage GoToBandsPage(string bandName)
        {
            _searchBtn.Click();
            _searchBox.SendKeys(bandName);
            _searchBox.Submit();

            return new SearchResultsPage().FindTheBand(bandName);
        }
    }
}