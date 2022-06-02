using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit_proj.Model
{
    public class Ticker
    {
        public string market;
        public double opening_price;
        public double high_price;
        public double low_price;
        public double trade_price;
        public string trade_time_kst;
        public long timestamp;
        public double prev_closing_price;
        public string change;
        public double change_price;
        public double change_rate;
        public double signed_change_price;
        public double signed_change_rate;
        public int unit;

    }
}
