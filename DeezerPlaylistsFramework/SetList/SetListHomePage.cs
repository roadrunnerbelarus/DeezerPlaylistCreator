using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.SetList
{
    public class SetListHomePage : SetListPageBase
    {
        [FindsBy(How = How.CssSelector, Using = "input#id3")] private IWebElement _boxSearch;
        [FindsBy(How = How.CssSelector, Using = "button.visible-lg.visible-md.searchButton")] private IWebElement _btnSearch;

        protected override string PageTitle => "setlist.fm - the setlist wiki";

        public static string Url => "http://www.setlist.fm/";

        internal SetListHomePage() : base(Url)
        {
        }

        public SetListPage GoToLastSetListOf(string band)
        {
            _boxSearch.SendKeys(band);

            try
            {
                _btnSearch.Click();
            }
            catch (ElementNotVisibleException)
            {
                _boxSearch.SendKeys(Keys.Enter);
            }
            catch (TargetInvocationException)
            {
                _boxSearch.SendKeys(Keys.Enter);
            }

            return new SearchResultsPage().OpenLatestSetlist();
        }


    }
}
