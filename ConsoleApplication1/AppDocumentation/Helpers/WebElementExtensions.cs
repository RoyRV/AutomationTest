using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentation.Helpers
{
    static class WebElementExtensions
    {
        public static void SetText(this IWebElement e, string text)
        {
            e.Click();
            e.Clear();
            e.SendKeys(text);
        }

        public static string GetText(this IWebElement e)
        {
            string webElementText = e.Text;
            if (String.IsNullOrEmpty(webElementText))
            {
                string webElementTextAttribute = e.GetAttribute("value");
                if (!String.IsNullOrEmpty(webElementTextAttribute)) return webElementTextAttribute;
            }
            return webElementText;
        }
    }
}
