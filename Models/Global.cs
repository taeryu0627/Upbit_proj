using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upbit_proj.Util;

namespace Upbit_proj.Models
{
    public class Global
    {
        public static string Ini { get; set; } = "C:\\Test\\Upbit_proj\\Upbit_proj\\IP.ini";
        public static string ServerURL { get; set; } = Common.GetProfile("PROJECT", "IP", Ini);
        public static string AccessKey { get; set; } = Common.GetProfile("PROJECT", "ACCESSKEY", Ini);
        public static string SecretKey { get; set; } = Common.GetProfile("PROJECT", "SECRETKEY", Ini);
        public static string Money { get; set; } = Common.GetProfile("PROJECT", "MONEY", Ini);

        public static string Save { get; set; } = "C:\\Test\\Upbit_proj\\Upbit_proj\\SAVE.ini";
        public static string Coin { get; set; } = Common.GetProfile("SAVE", "COIN", Save);
        public static string Count { get; set; } = Common.GetProfile("SAVE", "COUNT", Save);
        public static string Price { get; set; } = Common.GetProfile("SAVE", "PRICE", Save);
        public static string SaveMoney { get; set; } = Common.GetProfile("SAVE", "MONEY", Save);

        static Global()
        {

        }
    }
}
