using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Upbit_proj.Model;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;
using Upbit_proj.Util;
using Upbit_proj.View;
using LiveCharts;
using LiveCharts.Defaults; //Contains the already defined types
using LiveCharts.Helpers;
using Upbit_proj.Model.Api;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Upbit_proj.Message;

namespace Upbit_proj.ViewModel
{
    public class SelectedViewModel: ViewModelBase
    {
        private string _selectedCoin;
        public string SelectedCoin { get { return _selectedCoin; } set { Set(nameof(SelectedCoin), ref _selectedCoin, value); } }

        private string _coinName;
        public string CoinName { get { return _coinName; } set { Set(nameof(CoinName), ref _coinName, value); } }

        private string _nowPersent;
        public string NowPersent { get { return _nowPersent; } set { Set(nameof(NowPersent), ref _nowPersent, value); } }

        private string _colors;
        public string Colors { get { return _colors; } set { Set(nameof(Colors), ref _colors, value); } }

        public SeriesCollection SeriesCollection { get; set; }

        private DateTime _initialDateTime;
        public DateTime InitialDateTime { get { return _initialDateTime; } set { Set(nameof(InitialDateTime), ref _initialDateTime, value); } }
        public PeriodUnits Period { get; set; }
        public IAxisWindow SelectedWindow { get; set; }
        public Func<Double, String> Formatter { get; set; }

        public ChartValues<double> Values1 { get; set; }
        public List<double> Value;
        public ChartValues<double> Values2 { get; set; }

        private SavePriceList candle;
        private Timer aTimer;
        private readonly double cycleTime = 1000; // 10초
        public List<SavePrice> Items { get; set; } = new List<SavePrice>();
        private string Name;
        private CandleMinList candlemin;
        private int CheckPoint = 0;
        private List<SavePrice> list = new List<SavePrice>();
        private ChartValues<double> cv = new ChartValues<double>();
        private string checkTime = null;
        public ICommand SellCommand { get; set; }
        public ICommand BuyCommand { get; set; }

        public SelectedViewModel(object param)
        {
            APIClass apiClass = new APIClass();
            Name = param.ToString();
            BuyCommand = new RelayCommand(() => B_Command());
            SellCommand = new RelayCommand(() => S_Command());

            CandleDayList candleday = new CandleDayList(Name);
            candlemin = new CandleMinList(Name);

            foreach (CandleMinBit item in candlemin)
            {
                cv.Add(item.TradePrice);
            }
            Values1 = cv;
            List<MarketAll> market = apiClass.GetMarketAll();
            _coinName = param.ToString();
            foreach (MarketAll item in market)
            {
                if(param.ToString() == item.market)
                {
                    _selectedCoin = item.korean_name + _coinName;
                }
            }
            
            _nowPersent = candleday[0].Now + "%";
            if(_nowPersent[0] == '-')
            {
                _colors = "Blue";
            }
            else if(_nowPersent[0] == '0')
            {
                _colors = "Gray";
            }
            else
            {
                _colors = "Red";
            }
            SetTimer();
        }
        private void B_Command()
        {
            Messenger.Default.Send(new PopupPage(PopupName.Buy, Name));
        }

        private void S_Command()
        {
            Messenger.Default.Send(new PopupPage(PopupName.Buy, Name));
        }
        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(cycleTime);

            // 이벤트 핸들러 연결
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Timer에서 Elapsed 이벤트를 반복해서 발생
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.candle = new SavePriceList(Name);
            string time = candle[0].Time.ToString();
            string[] hms = time.Split(':');
            if (checkTime == null) checkTime = hms[1];
            cv.RemoveAt(cv.Count - 1);
            cv.Add(candle[0].TradePrice);
            if (checkTime != hms[1])
            {
                if (cv.Count > 5)
                {
                    cv.RemoveAt(0);
                }
                cv.Add(candle[0].TradePrice);
                Values1 = cv;
                Values1 = cv;
                list.Add(candle[0]);
                Items = list;
                CheckPoint = 0;
                RaisePropertyChanged(nameof(Items));
                checkTime = hms[1];
            }

        }
    }
}
