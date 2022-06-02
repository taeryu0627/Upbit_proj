using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upbit_proj.Model.Api;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;
using Upbit_proj.Util;

namespace Upbit_proj.Model
{
    
    public class CandleMinList : ObservableCollection<CandleMinBit>
    {
        public double persent = new double();
        public CandleMinList(string param)
        {
            APIClass apiClass = new APIClass();
            List<CandleMinute> candle = apiClass.GetCandleMinutes(param, Models.Api.APIClass.MinuteUnit._1, DateTime.Now, 4);

            foreach (CandleMinute candleMinute in candle)
            {
                Add(new CandleMinBit()
                {
                    Market = candleMinute.market,
                    TradePrice = candleMinute.trade_price,
                    ACC = candleMinute.candle_acc_trade_price,
                    Time = candleMinute.candle_date_time_kst
                });
            }
        }

    }

    public class CandleMinBit
    {
        public double ACC { get; set; }
        public double TradePrice { get; set; }
        public string Market { get; set; }
        public string Time { get; set; }
    }
}

