using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    public class Driver
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
                    _driver = new ChromeDriver();
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                }
                return _driver;
            }
        }
        public static void Close()
        {
            _driver.Quit();
        }

        public static void ScrollToElement(IWebElement element)
        {
            var js = (IJavaScriptExecutor)Instance;
            js.ExecuteScript($"window.scrollTo(0,{element.Location.Y});");
            Thread.Sleep(500);
        }
    }
}