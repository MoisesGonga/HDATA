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
    /// Interaction logic for TextBoxLogin.xaml
    /// </summary>
    public partial class TextBoxLogin : UserControl
    {
        public TextBoxLogin()
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


        public string Text
        {
            get
            {
                return this.Txt_main.Text;
            }
            set
            {
                Txt_main.Text = value;
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

        private void Txt_main_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void txt_watermarked_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_watermarked.Visibility = Visibility.Collapsed;
            Txt_main.Visibility = Visibility;
            Txt_main.Focus();
        }

        private void Txt_main_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_main.Text))
            {
                Txt_main.Visibility = Visibility.Collapsed;
                txt_watermarked.Visibility = Visibility.Visible;

            }
        }
    }
}
