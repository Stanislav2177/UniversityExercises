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

namespace Csharp_5upr_Svetlin_49
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            GenerateButton.IsEnabled = false;
            StartingTimeLabel.Content = "Start Time: " + DateTime.Now.ToString();
            await SlowWork();
            EndTimeLabel.Content = "End Time:" + DateTime.Now.ToString();
            GenerateButton.IsEnabled = true;

        }


        public async Task SlowWork()
        {
            for (int i = 0; i <= 100; i++)
            {
                NumberLabel.Content = i.ToString();
                await Task.Delay(100);
            }
        }
    }
}
