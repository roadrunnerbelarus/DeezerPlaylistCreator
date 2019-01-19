using TestFramework.Deezer;
using TestFramework.LastFm;
using TestFramework.SetList;

namespace TestFramework
{
    public static class Pages
    {
        public static LastFmPage LastFmPage => new LastFmPage();
        public static DeezerLoginPage DeezerLoginPage => new DeezerLoginPage();
        public static SetListHomePage SetListHomePage => new SetListHomePage();
    }
}
