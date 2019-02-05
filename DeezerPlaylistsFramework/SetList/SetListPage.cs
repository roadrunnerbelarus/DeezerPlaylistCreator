using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.SetList
{
    public class SetListPage : SetListPageBase
    {
        [FindsBy(How = How.CssSelector, Using = "a.songLabel")] private IList<IWebElement> _songs;
        [FindsBy(How = How.CssSelector, Using = "div.setlistHeadline> h1 > strong")] private IWebElement _setListName;
        [FindsBy(How = How.CssSelector, Using = "div.dateBlock")] private IWebElement _concertDate;
        
        public List<string> GetSetlist()
        {
            var setList = _songs.Select(s => s.Text.ToLower()).ToList();
            return setList;
        }

        public string GetSetlistName()
        {
            var setlistName = $"{_setListName.Text} {_concertDate.Text}".Replace("\r\n", "_");
            return setlistName;
        }
    }
}