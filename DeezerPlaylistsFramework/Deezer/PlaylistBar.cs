// ReSharper disable CollectionNeverUpdated.Local

#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class PlaylistBar : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = "span.heading-4")] private IWebElement _createPlaylistBtn;

        [FindsBy(How = How.CssSelector, Using = "div.modal-dialog.modal-large")] private IWebElement
            _createPlaylistWindow;

        [FindsBy(How = How.CssSelector, Using = "div.heading-4.ellipsis a")] private IList<IWebElement>
            _existingPlaylists;

        [FindsBy(How = How.CssSelector, Using = "div.panel-wrapper.panel-wrapper-playlists")] private IWebElement
            _playListsBar;

        [FindsBy(How = How.CssSelector, Using = "button.nav-link")] private IList<IWebElement> _sideBarBtns;

        private IWebElement PlaylistsBtn => _sideBarBtns[0];

        internal PlaylistBar()
        {
            ExpandPlaylistsBar();
        }

        private void ExpandPlaylistsBar()
        {
            try
            {
                if (_playListsBar.Displayed)
                    return;
            }
            catch (Exception)
            {
                PlaylistsBtn.Click();
            }
        }

        private bool _playlistExists;

        public void CreatePlaylist(string playlistName)
        {
            ExpandPlaylistsBar();

            if (IsPlaylistExist(playlistName))
                return;

            _createPlaylistBtn.Click();
            // window appears
            _createPlaylistWindow.FindElement(By.CssSelector("input.form-control.form-control-block"))
                .SendKeys(playlistName);
            _createPlaylistWindow.FindElement(By.CssSelector("button#modal_playlist_assistant_submit")).Click();
        }

        private List<string> GetExistingPlaylists()
        {
            ExpandPlaylistsBar();
            var playLists = new List<string>();

            foreach (var playlist in _existingPlaylists)
            {
                playLists.Add(playlist.Text.ToLower());
            }
            return playLists;
        }

        public bool IsPlaylistExist(string playlistName)
        {
            if (_playlistExists)
                return true;

            return _playlistExists = GetExistingPlaylists().Contains(playlistName.ToLower());
        }

        public DeezerPlaylistPage OpenPlaylist(string playlistName)
        {
            ExpandPlaylistsBar();

            if (!IsPlaylistExist(playlistName))
                throw new NotFoundException("No such playlist");

            var playlistLink = _existingPlaylists.FirstOrDefault(el => string.Equals(el.Text, playlistName, StringComparison.InvariantCultureIgnoreCase));

            if (playlistLink == null)
                throw new NoSuchElementException("Link to playlist was not found");

            playlistLink.Click();
            return new DeezerPlaylistPage();
        }

        public void DeletePlaylists(params string[] playlists)
        {
            foreach (var playlist in playlists)
            {
                DeezerPlaylistPage playlistPage;
                try
                {
                    playlistPage = OpenPlaylist(playlist);
                }
                catch (NotFoundException) 
                {
                    continue;
                }

                playlistPage.DeletePlaylist(playlist);
            }
        }
    }
}