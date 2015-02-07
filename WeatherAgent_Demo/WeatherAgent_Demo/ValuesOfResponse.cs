using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace WeatherAgent_Demo
{
    public class ValuesOfResponse
    {
        public static string Temperature { get; set; }
        public static string Type { get; set; }
        public static string Pressure { get; set; }
        public static string windSpeed { get; set; }

        public static ObservableCollection<ValueOfResponseInfo> WeatherWeek = new ObservableCollection<ValueOfResponseInfo>();
    }

    public class ValueOfResponseInfo
    {
        public string MorningWeather { get; set; }
        public string EveningWeather { get; set; }
        public string DayWeather { get; set; }
        public string NightWeather { get; set; }
    }

    public class Info
    {
        private ImageSource path;
        public ImageSource Path
        {
            get { return path; }
            set { path = value; }
        }

        public ValueOfResponseInfo Weath
        { get; set; }

        private string day;
        public string Day
        {
            get { return day; }
            set { day = value; }
        }
    }

}
