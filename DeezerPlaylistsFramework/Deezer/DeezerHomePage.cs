namespace TestFramework.Deezer
{
    public class DeezerPageBase : PageBase
    {
        public SideBar SideBar => new SideBar();
    }

    public class DeezerHomePage : DeezerPageBase
    {
        internal DeezerHomePage()
        {
        }

        public new bool IsAt()
        {
            return SideBar.IsDisplayed();
        }
    }
}