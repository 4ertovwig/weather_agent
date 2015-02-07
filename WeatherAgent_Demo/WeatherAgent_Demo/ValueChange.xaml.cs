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

namespace WeatherAgent_Demo
{
    /// <summary>
    /// Логика взаимодействия для ValueChange.xaml
    /// </summary>
    public partial class ValueChange : Window
    {
        #region Binding members
        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string datevalue;
        public string DateValue
        {
            get { return datevalue; }
            set { datevalue = value; }
        }

        private string morningvalue;
        public string MorningValue
        {
            get { return morningvalue; }
            set { morningvalue = value; }
        }
        private string dayvalue;
        public string DayValue
        {
            get { return dayvalue; }
            set { dayvalue = value; }
        }
        private string eveningvalue;
        public string EveningValue
        {
            get { return eveningvalue; }
            set { eveningvalue = value; }
        }
        private string nightvalue;
        public string NightValue
        {
            get { return nightvalue; }
            set { nightvalue = value; }
        }
        #endregion

        public ValueChange(Int32 NumberOfChild, params string[] parameters)
        {
            InitializeComponent();
            datevalue = parameters[0];
            morningvalue = parameters[1];
            dayvalue = parameters[2];
            eveningvalue = parameters[3];
            nightvalue = parameters[4];
            number = NumberOfChild.ToString();
            this.Close.Click += (sender, RoutedEventArgs) => { this.Close(); };

            this.Changed.Click += (sender, RoutedEventArgs) => 
            {
                InfoAboutWeather Owner = this.Owner as InfoAboutWeather;
                var AllTextBlocks = TreeVisualHelp.FindVisualChildren<TextBlock>(Owner.InfoAbout.InfoList).ToList();
                Int32 NumberOfTextBlockDate = 0;
                foreach (TextBlock txt in AllTextBlocks)
                {
                    NumberOfTextBlockDate++;
                    if (txt.Text == DateTime.Now.AddDays(Int32.Parse(Number)).ToString("dd MM yyyy"))
                    {
                        AllTextBlocks[NumberOfTextBlockDate + 4].Text = MorningValue;
                        AllTextBlocks[NumberOfTextBlockDate + 5].Text = DayValue;
                        AllTextBlocks[NumberOfTextBlockDate + 6].Text = EveningValue;
                        AllTextBlocks[NumberOfTextBlockDate + 7].Text = NightValue;
                        break;
                    }
                }
                this.Close();
            };

            this.Change.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                this.DragMove();
            };

            this.DataContext = this;
        }
    }
}
