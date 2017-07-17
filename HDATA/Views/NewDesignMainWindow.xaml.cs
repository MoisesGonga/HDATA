using HDATA.Controls;
using MahApps.Metro.Controls;
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
    /// Interaction logic for NewDesignMainWindow.xaml
    /// </summary>
    public partial class NewDesignMainWindow : MetroWindow
    {
        public NewDesignMainWindow()
        {
            InitializeComponent();
            
        }

        private void btnAddTab_Click(object sender, RoutedEventArgs e)
        {
            ClosableTab tabclose = new ClosableTab();
            //tabclose.Header = "Teste "+DateTime.Today.Second;
            //tabclose.Title = "Paciente";
            tabclose.Content = new MainPaciente_UserControl();
            this.tabControl.Items.Add(tabclose);
            tabclose.Focus();
        }
    }
}
