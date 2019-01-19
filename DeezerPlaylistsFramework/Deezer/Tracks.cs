// ReSharper disable CollectionNeverUpdated.Local

#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.Deezer
{
    public class Tracks : DeezerPageBase
    {
        [FindsBy(How = How.CssSelector, Using = "div.datagrid-row.song")] private IWebElement _tracks;
        
        internal Tracks()
        {
            //Driver.Wait.Until(d => _tracks.Displayed);
        }

        // This mechanism probably should be re-implemented
        // to search for songs in proper albums
        public void AddTracksToPlaylist(List<string> tracksToAdd, string playlistName)
        {
            var deezerTrackEls = GetTrackElements();
            int numOfTracksBeforeScroll = 6;
            deezerTrackEls.RemoveRange(numOfTracksBeforeScroll, deezerTrackEls.Count - numOfTracksBeforeScroll);
            AddToPlaylist(ref tracksToAdd, deezerTrackEls, playlistName);

            for (int i = 0; i < 5; i++)
            {
                if(tracksToAdd.Count == 0)
                    break;

                string trackOnBottomText = deezerTrackEls.Last().Text;

                ScrollToTrack(deezerTrackEls.Last());

                Thread.Sleep(TimeSpan.FromSeconds(1));
                deezerTrackEls = GetTrackElements();

                int trackOnTopIndex = deezerTrackEls.FindIndex(el => el.Text.Equals(trackOnBottomText));
                deezerTrackEls.RemoveRange(0, trackOnTopIndex + 1);

                int visibleTracks = 16;
                if (deezerTrackEls.Count < visibleTracks)
                {
                    AddToPlaylist(ref tracksToAdd, deezerTrackEls, playlistName);
                    break;
                }
                deezerTrackEls.RemoveRange(visibleTracks, deezerTrackEls.Count - visibleTracks);
                AddToPlaylist(ref tracksToAdd, deezerTrackEls, playlistName);
            }
        }

        private int numberOfAddedTracks;

        private void AddToPlaylist(ref List<string> tracksToAdd, List<IWebElement> deezerTrackEls, string playlistName)
        {
            var listTrackRows = new List<IWebElement>();

            foreach (var trackToAdd in tracksToAdd)
            foreach (var deezerTrack in deezerTrackEls)
            {
                var deezerTrackTitle = deezerTrack
                    .FindElement(By.CssSelector("a.datagrid-label.datagrid-label-main.title"))
                    .Text.ToLower();

                if (!string.Equals(deezerTrackTitle, trackToAdd,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    if (deezerTrackTitle.Contains("(live") || deezerTrackTitle.Contains("demo") ||
                        deezerTrackTitle.Contains("remix"))
                        continue;

                    if (!trackToAdd.Contains(deezerTrackTitle))
                        if (!deezerTrackTitle.Contains(trackToAdd))
                            continue;
                }

                listTrackRows.Add(deezerTrack);

                deezerTrackEls = deezerTrackEls.Where(tr => !tr.Equals(deezerTrack)).ToList();
                tracksToAdd = tracksToAdd.Where(tr => !tr.Equals(trackToAdd)).ToList();

                break;
            }

            if (listTrackRows.Count == 0)
                return;

            foreach (var elTrackRow in listTrackRows)
            {
                // For history
                //var el =
                //    Instance.Instance.FindElement(
                //        By.XPath($"//a[contains(text(),\"{song.Text.Trim()}\")]/parent::div/parent::div"));

                var btnAddtoPlaylist =
                    elTrackRow.FindElement(By.CssSelector("button.action-item-btn.datagrid-action"));

                Thread.Sleep(1000);
                var actions = new Actions(Driver.Instance);
                actions.MoveToElement(elTrackRow).Build().Perform();

                Driver.Wait.Until(d => btnAddtoPlaylist.Displayed);
                btnAddtoPlaylist.Click();

                var addToPlaylistMenu = Driver.Instance.FindElement(By.CssSelector("div.page-contextmenu.is-opened"));
                var bandPlaylist =
                    addToPlaylistMenu.FindElements(
                            By.XPath(
                                $"//span[contains(@class,'label ellipsis')][contains(text(),\"{playlistName}\")]"))
                        .LastOrDefault();

                if (bandPlaylist == null)
                    throw new NotFoundException();

                bandPlaylist.Click();

                numberOfAddedTracks++;
            }
        }

        private static List<IWebElement> GetTrackElements()
        {
            return Driver.Instance
                .FindElements(By.CssSelector("div.datagrid-row.song")).ToList();
        }

        private static void ScrollToTrack(IWebElement trackElement)
        {
            var js = (IJavaScriptExecutor)Driver.Instance;
            for (int i = 0; i < 2; i++)
            {
                js.ExecuteScript($"window.scrollTo(0,{trackElement.Location.Y});");
                Thread.Sleep(1000);
            }
        }

        public IList<string> GetTrackNames()
        {
            // TO SHOW
            //try
            //{
            //    Driver.Wait.Until(d => _deezerTracks.FirstOrDefault().Displayed);
            //    return _deezerTracks.Select(element => element.Text.ToLower()).ToList();
            //}
            //catch (NullReferenceException)
            //{
            //    return new List<string>();
            //}

            //Driver.Wait.Until(d =>
            //{
            //    try
            //    {
            //        return deezerTracks.FirstOrDefault().Displayed;
            //    }
            //    catch (NullReferenceException)
            //    {
            //        return true;
            //    }
            //});

            var deezerTracks = GetTrackElements();
            var trackNames = deezerTracks.Select(element => element.Text.ToLower()).ToList();

            return trackNames;
        }

        private void ScrollDown(int attempts)
        {
            try
            {
                // v1:

                //for (int i = 0; i < attempts; i++)
                //{
                //    var list = Driver.Instance.FindElements(By.CssSelector("div.datagrid-row.song"));

                //    var point =
                //    ((OpenQA.Selenium.Remote.RemoteWebElement)
                //        list.Last()).LocationOnScreenOnceScrolledIntoView;

                //    // unavoidable evil
                //    Thread.Sleep(1000);
                //}

                // v2:
                // inappropriate as the page can handle <= 45 songs 

                //var actions = new Actions(Driver.Instance);
                //for (int i = 0; i < attempts; i++)
                //{
                //     actions.KeyDown(Keys.Control).SendKeys(Keys.End).Perform();

                //    // unavoidable evil
                //    Thread.Sleep(1000);
                //}

                //v3:

                for (var i = 0; i < attempts; i++)
                {
                    Driver.Instance.FindElement(By.CssSelector("input#menu_search")).SendKeys(Keys.PageDown);
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