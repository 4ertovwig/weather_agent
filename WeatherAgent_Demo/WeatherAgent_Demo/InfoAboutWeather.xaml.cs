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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WeatherAgent_Demo
{
    /// <summary>
    /// Логика взаимодействия для InfoAboutWeather.xaml
    /// </summary>
    public partial class InfoAboutWeather : Window
    {

        #region Binding members
        private ObservableCollection<Info> Info;
        public ObservableCollection<Info> WeathInfo
        {
            get 
            {
                Int32 NumberOfDay = 0;
                Info = new ObservableCollection<Info>();
                foreach (ValueOfResponseInfo af in ValuesOfResponse.WeatherWeek)
                {
                    NumberOfDay++;
                    Info.Add(new Info() { Day = DateTime.Now.AddDays(NumberOfDay).ToString("dd MM yyyy"), Weath = new ValueOfResponseInfo{ MorningWeather = af.MorningWeather, DayWeather = af.DayWeather, EveningWeather = af.EveningWeather, NightWeather = af.NightWeather }, Path = RequestYandex.PathIcon() });
                }
                return Info; 
            }
        }


    /*    private BitmapImage pathImage;
        public BitmapImage PathImage
        {
            get { return pathImage; }
        }
        public IEnumerable<string> MorningWeather
        { 
            get 
            { 
                for(Int32 i=0; i<WeathInfo.Count; i++ )
                yield return WeathInfo[i].MorningWeather; 
            } 
        }

        public IEnumerable<string> DayWeather
        {
            get
            {
                for (Int32 i = 0; i < WeathInfo.Count; i++)
                    yield return WeathInfo[i].DayWeather;
            }
        }

        public IEnumerable<string> EveningWeather
        {
            get
            {
                for (Int32 i = 0; i < WeathInfo.Count; i++)
                    yield return WeathInfo[i].EveningWeather;
            }
        }

        public IEnumerable<string> NightWeather
        {
            get
            {
                for (Int32 i = 0; i < WeathInfo.Count; i++)
                    yield return WeathInfo[i].NightWeather;
            }
        } */
        #endregion
       

        public InfoAboutWeather()
        {
            InitializeComponent();
            InfoList.ItemsSource = WeathInfo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var TextBlocks = TreeVisualHelp.FindVisualChildren<TextBlock>(sender as Button).ToList();
            List<string> DateList = new List<string>();

            var AllTextBlocks = TreeVisualHelp.FindVisualChildren<TextBlock>(InfoList).ToList();
            foreach (TextBlock txt in AllTextBlocks)
            {
                if(txt.Name == "Date")
                DateList.Add(txt.Text);
            }
            string Date = TextBlocks[0].Text.ToString();
            string Morning = TextBlocks[5].Text.ToString();
            string Day = TextBlocks[6].Text.ToString();
            string Evening = TextBlocks[7].Text.ToString();
            string Night = TextBlocks[8].Text.ToString();

            //индекс визуального элемента, на котором произошел клик
            Int32 NumberOfChild = 0;
            foreach (string date in DateList)
            {
                NumberOfChild++;
                if (date == Date)
                    break;
            }

            ValueChange newWindow = new ValueChange(NumberOfChild, Date, Morning, Day, Evening, Night);
            newWindow.Owner = this;
            newWindow.ShowDialog();
        }

    }
}
