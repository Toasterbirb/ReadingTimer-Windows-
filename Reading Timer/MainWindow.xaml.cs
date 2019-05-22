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

namespace Reading_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private double pagesRead = 0;
        private double seconds = 0;

        private string status = "null";

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(timer_tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void ReadPages(object sender, RoutedEventArgs e)
        {
            if(status == "null")
            {
                if(curPage.Text != "" && pageCount.Text != "")
                {
                    readButton.Content = "Stop reading";
                    status = "reading";
                    dispatcherTimer.Start();
                }
            }
            else if (status == "reading")
            {
                dispatcherTimer.Stop();
                status = "null";
                pagesRead += 5;
                curPage.Text = (int.Parse(curPage.Text) + 5).ToString();


                //Update stats window
                onePage.Content = "One page: " + calcTime(Math.Round(seconds / pagesRead));


                //15 minutes = 900 seconds
                pagesPer15.Content = "Pages / 15 min: " + Math.Round(900.00 / (seconds / pagesRead), 1);
                
                    
                estTimeLeft.Content = "Estimated time left: " + calcTime(Math.Round((double.Parse(pageCount.Text) - double.Parse(curPage.Text)) * (seconds / pagesRead)));
                avgTotalCount.Content = "Average of " + pagesRead + " pages";

                readButton.Content = "Start reading";
            }
        }

        private string calcTime(double seconds)
        {
            string result = "";
            int minutes = 0;
            int hours = 0;
            double tempSeconds = seconds;

            if (tempSeconds >= 60)
            {
                while (true)
                {
                    if (tempSeconds >= 60)
                    {
                        tempSeconds -= 60;
                        minutes++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (minutes >= 60)
            {
                while (true)
                {
                    if (minutes >= 60)
                    {
                        minutes -= 60;
                        hours++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if(hours > 0)
            {
                result += hours + "h ";
            }
            if(minutes > 0)
            {
                result += minutes + "min ";
            }
            if(tempSeconds > 0)
            {
                result += tempSeconds + "s";
            }

            return result;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            seconds++;
        }
    }
}
