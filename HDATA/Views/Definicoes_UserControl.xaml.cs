using CamadaNegocio;
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

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for Definicoes_UserControl.xaml
    /// </summary>
    public partial class Definicoes_UserControl : UserControl
    {
        Centro_Hemodialise Centro_Hemodialise_;

        public Definicoes_UserControl()
        {
            InitializeComponent();
        }

        public Definicoes_UserControl(Centro_Hemodialise Centro_Hemodialise_)
        {
            InitializeComponent();
            this.Centro_Hemodialise_ = Centro_Hemodialise_;
        }

        private void DefinicaoContaUtilizador_UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
