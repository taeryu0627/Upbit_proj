using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using Upbit_proj.Message;
using Upbit_proj.Model;

namespace Upbit_proj.View
{
    public partial class MainPageView : UserControl
    {
        private Timer aTimer;
        private readonly double cycleTime = 1000; // 1초
        public MainPageView()
        {
            InitializeComponent();
            SetTimer();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Bitcoin bitcoin = ((ListViewItem)sender).Content as Bitcoin;
                Messenger.Default.Send(new GoToPage(PageName.Selected, bitcoin.Market));
            }
            catch
            {
                SaveData bitcoin = ((ListViewItem)sender).Content as SaveData;
                Messenger.Default.Send(new GoToPage(PageName.Selected, bitcoin.Market));
            }
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs e)
        {

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
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                MyListView.Items.Refresh(); 
            }));
        }
    }
}
