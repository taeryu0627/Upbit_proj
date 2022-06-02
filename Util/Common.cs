using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Upbit_proj.Util
{
    public class Common
    {
        public string path;
        public Common(string INIPath)
        {
            path = INIPath;
        }

        [DllImport("kernel32")] 
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath); 
        [DllImport("kernel32")] 
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        
        public static string GetProfile(string Section, string Key, string Ini)
        {
            StringBuilder temp = new StringBuilder(3000);
            int i = GetPrivateProfileString(Section, Key, string.Empty, temp, 3000, Ini);
            return temp.ToString();
        }
        public void IniWriteValue(string Section, string Key, string Value)
        {
        // WritePrivateProfileString("카테고리", "Key값", "Value", "저장할 경로");
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        internal void IniWriteValue(string v1, string v2, object p)
        {
            throw new NotImplementedException();
        }
    }
}
