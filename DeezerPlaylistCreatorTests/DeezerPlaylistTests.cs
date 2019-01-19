using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using TestFramework;
using TestFramework.LastFm;
using TestFramework.Deezer;
using LastfmAPI = Lastfm.Services;
using Lastfm;

namespace DeezerPlaylistTests
{
    [TestFixture]
    public class DeezerPlaylistTests
    {
        private readonly string _lastfmEmail = LastfmCredentials.UserName;
        private readonly string _lastfmPassword = LastfmCredentials.Password;

        [Test]
        public void ShouldCreatePlaylist()
        {
            var band = "kreator";
            int numberOfTracks = 20;

            var session = new Session();
            //session.Authenticate(_lastfmEmail, Lastfm.Utilities.md5(_lastfmPassword));

            var artist = new LastfmAPI.Artist(band, session);
            var topTracks = artist.GetTopTracks(numberOfTracks);

            var topTracksTitles = new string[numberOfTracks];

            for (int i = 0; i < topTracksTitles.Length; i++)
            {
                topTracksTitles[i] = topTracks[i].Item.Title;
            }

            //var lastFmPage = Pages.LastFmPage;
            //Assert.IsTrue(lastFmPage.IsAt(), "This is not LastFM page");
            //var bandPage = lastFmPage.GoToBandsPage(band);
            //Assert.IsTrue(
            //    bandPage.IsThisBandPage(band), $"This is not {band}'s page");
            //var topSongs = bandPage.Tracks.
            //    OpenView(TimeView.AllTime).
            //    GetTopSongsNames(numberOfTracks);

            Assert.IsNotEmpty(topTracksTitles, "List of songs is empty");

            var deezerLoginPage = Pages.DeezerLoginPage;
            Assert.IsTrue(deezerLoginPage.IsAt(), "This is not Deezer login page");

            var deezerHomePage = deezerLoginPage.Login();
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");

            var playlistBar = deezerHomePage.SideBar.Playlists;
            playlistBar.CreatePlaylist(band);
            Assert.IsTrue(playlistBar.IsPlaylistExist(band), $"{band}'s playlist was not found");

            var deezerBandPage = deezerHomePage.SideBar.GoToBandPage(band);
            Assert.IsTrue(deezerBandPage.IsThisBandPage(band));

            var tracksPage = deezerBandPage.Tracks;
            tracksPage.AddTracksToPlaylist(topTracksTitles.ToList(), band);

            var playlistPage = playlistBar.OpenPlaylist(band);
            Assert.IsNotEmpty(playlistPage.GetTrackNames(), "No tracks in playlist");

            playlistPage.Play();
        }

        [Test]
        public void ShouldDeletePlaylist()
        {
            var deezerLoginPage = Pages.DeezerLoginPage;
            Assert.IsTrue(deezerLoginPage.IsAt(), "This is not Deezer login page");

            var deezerHomePage = deezerLoginPage.Login();
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");

            var existingPlaylists = deezerHomePage.SideBar.Playlists.GetExistingPlaylists();

            var playlistsToDelete = new List<string>();
            var indexes = new List<int> {0, 6, 7, 16, 17};
            foreach (var i in indexes)
            {
                playlistsToDelete.Add(existingPlaylists[i]);
            }

            deezerHomePage.SideBar.Playlists.DeletePlaylists(playlistsToDelete);
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");

            Driver.Close();
        }

        [Test]
        public void ShouldCreatePlaylistSetList()
        {
            var band = "guns n' roses".ToLower();

            var setlistHomePage = Pages.SetListHomePage;
            Assert.IsTrue(setlistHomePage.IsAt(), "This is not Setlist login page");

            setlistHomePage.AcceptCookies();
            var setlistPage = setlistHomePage.GoToLastSetListOf(band);

            var setListName = setlistPage.GetSetlistName();
            var setList = setlistPage.GetSetlist();

            var deezerLoginPage = Pages.DeezerLoginPage;
            //Assert.IsTrue(deezerLoginPage.IsAt(), "This is not Deezer login page");

            var deezerHomePage = deezerLoginPage.Login();
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");

            var playlistBar = deezerHomePage.SideBar.Playlists;
            playlistBar.CreatePlaylist(setListName);
            Assert.IsTrue(playlistBar.IsPlaylistExist(setListName), $"{setListName} playlist was not found");

            var deezerBandPage = deezerHomePage.SideBar.GoToBandPage(band);
            Assert.IsTrue(deezerBandPage.IsThisBandPage(band));

            deezerBandPage.Tracks.AddTracksToPlaylist(setList, setListName);

            var playlistPage = playlistBar.OpenPlaylist(setListName);
            var tracksInPlaylist = playlistPage.GetTrackNames();
            Assert.IsNotEmpty(tracksInPlaylist, "No tracks in playlist");

            playlistPage.Play();
        }

        [TearDown]
        public void TearDown()
        {
            //Driver.Close();
        }
    }
}