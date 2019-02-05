// ReSharper disable CollectionNeverUpdated.Local

#pragma warning disable 649

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Deezer
{
    public class DeezerPlaylistPage : Tracks
    {
        [FindsBy(How = How.XPath, Using = "//*[contains(@class, \'svg-icon-edit\')]/..")] private IWebElement _editBtn;

        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-action.btn-play.btn-primary")] private IWebElement
            _listenPlaylistBtn;

        [FindsBy(How = How.CssSelector, Using = "div.modal-dialog.modal-large")] private IWebElement _editPlaylistWnd;

        internal DeezerPlaylistPage()
        {
        }

        public void DeletePlaylist(string playlistName)
        {
            var editWnd = OpenEditWindow();
            editWnd.FindElement(By.CssSelector("button.btn.btn-default")).Click();
            editWnd.FindElement(By.CssSelector("input.form-control")).SendKeys(playlistName);
            editWnd.FindElement(By.CssSelector("button#modal_playlist_assistant_submit")).Click();
        }

        private IWebElement OpenEditWindow()
        {
            _editBtn.Click();
            //return Driver.Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.modal-dialog.modal-large")));

            Driver.Wait.Until(d => _editPlaylistWnd.Displayed);
            return _editPlaylistWnd;
        }

        public void Play()
        {
            _listenPlaylistBtn.Click();
        }
    }
}