#pragma warning disable 649
// ReSharper disable CollectionNeverUpdated.Local

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.LastFm
{
    public enum TimeView
    {
        Last7Days,
        Last30Days,
        Last90Days,
        Last365Days,
        AllTime
    }

    public class TracksPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "h1.content-top-header")] private IWebElement _contentHeader;

        [FindsBy(How = How.CssSelector, Using = "button.disclose-trigger.dropdown-menu-clickable-button")] private
            IWebElement _time;

        [FindsBy(How = How.CssSelector, Using = "a.dropdown-menu-clickable-item")] private IList<IWebElement> _list;

        [FindsBy(How = How.CssSelector, Using = "a.link-block-target")] private IList<IWebElement> _songEls;

        internal TracksPage()
        {
            Driver.Wait.Until(ExpectedConditions.TextToBePresentInElement(_contentHeader, "Tracks"));
        }

        public void OpenView(TimeView time)
        {
            string view;
            switch (time)
            {
                case TimeView.AllTime:
                    view = "All time";
                    break;
                case TimeView.Last365Days:
                    view = "Last 365 days";
                    break;
                case TimeView.Last90Days:
                    view = "Last 90 days";
                    break;
                case TimeView.Last30Days:
                    view = "Last 30 days";
                    break;
                case TimeView.Last7Days:
                    view = "Last 7 days";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_time.Text.Contains(view))
                return;

            _time.Click();
            _list.Single(v => v.Text == view).Click();

            //Driver.Wait.Until(ExpectedConditions.TextToBePresentInElement(_time, view));
            Driver.Wait.Until(d => _time.Text.Contains(view));
        }

        public List<string> GetTopSongsNames(int count)
        {
            var songNames = new List<string>();
            for (var i = 2; i < count + 2; i++)
            {
                songNames.Add(_songEls[i].Text);
            }
            return songNames;
        }
    }
}