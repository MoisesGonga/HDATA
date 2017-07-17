using CamadaNegocio;
using CamadaObjectoTransferecia;
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
        
        Usuario user;
        public Definicoes_Window()
        {
            InitializeComponent();
            
            
        }

        public Definicoes_Window(Usuario us)
        {
            this.user = us;
            InitializeComponent();
            if (user.Perfil_Usuario=="Administrador")
            {
                NavigationService.GridNavigationUsercontrol(grid_principal, new Definicoes_UserControl(user));
            }
            else
            {
                //Height = "360.448" Width = "536.194"
                //this.Height = 500;
                //this.Width =564;
                NavigationService.GridNavigationUsercontrol(grid_principal, new Definicoes_UserControl(user));
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
