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

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

namespace WeatherControl
{
    class Weather:DependencyObject
    {
        public static readonly DependencyProperty TempProperty;
        private string wind_direction;
        private int wind_speed;
        private enum Precipitation
        {
            sunny,
            cloudy,
            rain,
            snow
        }

        public int Temp
        {
            get => (int) GetValue(TempProperty);
            set => SetValue(TempProperty,value);
        }
        public string Wind_direction
        {
            get { return wind_direction; }
            set { wind_direction = value; }
        }
        public int Wind_speed
        {
            get { return wind_speed; }
            set { wind_speed = value; }
        }
        public Weather(int temp, string wind_direction, int wind_speed, int precipitation)
        {
            this.Temp = temp;
            this.Wind_direction = wind_direction;
            this.Wind_speed = wind_speed;
        }
        static Weather()
        {
            TempProperty = DependencyProperty.Register(
                nameof(Temp),
                typeof(int),
                typeof(Weather),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemp)),
            new ValidateValueCallback(ValidateTemp));
        }

        private static bool ValidateTemp(object value)
        {
            int v = (int) value;
            if (v >= 0 && v <= 50)
                return true;
            else
                return false;
        }

        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50)
                return v;
            else
                return 0;
        }
    }
}
