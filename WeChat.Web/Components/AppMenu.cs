using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WeChat.Data.Components
{
    public class AppMenu
    {
        internal const string MENU_FILE_NAME = "menu.json";

        public static void Save(string json)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppMenu.MENU_FILE_NAME);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }

        public static string Read()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppMenu.MENU_FILE_NAME);
            return File.ReadAllText(fileName, Encoding.UTF8); 
        }

    }
}