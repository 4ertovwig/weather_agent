using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows;

namespace WeatherAgent_Demo
{
    class RequestYandex
    {
        public static BitmapImage PathIcon()
        {
            Int32 valueTemp = Convert.ToInt32(ValuesOfResponse.Temperature, 10);
            BitmapImage path = null;
            if( valueTemp <= -15) path = new BitmapImage(new Uri(@"resources/Cold.png", UriKind.Relative));
            if((valueTemp >-15) && (valueTemp<=0)) path = new BitmapImage(new Uri(@"resources/ColdHot.png", UriKind.Relative));
            if((valueTemp >0) && (valueTemp<=15)) path = new BitmapImage(new Uri(@"resources/ColdHot.png", UriKind.Relative));
            if(valueTemp >15) path = new BitmapImage(new Uri(@"resources/ColdHot.png", UriKind.Relative));
            return path;
        }

        public static void GET(string Data)
        {
            try
            {
                WebRequest req = WebRequest.Create(@"http://export.yandex.ru/weather-ng/forecasts/" + Data);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
                byte[] array = ASCIIEncoding.UTF8.GetBytes(Out);
                File.WriteAllText("yaresponce.xml", "");
                FileStream fileStream = new FileStream("yaresponce.xml", FileMode.OpenOrCreate);
                fileStream.Write(array, 0, array.Length);
                fileStream.Close();
            }
            catch (WebException)
            {
                MessageBox.Show("Проверьте подключение к интернету");
            }
        }

        public static void ParseXml()
        {
            XmlDocument xdocument = new XmlDocument();
            xdocument.Load("yaresponce.xml");
            XmlNodeList ndList = xdocument.GetElementsByTagName("fact");
            foreach (XmlNode node in ndList)
            {
                ValuesOfResponse.Temperature = node["temperature"].InnerText;
                ValuesOfResponse.Type = node["weather_type"].InnerText;
                ValuesOfResponse.windSpeed = node["wind_speed"].InnerText;
                ValuesOfResponse.Pressure = node["pressure"].InnerText;
            }
            
            
            var nsmgr = new XmlNamespaceManager(xdocument.NameTable);
            nsmgr.AddNamespace("f", @"http://weather.yandex.ru/forecast");
            for (Int32 i = 2; i < 8; i++)
            {
                 ValuesOfResponse.WeatherWeek.Add(new ValueOfResponseInfo
                 {
                      MorningWeather = RandomParse(xdocument, nsmgr, i, 1),
                      EveningWeather = RandomParse(xdocument, nsmgr, i, 3),
                      DayWeather = RandomParse(xdocument, nsmgr, i, 2),
                      NightWeather = RandomParse(xdocument, nsmgr, i, 4)
                 });
            }
            //раскомментить чтобы не видеть адовый xml от яндекса
        //    File.Delete("yaresponce.xml");
        }

        //понадобилось вследствие того, что <temperature> или <temperature_from> возвращается рандомно, с этим дольше всего возился
        public static string RandomParse(XmlDocument xdocument, XmlNamespaceManager nsmgr, Int32 i, Int32 j)
        {
            nsmgr = new XmlNamespaceManager(xdocument.NameTable);
            nsmgr.AddNamespace("f", @"http://weather.yandex.ru/forecast");
            if (xdocument.SelectSingleNode("/f:forecast/f:day[" + i.ToString() + "]/f:day_part[" + j.ToString() + "]/f:temperature_from", nsmgr) != null)
                return xdocument.SelectSingleNode("/f:forecast/f:day[" + i.ToString() + "]/f:day_part[" + j.ToString() + "]/f:temperature_from", nsmgr).InnerText;
            else
                return xdocument.SelectSingleNode("/f:forecast/f:day[" + i.ToString() + "]/f:day_part[" + j.ToString() + "]/f:temperature", nsmgr).InnerText;
            
        }
    }
}
