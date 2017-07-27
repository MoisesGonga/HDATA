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
using System.Windows.Threading;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for ViewSplash.xaml
    /// </summary>
    public partial class ViewSplash : Window
    {
        DispatcherTimer timer;
        LoginMain login;


        public ViewSplash()
        {
            
            InitializeComponent();
            lbl_txt_carregando.Content = "Carregando componentes  do sistema";
            
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,0,200);
            timer.Tick += new  EventHandler((s,e)=> {

            if (mainProgressBar.Value <= 100)
                {
                    if (mainProgressBar.Value >= 70 && mainProgressBar.Value < 90)
                    {
                        timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                        lbl_txt_carregando.Content = "Finalizando o carregamento do sistema";

                    }
                    else if (mainProgressBar.Value >= 90 && mainProgressBar.Value <= 100)
                    {
                        lbl_txt_carregando.Content = "Sistema carregado com sucesso";
                        timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                    }
                    else if (mainProgressBar.Value >= 0 && mainProgressBar.Value <= 10)
                        lbl_txt_carregando.Content = "Seja Bem-vindo ao HDATA";
                    else if (mainProgressBar.Value > 10 && mainProgressBar.Value < 30)
                    {
                        timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                        lbl_txt_carregando.Content = "O sistema está carregando os componentes";
                    }
                    else if (mainProgressBar.Value >= 30 && mainProgressBar.Value <= 70)
                    {
                        timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
                        lbl_txt_carregando.Content = "Verificando os componentes";
                    }

                    if (mainProgressBar.Value%4 ==0)
                        lbl_carregando.Content = "";
                    else
                        lbl_carregando.Content += ".";
                    mainProgressBar.Value += 1;
                    this.lbl_percentagem.Content = mainProgressBar.Value.ToString()+"%";
                }
                if(mainProgressBar.Value >= 100)
                {
                    this.timer.IsEnabled = false;
                    timer.Stop();
                    login = new Views.LoginMain(this);
                    this.Visibility = Visibility.Hidden;
                    login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    login.ShowDialog();
                }
            });
            timer.Start();
        }

        private void grid_main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
