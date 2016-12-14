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

namespace HDATA.Controls
{
    /// <summary>
    /// Interaction logic for PasswordLogin.xaml
    /// </summary>
    public partial class PasswordLogin : UserControl
    {
        public PasswordLogin()
        {
            InitializeComponent();
        }

        private string _water_markerk;
        public string Watermark
        {
            get
            {
                return this._water_markerk;
            }
            set
            {
                _water_markerk = value;
                txt_watermarked.Text = _water_markerk;
            }
        }

        private string background_Color;
        public string Background_Color
        {
            get
            {
                return this.background_Color;
            }
            set
            {
                background_Color = value;

            }
        }

        public string Text
        {
            get
            {
                return this.Txt_main.Password;
            }
            set
            {
                Txt_main.Password = value;
            }
        }

        public Brush Border_Color
        {
            get
            {
                return Main_Control.BorderBrush;
            }
            set
            {
                Main_Control.BorderBrush = value;

            }
        }

        public Thickness Border_Thickness
        {
            get
            {
                return Main_Control.BorderThickness;
            }
            set
            {
                Main_Control.BorderThickness = value;

            }
        }


        private void txt_watermarked_GotFocus(object sender, RoutedEventArgs e)
        {

            txt_watermarked.Visibility = Visibility.Collapsed;
            Txt_main.Visibility = Visibility;
            Txt_main.Focus();


        }

        private void Txt_main_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_main.Password))
            {

                Txt_main.Visibility = Visibility.Collapsed;
                txt_watermarked.Visibility = Visibility.Visible;

            }
        }

        private void Txt_main_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_watermarked_KeyDown(object sender, KeyEventArgs e)
        {

            txt_watermarked.Visibility = Visibility.Collapsed;
            Txt_main.Visibility = Visibility;
            Txt_main.Focus();


        }

    }
}
