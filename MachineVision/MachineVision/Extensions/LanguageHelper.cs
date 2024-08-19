using System.Collections;
using System.Windows;

namespace MachineVision.Extensions
{
    public static class LanguageHelper
    {
        public static string AppCurrentLanguage { get; set; }
        public static Dictionary<string, string> KeyValues { get; set; }

        public static void SetLanguage(string key)
        {
            var resource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                t => t.Source != null &&
                t.Source.OriginalString != null &&
                t.Source.OriginalString.Contains(key));

            if (resource != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resource);
            }
            Application.Current.Resources.MergedDictionaries.Add(resource);

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            foreach (DictionaryEntry item in resource)
            {
                keyValues.Add(item.Key.ToString(), item.Value.ToString());
            }

            AppCurrentLanguage = key;
            KeyValues = keyValues;

            // 设置当前线程的用户界面文化（UICulture），即应用程序在当前线程中使用的语言和区域性设置。
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(key);
        }
    }
}
