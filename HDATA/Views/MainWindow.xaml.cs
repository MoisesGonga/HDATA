using CamadaObjectoTransferecia;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfPageTransitions;
using HDATA.UserAccess;
using CamadaNegocio;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPaciente_UserControl listapacientes;
        MainTabelas_UserControl tabelas;
        Home home;
        Usuario user;
        usc_sessao_hemodialise sessaoHemodialise;

        public MainWindow()
        {

            user = new Usuario();
         
            user.Perfil_Usuario = UserType.Admin;
            user.NomeUsuario = "Moisés Gonga";
            AppCommon.LogedUserType = user.Perfil_Usuario;
            user.Funcionario = new Funcionario();
            user.Funcionario.Nome = "Root ";
        InitializeComponent();
        listapacientes  = new MainPaciente_UserControl();
        home = new Home();
        tabelas = new MainTabelas_UserControl();
        pagetransitioncontrol.TransitionType = PageTransitionType.Fade;
            pagetransitioncontrol.ShowPage(home);
            sessaoHemodialise = new Views.usc_sessao_hemodialise();
           // funcionario = new Funcionario();
           //funcionario.Categoria = "Administrador";
           //BeginStoryboard()
        }
    public MainWindow( Usuario user)
        {
            AppCommon.LogedUserType = user.Perfil_Usuario;
            InitializeComponent();
            
            listapacientes = new MainPaciente_UserControl();
            home = new Home();
            tabelas = new MainTabelas_UserControl();
            pagetransitioncontrol.TransitionType = PageTransitionType.Fade;
            pagetransitioncontrol.ShowPage(home);
            this.user = user;
            sessaoHemodialise = new Views.usc_sessao_hemodialise();
            lbl_abreviacao_usuario.Content = user.SiglaUsuario;
                //Abreviacao(user.NomeUsuario).ToUpper();
        }

        private string Abreviacao(string username)
        {
            string ret = "";
            
            if (username.Length > 2)
            {
                ret = username[0]+"" + username[1]+"";
            }
            return ret;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pagetransitioncontrol.CurrentPage != home)
            {
                pagetransitioncontrol.TransitionType = PageTransitionType.Fade;
                pagetransitioncontrol.ShowPage(home);
            }
            /*
             * BLUR EFFECT
            var blur = new BlurEffect();
            blur.Radius = 5;
            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.White);
            this.Effect = blur;
            MessageBox.Show("hello");
            this.Effect = null;
            this.Background = current;
            */
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pagetransitioncontrol.CurrentPage != listapacientes)
            {
                listapacientes = new MainPaciente_UserControl();
                pagetransitioncontrol.TransitionType = PageTransitionType.Slide;
                pagetransitioncontrol.ShowPage(listapacientes);
                pagetransitioncontrol.TransitionType = PageTransitionType.Fade;

            }
        }

        private void btn_ajuda_Click(object sender, RoutedEventArgs e)
        {
           var blur = new BlurEffect();
           blur.Radius = 5;
           var current = this.Background;
           this.Background = new SolidColorBrush(Colors.White);
           this.Effect = blur;
           MessageBox.Show("Esta função está em fase de contrução!!!");
           this.Effect = null;
           this.Background = current;
        }

        private void btn_Tabelas_Click(object sender, RoutedEventArgs e)
        {
            if (pagetransitioncontrol.CurrentPage != tabelas)
            {
                pagetransitioncontrol.TransitionType = PageTransitionType.Fade;
                pagetransitioncontrol.ShowPage(tabelas);

            }
        }

        private void btn_sair_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void btn_sair_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // BLUR EFFECT
            //var blur = new BlurEffect();
            //blur.Radius = 8;         
            //var current = this.Background;

            //this.Background = new SolidColorBrush(Color.FromRgb(52,152,219));
            //this.Effect = blur;

            //
            
            this.Effect = new BlurEffect
            {
                KernelType = KernelType.Gaussian,
                Radius = 20,
                RenderingBias = RenderingBias.Quality
            };
            if (MessageBox.Show("Tem a Certeza que pretende sair?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
            {
                try
                {
                    UsuarioBLL usuariobll = new UsuarioBLL();
                        user.DataUltimoAcesso = DateTime.Now;
                        usuariobll.ActualizarUsuario(user);
                    
                }
                catch (Exception)
                {
                    throw new Exception("Ocorreu um problema ao finalizar a aplicação, Por favor informe ao Administrador do Sistema");
                }
                
                Application.Current.Shutdown();
                
            }
           this.Effect = null;
           //this.Background = current;
        }

        private void btn_sair_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void ImageAwesome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (user.Perfil_Usuario==UserType.Admin)
            {
                //Categoria de Admin
                //BLUR EFFECT
                var blur = new BlurEffect();
                blur.Radius = 7;
                var current = this.Background;
                this.Background = new SolidColorBrush(Colors.LightGray);
                this.Effect = blur;
                Definicoes_Window w_definicoes = new Definicoes_Window(user);
                w_definicoes.ShowDialog();
                this.Effect = null;
                this.Background = current;
            }
            else
            {  
                //BLUR EFFECT
                var blur = new BlurEffect();
                blur.Radius = 7;
                var current = this.Background;
                this.Background = new SolidColorBrush(Colors.LightGray);
                this.Effect = blur;
                Definicoes_Window w_definicoes = new Definicoes_Window(user);
                w_definicoes.ShowDialog();
                this.Effect = null;
                this.Background = current;
            }
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void sessao_hemodialise_Click(object sender, RoutedEventArgs e)
        {
            if (pagetransitioncontrol.CurrentPage != sessaoHemodialise)
            {
                pagetransitioncontrol.TransitionType = PageTransitionType.Fade;
                pagetransitioncontrol.ShowPage(sessaoHemodialise);

            }
        }

        private void relatorios_Click(object sender, RoutedEventArgs e)
        {
            var blur = new BlurEffect();
            blur.Radius = 5;
            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.White);
            this.Effect = blur;
            ViewReportProveniencia ViewReportProveniencia = new Views.ViewReportProveniencia();
            ViewReportProveniencia.ShowInTaskbar = false;
            ViewReportProveniencia.ShowDialog();
            //MessageBox.Show("A função de imprensão de relatórios está em fase de contrução!!!","Função em Contrução",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Effect = null;
            this.Background = current;
        }

        private void border_perfil_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}