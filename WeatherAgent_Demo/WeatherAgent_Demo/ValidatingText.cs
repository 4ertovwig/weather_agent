using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace WeatherAgent_Demo
{
    class ValidatingText : ValidationRule
    {
        private int _min;
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }
        private int _max;
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int age = 0;
            try
            {
                if (((string)value).Length > 0)
                    age = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Введите число а не строку " + e.Message);
            }

            if ((age < Min) || (age > Max))
            {
                return new ValidationResult(false,
                "Введите числа в диапазоне от: " + Min + " - " + Max + ".");
            }
            else if (((string)value).Contains("00")==true)
            {
                return new ValidationResult(false,
                "Не вводите множество нулей");
            }

            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
