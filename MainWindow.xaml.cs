
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Upbit_proj.Models.Api;
using Upbit_proj.ViewModel;

namespace Upbit_proj
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();
            MainWindow window = App.Current.MainWindow as MainWindow;

            Top = Properties.Settings.Default.Top;
            Left = Properties.Settings.Default.Left;
            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
        }
    }
}
