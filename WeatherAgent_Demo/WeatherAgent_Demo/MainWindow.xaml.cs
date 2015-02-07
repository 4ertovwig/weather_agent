using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Threading;

namespace WeatherAgent_Demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Time { get { return DateTime.Now.ToString("dd MMMM yyyy");} }

        #region Binding members
        private BitmapImage pathImage;
        public BitmapImage PathImage
        {
            get { return pathImage; }
        }

        public string Temperature
        {
            get 
            {
                if (ValuesOfResponse.Temperature != null)
                    return ValuesOfResponse.Temperature;
                else return "Подождите...";
            }
        }

        public string TypeOfWeather
        {
            get 
            {
                if (ValuesOfResponse.Type != null)
                    return ValuesOfResponse.Type;
                else return "Подождите...";
            }
        }

        public string WindSpeed
        {
            get
            {
                if (ValuesOfResponse.windSpeed != null)
                    return ValuesOfResponse.windSpeed;
                else return "Подождите...";
            }
        }

        public string Pressure
        {
            get
            {
                if (ValuesOfResponse.Pressure != null)
                    return ValuesOfResponse.Pressure;
                else return "Подождите...";
            }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(() => 
            { 
            //    RequestYandex.GET("27612.xml");
            //    RequestYandex.ParseXml();

            /// понадобилось чтобы привязать картинку к PathImage
                   this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,          
                   (ThreadStart)(() =>
                   {
                       RequestYandex.GET("27612.xml");
                       RequestYandex.ParseXml();
                       pathImage = RequestYandex.PathIcon();
                   })); 
            }).Wait();

            InfoWeather.Click += new RoutedEventHandler((sender, e) => 
            {
                InfoAboutWeather infabweath = new InfoAboutWeather();
                infabweath.ShowDialog();
            });

            this.MainWeather.MouseLeftButtonDown += (sender, e) =>
            {
                this.DragMove();
            };

            this.Close.Click += (sender, e) => { this.Close(); };
            this.About.Click += (sender, e) => { About ab = new About(); ab.ShowDialog(); };

            this.DataContext = this;
        }

    }
    
}
