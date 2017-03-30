using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    internal class Driver
    {
        private static IWebDriver _driver;
        public static WebDriverWait Wait { get; } = new WebDriverWait(Instance, TimeSpan.FromSeconds(5));
        public static string Title => _driver.Title;
        public static IWebDriver Instance
        {
            get
            {
                if (_driver == null)
                {
                    _driver = new ChromeDriver(@"C:\Libraries\");
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                }
                return _driver;
            }
        }
        public static void Close()
        {
            _driver.Quit();
        }
    }
}