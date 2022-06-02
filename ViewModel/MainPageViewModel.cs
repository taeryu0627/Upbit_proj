using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Upbit_proj.Models;
using Upbit_proj.Models.Api;
using Upbit_proj.Model;
using Upbit_proj.View;
using System.ComponentModel;
using Upbit_proj.Model.Api;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Upbit_proj.Util;
using System.Windows;

namespace Upbit_proj.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private BitcoinList items;
        public static List<SaveData> MyCoin { get; set; } = new List<SaveData>();
        public APIClass apiClass = new APIClass();
        public ICommand CloseBtn { get; set; }
        public ICommand RefreshBtn { get; set; }
        private string Coin = "";
        private string Price = "";
        private string Count = "";


        public MainPageViewModel()
        {
            
            this.items = new BitcoinList();
            CloseBtn = new RelayCommand(() => CloseCommand());
            RefreshBtn = new RelayCommand(() => RefreshCommand());
            Refresh();
        }

        private void RefreshCommand()
        {
            Refresh();
            MainPageView view = new MainPageView();
            view.MyListView.Items.Refresh();
        }
        private void CloseCommand()
        {
            Common common = new Common("C:\\Test\\Upbit_proj\\Upbit_proj\\IP.ini");
            common.IniWriteValue("PROJECT", "MONEY", (Double.Parse(Global.Money) - MainViewModel.UseMoney).ToString());
            Common save = new Common("C:\\Test\\Upbit_proj\\Upbit_proj\\SAVE.ini");
            foreach(SaveData item in MainViewModel.Save)
            {
                Coin += item.Market + ",";
                Price += item.Price + ",";
                Count += item.Count + ",";
            }
            save.IniWriteValue("SAVE", "MONEY", MainViewModel.MyMoney.ToString());
            save.IniWriteValue("SAVE", "COIN", Coin);
            save.IniWriteValue("SAVE", "COUNT", Count);
            save.IniWriteValue("SAVE", "PRICE", Price);
            Application.Current.MainWindow.Close();
        }
        private void Refresh()
        {
            MyCoin.Clear();
            foreach (SaveData data in MainViewModel.Save)
            {
                List<Ticker> tickerList = apiClass.GetTicker(data.Market);
                if(tickerList != null)
                {
                    try
                    {
                        MyCoin.Add(new SaveData
                        {
                            Market = data.Market,
                            Count = data.Count,
                            Price = data.Price,
                            NowCount = tickerList[0].trade_price.ToString(),
                            Persent = Math.Round((tickerList[0].trade_price / data.Price - 1), 3) * 100,
                        });
                    }
                    catch (Exception ex)
                    {
                        MyCoin.Add(new SaveData
                        {
                            Market = "ERR",

                        });
                    }
                }
                else
                {
                    MyCoin.Add(new SaveData
                    {
                        Market = "ERR",

                    });
                }
            }
            RaisePropertyChanged(nameof(MyCoin));
        }
        public BitcoinList Items
        {
            get { return this.items; }
            
        }
    }
}
