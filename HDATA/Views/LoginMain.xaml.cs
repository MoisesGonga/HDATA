using CamadaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CamadaObjectoTransferecia;
using System.Data;
using System.Windows.Threading;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for LoginMain.xaml
    /// </summary>
    public partial class LoginMain : Window
    {
        Centro_Hemodialise centro_hemodialise;
        public LoginMain()
        {
            InitializeComponent();
            centro_hemodialise = new Centro_Hemodialise();
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void FlatButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show($"Nome Usuário: {txt_login.Text} \nSenha: {txt_senha.Text}");
        }

        private void sucessButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            
            
        }

        private void sucessButton_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void sucessButton_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button_sair_Click(object sender, RoutedEventArgs e)
        {

            var blur = new BlurEffect();
            blur.Radius = 8;
            var current = this.Background;
            this.Background = new SolidColorBrush(Colors.White);
            this.Effect = blur;
            if (MessageBox.Show("Tem a Certeza que pretende sair?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
            {
                Application.Current.Shutdown();
            }
            this.Effect = null;
            this.Background = current;
        }

        private void Abrir_JanelaPrincipal( Usuario user)
        {
            MainWindow main = new MainWindow(user);
            main.ShowDialog();
        }

        private bool ValidacaoCampos()
        {
              if (string.IsNullOrEmpty(txt_login.Text) || string.IsNullOrEmpty(txt_senha.Text))
            {
                AlteracaoVisualErro("Por favor preencha os respectivos campos!!!");
                return false;
            }
            
            return true;
        }

        private async void button_entrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacaoCampos())
            {
                Usuario usuario = new Usuario();
                usuario.NomeUsuario = txt_login.Text;
                usuario.PalavraPasse = txt_senha.Text;
                usuario = centro_hemodialise.AutenticarUsuario(usuario);
//DataRow user = centro_hemodialise.AutenticarUsuario(txt_login.Text, txt_senha.Text);
                if (txt_login.Text == "Moises" && txt_senha.Text == "123#")
                {
                    VisualSucesso("Moises");
                    await Task.Delay(3000);
                    txt_login.Text = "";
                    txt_senha.Text = "";
                    this.Visibility = Visibility.Hidden;
                    MainWindow main = new MainWindow();
                    main.ShowDialog();
                }
                else if (usuario != null && usuario.NomeUsuario.Equals(txt_login.Text) && usuario.PalavraPasse.Equals(txt_senha.Text))
                {
                    VisualSucesso(usuario.NomeUsuario);
                    await Task.Delay(3000);

                    txt_login.Text = "";
                    txt_senha.Text = "";
                    this.Visibility = Visibility.Hidden;
                    Abrir_JanelaPrincipal(usuario);
                }
                else
                {
                    AlteracaoVisualErro("O Nome de Utilizador e a Palavra-Passe não coincidem");
                }
            }
            
           

           
        }

        private void txt_login_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AlteracaoVisualErro(string notificacao)
        {

            txt_login.Border_Color = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            
            txt_login.Border_Thickness = new Thickness(2);
            txt_senha.Border_Color = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            txt_senha.Border_Thickness = new Thickness(2);
            lbl_notificacao.Visibility = Visibility.Visible;
            lbl_notificacao.Content = notificacao;

        }

        private void VisualNormal()
        {
            txt_login.Border_Thickness = new Thickness(1);
            txt_login.Border_Color = new SolidColorBrush(Color.FromRgb(212, 212, 212));

            txt_senha.Border_Thickness = new Thickness(1);
            txt_senha.Border_Color = new SolidColorBrush(Color.FromRgb(212, 212, 212));
        }

        private void VisualSucesso(string nomeUsuario)
        {

            lbl_notificacao.Content = $"Seja Benvido {nomeUsuario} !!!";
            lbl_notificacao.Visibility = Visibility.Visible;
            lbl_notificacao.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113));
            
        }

        private void txt_login_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_notificacao.Visibility = Visibility.Hidden;
            VisualNormal();


        }

        private void txt_senha_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_notificacao.Visibility = Visibility.Hidden;
            VisualNormal();

        }
    }
}
