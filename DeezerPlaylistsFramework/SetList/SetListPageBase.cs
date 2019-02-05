using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.SetList
{
    public abstract class SetListPageBase : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "div.qc-cmp-ui.qc-cmp-showing")] private IWebElement _wndWeUseCookies;
        [FindsBy(How = How.CssSelector, Using = "button.qc-cmp-button")] private IWebElement _btnAccept;

        protected SetListPageBase()
        {
                
        }
        protected SetListPageBase(string url) : base(url)
        {
            AcceptCookies();
        }

        public void AcceptCookies()
        {
            if (_wndWeUseCookies.Displayed)
                _btnAccept.Click();
        }
    }
}
