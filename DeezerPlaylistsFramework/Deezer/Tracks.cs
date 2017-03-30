// ReSharper disable CollectionNeverUpdated.Local

#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class Tracks : DeezerPageBase
    {
        [FindsBy(How = How.CssSelector, Using = "td.track")] private IList<IWebElement> _deezerTracks;

        internal Tracks()
        {
        }

        public void AddTracksToPlaylist(IEnumerable<string> tracksList, string playlistName)
        {
            ScrollDown(2);

            var finalList = new List<IWebElement>();

            foreach (var topTrack in tracksList)
            {
                foreach (var deezerTrack in _deezerTracks)
                {
                    var deezerTrackText = deezerTrack.Text.ToLower();
                    if (!string.Equals(deezerTrackText, topTrack, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!topTrack.ToLower().Contains(deezerTrackText))
                        {
                            if (!deezerTrackText.Contains(topTrack.ToLower()))
                                continue;
                        }
                    }

                    if (deezerTrackText.Contains("(live") || deezerTrackText.Contains("(Live") ||
                        deezerTrackText.Contains("demo"))
                        continue;

                    finalList.Add(deezerTrack);
                    _deezerTracks.Remove(deezerTrack);
                    break;
                }
            }

            foreach (var song in finalList)
            {
                // For history
                //var el =
                //    Instance.Instance.FindElement(
                //        By.XPath($"//a[contains(text(),\"{song.Text.Trim()}\")]/parent::div/parent::div"));

                song.FindElement(By.CssSelector("span.context-menu-anchor")).Click();

                var contextMenu = Driver.Instance.FindElement(By.CssSelector("div.page-contextmenu.opened"));

                contextMenu.FindElements(By.CssSelector("button.menu-item"))[3].Click();

                var bandPlaylist =
                    contextMenu.FindElements(
                            By.XPath($"//span[contains(@class,'label ellipsis')][contains(text(),\"{playlistName}\")]"))
                        .LastOrDefault();

                if (bandPlaylist == null)
                    throw new NotFoundException();

                bandPlaylist.Click();
            }
        }

        public IList<string> GetTrackNames()
        {
            return _deezerTracks.Select(element => element.Text.ToLower()).ToList();
        }

        private void ScrollDown(int attempts)
        {
            try
            {
                for (int i = 0; i < attempts; i++)
                {
                    var list = Driver.Instance.FindElements(By.CssSelector("a.title.link-gray"));

                    var point =
                    ((OpenQA.Selenium.Remote.RemoteWebElement)
                        list.Last()).LocationOnScreenOnceScrolledIntoView;
                    Thread.Sleep(1000);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}