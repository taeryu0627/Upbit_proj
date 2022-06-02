
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upbit_proj.Model.Api;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;

namespace Upbit_proj.Model
{
    public class CandleDayList : ObservableCollection<CandleDayBit>
    {
        public double persent = new double();
        public APIClass apiClass = new APIClass();
        public CandleDayList(string param)
        {
            List<CandleDay> candle = apiClass.GetCandleDays(param.ToString(),DateTime.Now, 10);
            List<Ticker> ticker = apiClass.GetTicker(param);
            persent = Math.Round(ticker[0].signed_change_rate, 2) * 100;
            foreach (CandleDay candleday in candle)
            {
                string[] coin = candleday.market.Split(new char[] { '-' });
                string High = null;
                string Trade = null;
                    High = String.Format("{0:#,0}", Convert.ToInt32(candleday.high_price)) + "원";
                    Trade = String.Format("{0:#,0}", Convert.ToInt32(candleday.trade_price)) + "원";
                    if (candleday.high_price < 100)
                    {
                        High = Math.Round(candleday.high_price, 2).ToString() + "원";
                        Trade = Math.Round(candleday.trade_price, 2).ToString() + "원";
                    }
                string[] result = candleday.candle_date_time_kst.Split(new char[] { 'T' });
                Add(new CandleDayBit()
                {
                    HighPrice = High,
                    TradePrice = Trade,
                    DateTime = result[0],
                    Open = candleday.opening_price,
                    Close = candleday.trade_price,
                    High = candleday.high_price,
                    Low = candleday.low_price,
                    Now = persent.ToString(),
                    Time = Convert.ToDateTime(candleday.candle_date_time_kst)
                });
            }
            
            
        }
    }
    public class CandleDayBit
    {
        public string HighPrice { get; set; }
        public string TradePrice { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public string DateTime { get; set; }
        public string Now { get; set; }
        public DateTime Time { get; set; }
    }
}

