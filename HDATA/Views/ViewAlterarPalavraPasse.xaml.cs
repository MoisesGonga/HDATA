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
using CamadaObjectoTransferecia;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for ViewAlterarPalavraPasse.xaml
    /// </summary>
    public partial class ViewAlterarPalavraPasse : Window
    {
        private Usuario user;

        public ViewAlterarPalavraPasse()
        {
            InitializeComponent();
        }

        public ViewAlterarPalavraPasse(Usuario user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
