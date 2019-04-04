using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web;

namespace ApiStackNet.BLL.Service
{
    public class TranslateService
    {
        public string CurrentLanguage

        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.Headers.Get("language") ?? "en";
                }
                catch (Exception)
                {
                    return "en";
                }
            }
        }

        public CultureInfo CurrentCulture { get => CultureInfo.GetCultureInfo(this.CurrentLanguage); }

        public string GetTextForCurrentCulture(string key)
        {
            ResourceManager res = new ResourceManager("Resources.Resources", Assembly.Load("App_GlobalResources"));
            var result = res.GetString(key, this.CurrentCulture);
            return result;
        }
    }
}
