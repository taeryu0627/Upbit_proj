using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Timers;
using Upbit_proj.Message;
using Upbit_proj.Model;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;
using Upbit_proj.Util;

namespace Upbit_proj.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private ViewModelBase _currentViewModel;

        public static double MyMoney;
        public static double UseMoney = 0;
        private Timer aTimer;
        private readonly double cycleTime = 5000; // 1초
        public static List<AppointData> Appoint = new List<AppointData>();
        public static List<SaveData> Save = new List<SaveData>();
        public static List<SaveData> Panic = new List<SaveData>();
        public List<Ticker> ticker = new List<Ticker>();
        private string[] coins = new string[200];
        private string[] prices = new string[200];
        private string[] counts = new string[200];
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                Set(nameof(CurrentViewModel), ref _currentViewModel, value);
            }
        }


        private ViewModelBase _popViewModel;
        public ViewModelBase PopViewModel
        {
            get
            {
                return _popViewModel;
            }
            set
            {
                Set(nameof(PopViewModel), ref _popViewModel, value);
            }
        }

        public MainViewModel(IDataService dataService)
        {
            if(Global.SaveMoney == "") MyMoney = double.Parse(Global.Money);
            else MyMoney = double.Parse(Global.SaveMoney);
            if(Global.Coin != "")
            {
                coins = Global.Coin.Split(',');
                prices = Global.Price.Split(',');
                counts = Global.Count.Split(',');
            }
            if(coins[0] != null)
            {
                for (int i = 0; i < coins.Length; i++)
                {
                    if (coins[i] == "") break;
                    Save.Add(new SaveData { Market = coins[i], Price = double.Parse(prices[i]), Count = counts[i] });
                }
            }
            try
            {
                SetTimer();
            }
            catch
            {

            }
            Messenger.Default.Register<GoToPage>(this, (action) => ReceiveMessage(action));
            Messenger.Default.Register<PopupPage>(this, (action) => ReceiveMessage2(action));
            Messenger.Default.Send(new GoToPage(PageName.M_P));
        }

        private object ReceiveMessage(GoToPage action)
        {
            switch (action.PageName)
            {
                //메인
                case PageName.M_P:
                    CurrentViewModel = new MainPageViewModel();
                    break;
                case PageName.Selected:
                    CurrentViewModel = new SelectedViewModel(action.Param);
                    break;
                default:
                    break;
            }
            return null;
        }

        private object ReceiveMessage2(PopupPage action)
        {
            switch (action.PopupName)
            {
                case PopupName.Buy:
                    PopViewModel = new PopupBuyViewModel(action.Param);
                    break;
                case PopupName.Result:
                    PopViewModel = new PopupResultViewModel(action.Param, action.Params);
                    break;
                case PopupName.Close:
                    PopViewModel = null;
                    break;
                default:
                    break;
            }
            return null;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            APIClass apiClass = new APIClass();
            if (Save.Count != 0)
            {
                for(int i = 0; i<Save.Count; i++)
                {
                    SaveData item = Save[i];
                    ticker = apiClass.GetTicker(item.Market);
                    if(ticker != null)
                    {
                        try
                        {
                            if (ticker[0].trade_price >= item.Price * 1.015 || ticker[0].trade_price <= item.Price * 0.985)
                            {
                                MyMoney += (ticker[0].trade_price * double.Parse(item.Count));
                                Panic.Add(new SaveData { Market = item.Market, Price = ticker[0].trade_price, Count = item.Count });
                                Save.Remove(item);
                                Messenger.Default.Send(new PopupPage(PopupName.Result, item.Market, item.Count));
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            if (Panic.Count != 0)
            {
                foreach (SaveData item in Panic)
                {
                    ticker = apiClass.GetTicker(item.Market);
                    if(ticker != null)
                    {
                        if (ticker[0].trade_price <= item.Price * 0.985)
                        {
                            MyMoney -= (ticker[0].trade_price * double.Parse(item.Count));
                            Save.Add(new SaveData { Market = item.Market, Price = ticker[0].trade_price, Count = item.Count });
                            Panic.Remove(item);
                            Messenger.Default.Send(new PopupPage(PopupName.Result, item.Market, item.Count));
                        }
                    }
                }
            }
            if(Appoint.Count != 0)
            {
                for(int i = 0; i< Appoint.Count; i++)
                {
                    AppointData item = Appoint[i];
                    ticker = apiClass.GetTicker(item.Market);
                    if (ticker != null)
                    {
                        if (ticker[0].trade_price <= item.Select)
                        {
                            MyMoney -= (ticker[0].trade_price * double.Parse(item.Count));
                            Save.Add(new SaveData { Market = item.Market, Price = item.Select, Count = item.Count });
                            Appoint.Remove(item);
                            Messenger.Default.Send(new PopupPage(PopupName.Result, item.Market, item.Count));
                        }
                    }
                }
            }
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

    }
}