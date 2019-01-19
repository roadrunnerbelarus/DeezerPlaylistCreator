using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.SetList
{
    class SearchResultsPage : SetListPageBase
    {
        [FindsBy(How = How.CssSelector, Using = "div.col-xs-12.setlistPreview")] private IList<IWebElement> _concerts;
        //[FindsBy(How = How.CssSelector, Using = "//ol[contains(@class, \'list-inline\')]/../../..")] private IList<IWebElement> _concertsWithSetlist;

        public SetListPage OpenLatestSetlist()
        {
            if (_concerts == null)
                throw new NotFoundException();

            var xpath = "//ol[contains(@class, \'list-inline\')]/../../..";
            var concertsWithSetlist = Driver.Instance.FindElements(By.XPath(xpath));

            var latestConcertWithSetList = concertsWithSetlist.FirstOrDefault();
            var link = latestConcertWithSetList.FindElement(By.CssSelector("h2 > a"));

            Driver.ScrollToElement(link);

            link.Click();
            return new SetListPage();
        }
    }
}
