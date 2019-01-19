// ReSharper disable CollectionNeverUpdated.Local
#pragma warning disable 649

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class DeezerLoginPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "#topbar-login-button")] private IWebElement _loginBtn;
        [FindsBy(How = How.CssSelector, Using = "#login_mail")] private IWebElement _emailField;
        [FindsBy(How = How.CssSelector, Using = "#login_password")] private IWebElement _passwordField;
        [FindsBy(How = How.CssSelector, Using = "#login_form_submit")] private IWebElement _enterBtn;

        //protected override string PageTitle => "Deezer - Try Flow. Download and listen to music | Music Streaming";
        protected override string PageTitle => "Deezer - Flow - Загружайте и слушайте свою музыку | Бесплатная потоковая трансляция";

        public static string Url => "http://www.deezer.com";

        internal DeezerLoginPage() : base(Url)
        {
        }

        public DeezerHomePage Login()
        {
            var email = DeezerCredentials.Email;
            var password = DeezerCredentials.Password;

            _loginBtn.Click();
            _emailField.SendKeys(email);
            _passwordField.SendKeys(password);
            _enterBtn.Click();

            return new DeezerHomePage();
        }
    }
}