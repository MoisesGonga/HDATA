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
using System.Windows.Shapes;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for Definicoes_Window.xaml
    /// </summary>
    public partial class Definicoes_Window : Window
    {
        Centro_Hemodialise centro_hemodialise;

        public Definicoes_Window()
        {
            InitializeComponent();
        }

        public Definicoes_Window(string tipo_funcionario)
        {
            InitializeComponent();
            if (tipo_funcionario=="Administradorx")
            {
                NavigationService.GridNavigationUsercontrol(grid_principal, new Definicoes_UserControl(centro_hemodialise));
            }
            else
            {
                //Height = "360.448" Width = "536.194"
                this.Height = 500;
                this.Width =564;
                NavigationService.GridNavigationUsercontrol(grid_principal, new DefinicaoContaUtilizador_UserControl(centro_hemodialise));
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {
            ///this.DragMove();
        }
    }
}
