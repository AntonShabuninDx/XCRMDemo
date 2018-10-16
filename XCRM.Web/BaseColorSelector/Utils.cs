using System.Web;
using System.Linq;
using System.Drawing;
using DevExpress.ExpressApp.Utils;

namespace DevExpress.Web.Demos {

    public static class Utils {
        public const string CurrentBaseColorCookieKey = "DXCurrentBaseColor";
        public const string CurrentFontCookieKey = "DXCurrentFont";

        public static string CurrentBaseColor {
            get {
                if(HttpContext.Current.Request.Cookies[CurrentBaseColorCookieKey] != null)
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[CurrentBaseColorCookieKey].Value);
                return CurrentThemeDefaultBaseColor;
            }
        }
        public static string CurrentFont {
            get {
                if(HttpContext.Current.Request.Cookies[CurrentFontCookieKey] != null)
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[CurrentFontCookieKey].Value);
                return CurrentThemeDefaultFont;
            }
        }
        public static object GetFontFamiliesDataSource() {
            return CustomFontFamilies.Select(f => new { Text = GetShortFontName(f), Value = f });
        }
        static string GetShortFontName(string fullName) {
            if(string.IsNullOrWhiteSpace(fullName))
                return fullName;
            return fullName.Substring(fullName.IndexOf(' ') + 1, fullName.IndexOf(',') - fullName.IndexOf(' ') - 1).Trim('\'');
        }        
        public static string CurrentThemeDefaultBaseColor {
            get {
                return "#2c86d3";
            }
        }
        static string[] customBaseColors;
        public static string[] CustomBaseColors {
            get {
                if(customBaseColors == null)
                    customBaseColors = new string[] {
                    CurrentThemeDefaultBaseColor,
                    "#35B86B",
                    "#CE4776",
                    "#F7A233",
                    "#9F47CE",
                    "#5C57C9",
                    "#98CE47",
                    "#8C8C8C"
                };
                return customBaseColors;
            }
        }
        public static string CurrentThemeDefaultFont {
            get { return CurrentThemeDefaultFontSize + " " + CurrentThemeDefaultFontFamily; }
        }
        public static string CurrentThemeDefaultFontFamily {
            get {
                return "'Segoe UI','Helvetica Neue','Droid Sans',Arial,Tahoma,Geneva,Sans-serif";
            }
        }
        public static string CurrentThemeDefaultFontSize {
            get {
                return "14px";
            }
        }
        static string[] customFontFamilies;
        public static string[] CustomFontFamilies {
            get {
                if(customFontFamilies == null) {
                    customFontFamilies = new string[] { 
                        CurrentThemeDefaultFont, 
                        CurrentThemeDefaultFontSize + " " + "'Coda', normal",
                        CurrentThemeDefaultFontSize + " " + "'Verdana', normal",
                        CurrentThemeDefaultFontSize + " " + "'Comfortaa', normal"
                    };
                }
                return customFontFamilies;
            }
        }
    }
}
