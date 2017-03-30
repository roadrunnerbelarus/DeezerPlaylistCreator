using System;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework
{
    public abstract class PageBase
    {
        protected virtual string PageTitle { get; set;} 

        public virtual bool IsAt()
        {
            return string.Equals(Driver.Title, PageTitle, StringComparison.InvariantCulture);
        }

        protected PageBase(string url) : this()
        {
            Driver.Instance.Url = url;
        }

        protected PageBase()
        {
            PageFactory.InitElements(Driver.Instance, this);
        }
    }
}