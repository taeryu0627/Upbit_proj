using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit_proj.Model.Api
{
    public class CandleMinute
    {
        public string market;
        public string candle_date_time_utc;
        public string candle_date_time_kst;
        public double opening_price;
        public double high_price;
        public double lower_price;
        public double trade_price;
        public long timestamp;
        public double candle_acc_trade_price;
        public double candle_acc_trade_volume;
        public int unit;

    }
}
