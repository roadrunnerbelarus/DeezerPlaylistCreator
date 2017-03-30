using System;
using NUnit.Framework;
using TestFramework;
using TestFramework.LastFm;

namespace DeezerPlaylistTests
{
    [TestFixture]
    public class DeezerPlaylistTests
    {
        //private const string Email = "";
        //private const string Password = "";

        [Test]
        public void ShouldCreatePlaylist()
        {
            const string band = "Garbage";
			
			Console.WriteLine("Enter credentials, email, password");
            string Email = Console.ReadLine();
            string Password = Console.ReadLine();

            var lastFmPage = Pages.LastFmPage;
            Assert.IsTrue(lastFmPage.IsAt(), "This is not LastFM page");

            var bandPage = lastFmPage.GoToBandsPage(band);
            Assert.IsTrue(bandPage.IsThisBandPage(band), $"This is not {band}'s page");
            
            //
            var tracks = bandPage.OpenTracks();
            //bandPage.Tracks
            //

            tracks.OpenView(TimeView.Last365Days);

            var topSongs = tracks.GetTopSongsNames(20);
            Assert.IsNotEmpty(topSongs, "List of songs is empty");

            var deezerLoginPage = Pages.DeezerLoginPage;
            Assert.IsTrue(deezerLoginPage.IsAt(), "This is not Deezer login page");

            var deezerHomePage = deezerLoginPage.Login(Email, Password);
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");

            var playlistBar = deezerHomePage.SideBar.Playlists;
            playlistBar.CreatePlaylist(band);
            Assert.IsTrue(playlistBar.IsPlaylistExist(band), $"{band}'s playlist was not found");

            var deezerBandPage = deezerHomePage.SideBar.GoToBandPage(band);
            Assert.IsTrue(deezerBandPage.IsThisBandPage(band));

            deezerBandPage.Tracks.AddTracksToPlaylist(topSongs, band);

            var playlistPage = playlistBar.OpenPlaylist(band);
            var tracksInPlaylist = playlistPage.GetTrackNames();
            Assert.IsNotEmpty(tracksInPlaylist, "No tracks in playlist");
        }

        [Test]
        public void ShouldDeletePlaylist()
        {
            //
            string Email = Console.ReadLine();
            string Password = Console.ReadLine();
            //
            var deezerLoginPage = Pages.DeezerLoginPage;
            Assert.IsTrue(deezerLoginPage.IsAt(), "This is not Deezer login page");

            var deezerHomePage = deezerLoginPage.Login(Email, Password);
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");

            deezerHomePage.SideBar.Playlists.DeletePlaylists("");
            Assert.IsTrue(deezerHomePage.IsAt(), "This is not Deezer home page");
        }

        [TearDown]
        public void TearDown()
        {
            //Instance.Close();
        }
    }
}