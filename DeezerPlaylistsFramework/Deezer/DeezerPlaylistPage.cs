// ReSharper disable CollectionNeverUpdated.Local

#pragma warning disable 649

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Deezer
{
    public class DeezerPlaylistPage : Tracks
    {
        [FindsBy(How = How.CssSelector, Using = "span.icon.icon-edit")] private IWebElement _editBtn;

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
            return Driver.Wait.Until(d => Driver.Instance.FindElement(By.CssSelector("div.modal-dialog.modal-large")));
        }
    }
}