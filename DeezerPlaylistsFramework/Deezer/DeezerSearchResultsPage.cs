// ReSharper disable CollectionNeverUpdated.Local

#pragma warning disable 649

using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    internal class DeezerSearchResultsPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "li.nano-card-item.has-shadow.has-thumbnail-rounded a")] private
            IWebElement _candidateLink;

        [FindsBy(How = How.CssSelector, Using = "ul.navbar-nav li")] private IList<IWebElement> _tabsElements;

        [FindsBy(How = How.CssSelector, Using = "a.evt-click")] private IList<IWebElement> _bandLinks;

        private IWebElement ArtistsTab => _tabsElements[1];
        private IWebElement AlbumsTab => _tabsElements[2];

        internal DeezerBandsPage FindBand(string band)
        {
            try
            {
                if (_candidateLink.Text.ToLower().Equals(band.ToLower()))
                {
                    _candidateLink.Click();
                    return new DeezerBandsPage();
                }
            }
            catch (NoSuchElementException)
            {
                throw new NotFoundException($"{band} wasn't found");
            }

            ArtistsTab.Click();
            foreach (var link in _bandLinks)
            {
                if (!string.Equals(link.Text, band, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                link.Click();
                return new DeezerBandsPage();
            }
            throw new NotFoundException($"{band} wasn't found");
        }
    }
}