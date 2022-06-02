using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Upbit_proj.Message;
using Upbit_proj.Model;
using Upbit_proj.Models;
using Upbit_proj.Util;
using Upbit_proj.View;

namespace Upbit_proj.ViewModel
{
    public class PopupResultViewModel:ViewModelBase
    {
        private string _bitcoinName;
        public string BitcoinName { get { return _bitcoinName; } set { Set(nameof(BitcoinName), ref _bitcoinName, value); } }
        private string _sellreason;
        public string SellReason { get { return _sellreason; } set { Set(nameof(SellReason), ref _sellreason, value); } }
        public ICommand OkCommand { get; set; }

        private string Coin = "";
        private string Price = "";
        private string Count = "";
        private string[] coins = new string[200];
        private string[] prices = new string[200];
        private string[] counts = new string[200];
        public PopupResultViewModel(object param, object _param)
        {
            OkCommand = new RelayCommand(() => OK_Command());
            if (param.ToString() == "PRICE_ERR")
            {
                _bitcoinName = "5000원 이상부터 거래가 가능합니다.";
            }
            else if(param.ToString() == "OVER_ERR")
            {
                _bitcoinName = "가지고 있는 금액을 초과했습니다.";
            }
            else if (param.ToString() == "UNDER_ERR")
            {
                _bitcoinName = "지정가가 시장가보다 낮습니다.";
            }
            else if (param.ToString() == "APPOINT")
            {
                _bitcoinName = " 예약했습니다.";
            }
            else if(_param.ToString() == "GOODSELL")
            {
                _bitcoinName = param + "을 매도했습니다. ";
                _sellreason = "3% 상승";
            }
            else if (_param.ToString() == "BADSELL")
            {
                _bitcoinName = param + "을 매도했습니다. ";
                _sellreason = "1.5% 하락";
            }
            else
            {
                _bitcoinName = param as string;
                _bitcoinName = _bitcoinName + "을" + _param + "개 구매하셨습니다.";
                string Left = Global.Money;
            }
            Coin = "";
            Price = "";
            Count = "";
            Common save = new Common("C:\\Test\\Upbit_proj\\Upbit_proj\\SAVE.ini");
            foreach (SaveData item in MainViewModel.Save)
            {
                Coin += item.Market + ",";
                Price += item.Price + ",";
                Count += item.Count + ",";
            }
            
            save.IniWriteValue("SAVE", "MONEY", MainViewModel.MyMoney.ToString());
            save.IniWriteValue("SAVE", "COIN", Coin);
            save.IniWriteValue("SAVE", "COUNT", Count);
            save.IniWriteValue("SAVE", "PRICE", Price);
        }

        private void OK_Command()
        {
            Messenger.Default.Send(new PopupPage(PopupName.Close));
        }
    }
}
