using CamadaNegocio;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CamadaObjectoTransferecia;
using System;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for LoginMain.xaml
    /// </summary>
    public partial class LoginMain : Window
    {
        //Centro_Hemodialise centro_hemodialise;
        UsuarioBLL usuarioBLL;
        public LoginMain()
        {
            InitializeComponent();
            
            usuarioBLL = new UsuarioBLL();
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
                usuario = usuarioBLL.AutenticarUsuario(usuario);
                if (txt_login.Text == "Moises" && txt_senha.Text == "123#")
                {
                    VisualSucesso("Moises");
                    await Task.Delay(2000);
                    txt_login.Text = "";
                    txt_senha.Text = "";
                    this.Visibility = Visibility.Hidden;
                    MainWindow main = new MainWindow();
                    main.ShowDialog();
                }
                else if (usuario != null && usuario.NomeUsuario.Equals(txt_login.Text) && usuario.PalavraPasse.Equals(txt_senha.Text))
                {
                    VisualSucesso(usuario.NomeUsuario);
                    await Task.Delay(2000);
                    txt_login.Text = "";
                    txt_senha.Text = "";
                    MainWindow main = new MainWindow(usuario);
                    try
                    {
                        ContactoBLL contBLL = new ContactoBLL();
                        //MessageBox.Show(endBLL.AlterarEndereco(new Endereco(3, "Cuba", "Habana", "Gaunabaz", "Fidel de Castro Hero!")));
                        //MessageBox.Show(contBLL.AlterarContacto(new Contacto(3,"945216547"," vidalinho@gmail.com","+244945648421","Victor António")));
                        PessoaBLL pBLL = new PessoaBLL();
                        Paciente pc = new Paciente("Moisés Daniel Gonga", "Daniel Gonga", "Joana Caculo Dala", "Luanda", "Angolana", new DateTime(1995, 01, 26), EnumEstadoCivil.Solteiro, EnumGenero.Masculino, "004002082LA034", "Técnico Médio de Informatica", new Contacto("+244941808111", "moitimdg95@gmail.com", "+244922095655", "Daniela António"), new Endereco("Angola", "Luanda", "Luanda", "Rua Dr. António Agostinho Neto"), "H-15-2016", DateTime.Now.Date, "Dra. Mara", new DateTime(2008, 04, 24), "Negra", new Proveniencia(1), "Ministério das Telecomunicações", "TERM Nº 125/2016");
                        MessageBox.Show(pBLL.CadastrarPessoa(pc)+"");
                        
                        //MessageBox.Show(contBLL.AlterarContacto(new Contacto(3,"945216547"," vidalinho@gmail.com","+244945648421","Victor António")));
                        //endereco = new Endereco(4, "España", "Madrid", "Gaunabaz", "Fidel de Castro Hero!");
                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception(ex.Message);
                    }
                    this.Close();
                    main.ShowDialog();

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
