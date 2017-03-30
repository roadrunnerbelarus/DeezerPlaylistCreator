using TestFramework.Deezer;
using TestFramework.LastFm;

namespace TestFramework
{
    public static class Pages
    {
        public static LastFmPage LastFmPage => new LastFmPage();
        public static DeezerLoginPage DeezerLoginPage => new DeezerLoginPage();
    }
}
