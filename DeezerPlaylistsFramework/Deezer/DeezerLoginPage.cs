// ReSharper disable CollectionNeverUpdated.Local
#pragma warning disable 649

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class DeezerLoginPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "#login_button")] private IWebElement _loginBtn;
        [FindsBy(How = How.CssSelector, Using = "#login_mail")] private IWebElement _emailField;
        [FindsBy(How = How.CssSelector, Using = "#login_password")] private IWebElement _passwordField;
        [FindsBy(How = How.CssSelector, Using = "#login_form_submit")] private IWebElement _enterBtn;

        protected override string PageTitle => "Deezer - Flow my Music";

        public static string Url => "http://www.deezer.com";

        internal DeezerLoginPage() : base(Url)
        {
        }

        public DeezerHomePage Login(string email, string password)
        {
            _loginBtn.Click();
            _emailField.SendKeys(email);
            _passwordField.SendKeys(password);
            _enterBtn.Click();

            return new DeezerHomePage();
        }
    }
}