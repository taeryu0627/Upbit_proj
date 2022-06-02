using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Upbit_proj.Message;
using Upbit_proj.Model;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;
using Upbit_proj.Util;

namespace Upbit_proj.ViewModel
{
    public class PopupBuyViewModel:ViewModelBase
    {
        private string _bitcoinName;
        public string BitcoinName { get { return _bitcoinName; } set { Set(nameof(BitcoinName), ref _bitcoinName, value); } }

        private string _marketPrice;
        public string MarketPrice { get { return _marketPrice; } set { Set(nameof(MarketPrice), ref _marketPrice, value); } }
        private string _selectPrice;
        public string SelectPrice { get { return _selectPrice; } set { Set(nameof(SelectPrice), ref _selectPrice, value); } }
        private string _paymentPrice;
        public string PaymentPrice
        {
            get { return _paymentPrice; }
            set
            {
                _paymentPrice = value;
                if(_paymentPrice != "")
                {
                    bitcoin = Double.Parse(_paymentPrice) / Double.Parse(_selectPrice);
                    MyPrice = Double.Parse(_paymentPrice);
                    coin = Convert.ToDouble(Math.Round(bitcoin, 8).ToString());
                    _bitcoin = coin.ToString("f8");
                    RaisePropertyChanged(nameof(Bitcoin));
                }
            }
        }
        private string _bitcoin;
        public string Bitcoin { get { return _bitcoin; } set { Set(nameof(Bitcoin), ref _bitcoin, value); } }

        public ICommand TenCommand { get; set; }
        public ICommand QuarterCommand { get; set; }
        public ICommand HalfCommand { get; set; }
        public ICommand AllCommand { get; set; }
        public ICommand OkCommand { get; set; }

        public double MyPrice;
        public double bitcoin = new double();
        public List<Ticker> price;
        public List<SaveData> SaveDataList = new List<SaveData>();

        private double coin;

        public PopupBuyViewModel(object param)
        {
            MyPrice = MainViewModel.MyMoney;
            _bitcoinName = param.ToString();
            _paymentPrice = MyPrice.ToString();
            APIClass apiclass = new APIClass();
            price = apiclass.GetTicker(_bitcoinName);
            _selectPrice = price[0].trade_price.ToString();
            bitcoin = Double.Parse(_paymentPrice) / Double.Parse(_selectPrice);
            _bitcoin = bitcoin.ToString();

            TenCommand = new RelayCommand(() => T_Command());
            QuarterCommand = new RelayCommand(() => Q_Command());
            HalfCommand = new RelayCommand(() => H_Command());
            AllCommand = new RelayCommand(() => A_Command());
            OkCommand = new RelayCommand(() => OK_Command());
            foreach (Ticker ticker in price)
            {
                _marketPrice = ticker.trade_price.ToString();
            }
        }

        private void T_Command()
        {
            SendPrice(0.1);
        }
        private void Q_Command()
        {
            SendPrice(0.25);
        }
        private void H_Command()
        {
            SendPrice(0.5);
        }
        private void A_Command()
        {
            SendPrice(1.0);
        }
        public void OK_Command()
        {
            if(Double.Parse(PaymentPrice) < 5000)
            {
                Messenger.Default.Send(new PopupPage(PopupName.Result, "PRICE_ERR", _bitcoin));
            }
            else if (Double.Parse(PaymentPrice) > MainViewModel.MyMoney)
            {
                Messenger.Default.Send(new PopupPage(PopupName.Result, "OVER_ERR", _bitcoin));
            }
            else
            {
                MainViewModel.Appoint.Add(new AppointData { Select = Double.Parse(SelectPrice), Market = _bitcoinName, Count = Math.Round(Double.Parse(_bitcoin), 8).ToString() });
                Messenger.Default.Send(new PopupPage(PopupName.Result, "APPOINT", _bitcoin));
            }
        }
            
        private void SendPrice(double a)
        {
            MyPrice = MainViewModel.MyMoney;
            MyPrice = MyPrice * a;
            _paymentPrice = MyPrice.ToString();
            RaisePropertyChanged(nameof(PaymentPrice));
            bitcoin = Double.Parse(_paymentPrice) / Double.Parse(_selectPrice);
            coin = Convert.ToDouble(Math.Round(bitcoin, 8).ToString());
            
            _bitcoin = coin.ToString("f8");
            RaisePropertyChanged(nameof(Bitcoin));
        }
    }
}
