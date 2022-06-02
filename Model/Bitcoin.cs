using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Upbit_proj.Model.Api;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;

namespace Upbit_proj.Model
{
    public class BitcoinList : ObservableCollection<Bitcoin>
    {
        APIClass apiClass = new APIClass();
        public  BitcoinList()
        {
            
            List<MarketAll> market = apiClass.GetMarketAll();
            
            foreach (MarketAll bitcoin in market)
            {
                string[] coin = bitcoin.market.Split(new char[] { '-' });
                
                if (coin[0] == "KRW" && coin[1] != "BTT")
                {

                    Add(new Bitcoin()
                    {
                        Market = bitcoin.market,
                        Korean = bitcoin.korean_name,
                        English = bitcoin.english_name,
                        Warning = bitcoin.market_warning,
                    });
                }
            }
        }
    }
    public class Bitcoin
    {
        public string Market { get; set; }
        public string Korean { get; set; }
        public string English { get; set; }
        public string Warning { get; set; }
        public string Amount { get; set; }
    }
}


