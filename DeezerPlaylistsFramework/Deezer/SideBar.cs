// ReSharper disable CollectionNeverUpdated.Local
#pragma warning disable 649

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class SideBar : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "div#page_sidebar")] private IWebElement _sideBar;
        [FindsBy(How = How.CssSelector, Using = "input#menu_search")] private IWebElement _searchField;

        internal SideBar()
        {
        }

        public bool IsDisplayed()
        {
            return _sideBar.Displayed;
        }

        public PlaylistBar Playlists => new PlaylistBar();

        public DeezerBandsPage GoToBandPage(string band)
        {
            _searchField.SendKeys(band);
            _searchField.Submit();

            var results = new DeezerSearchResultsPage();
            return results.FindBand(band);
        }
    }
}