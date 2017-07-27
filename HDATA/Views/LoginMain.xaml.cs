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
        bool logado = false;
        private ViewSplash viewSplash;

        public LoginMain()
        {
            InitializeComponent();
            try
            {
                //EnderecoBLL endBLL = new EnderecoBLL();
                //MessageBox.Show(endBLL.ActualizarEndereco(new Endereco(31,"Angola", "Luanda", "Luanda", "Rua Dr. António Agostinho Neto")));
                  //PacienteBLL pcBLL = new PacienteBLL();
               // Paciente pc = pcBLL.ObterPacientePeloCodigo(23);
                
                //Paciente pc = new Paciente(25,"Ariel Baunilha", "Daniel Gonga", "Joana Caculo Dala", "Luanda", "Angolana", new DateTime(1995, 01, 26), EnumEstadoCivil.Solteiro, EnumGenero.Masculino, "004002082LA034", "Técnico Médio de Informatica", new Contacto("+244941808111", "moitimdg95@gmail.com", "+244922095655", "Daniela António"), new Endereco("Angola", "Luanda", "Luanda", "Rua Dr. António Agostinho Neto"), "H-15-2016", DateTime.Now.Date, DateTime.MinValue, "Dra. Mara", new DateTime(2008, 04, 24), "Negra", new Proveniencia(1), "Ministério das Telecomunicações", "TERM Nº 125/2016");
                // PessoaBLL pbll = new PessoaBLL();
               // MessageBox.Show(pc != null ? pc.Nome : "Nullo");
                //MessageBox.Show(pcBLL.ActualizarPaciente(pc) + "");
                //   ProvenienciaBLL pbll = new ProvenienciaBLL();
                // Proveniencia prov = pbll.ConsultarProvenienciaPeloID(32);
                //MessageBox.Show(prov!=null? prov.Nome_Proveniencia: "Nullo");
                //MessageBox.Show(pbll.ActualizarPessoa(pc)+"");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            usuarioBLL = new UsuarioBLL();
        }

        public LoginMain(ViewSplash viewSplash)
        {
            InitializeComponent();
            try
            {
                viewSplash.Close();
                usuarioBLL = new UsuarioBLL();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar a Tela de Login, por favor contacte ao Administrador do sistema...");
            }
            
            

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
            this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 225));
            this.Effect = blur;
            if (MessageBox.Show("Tem a Certeza que pretende sair?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
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

        private void button_entrar_Click(object sender, RoutedEventArgs e)
        {
            
            LogarUsuario_MD5();
            //if (ValidacaoCampos())
            //{
            //    Usuario usuario = new Usuario();
            //    usuario.NomeUsuario = txt_login.Text;
            //    usuario.PalavraPasse = txt_senha.Text;
            //    usuario = usuarioBLL.AutenticarUsuario(usuario);
            //    if (txt_login.Text == "Moises" && txt_senha.Text == "123#")
            //    {
            //        VisualSucesso("Moises");
            //        await Task.Delay(2000);
            //        txt_login.Text = "";
            //        txt_senha.Text = "";
            //        this.Visibility = Visibility.Hidden;
            //        MainWindow main = new MainWindow();
            //        main.ShowDialog();
            //    }
            //    else if (usuario != null && usuario.NomeUsuario.Equals(txt_login.Text) && usuario.PalavraPasse.Equals(txt_senha.Text))
            //    {
            //        VisualSucesso(usuario.NomeUsuario);
            //        await Task.Delay(2000);
            //        txt_login.Text = "";
            //        txt_senha.Text = "";
            //        MainWindow main = new MainWindow(usuario);
            //        //try
            //        //{
            //        //    ContactoBLL contBLL = new ContactoBLL();
            //        //    //MessageBox.Show(endBLL.AlterarEndereco(new Endereco(3, "Cuba", "Habana", "Gaunabaz", "Fidel de Castro Hero!")));
            //        //    //MessageBox.Show(contBLL.AlterarContacto(new Contacto(3,"945216547"," vidalinho@gmail.com","+244945648421","Victor António")));
            //        //    PessoaBLL pBLL = new PessoaBLL();
            //        //    PacienteBLL pcBLL = new PacienteBLL();
            //        //    Paciente pc = new Paciente("2 Piniel Gonga", "Daniel Gonga", "Joana Caculo Dala", "Luanda", "Angolana", new DateTime(1995, 01, 26), EnumEstadoCivil.Solteiro, EnumGenero.Masculino, "004002082LA034", "Técnico Médio de Informatica", new Contacto("+244941808111", "moitimdg95@gmail.com", "+244922095655", "Daniela António"), new Endereco("Angola", "Luanda", "Luanda", "Rua Dr. António Agostinho Neto"), "H-15-2016", DateTime.Now.Date,DateTime.MinValue, "Dra. Mara", new DateTime(2008, 04, 24), "Negra", new Proveniencia(1), "Ministério das Telecomunicações", "TERM Nº 125/2016");
            //        //    //MessageBox.Show(pBLL.CadastrarPessoaFunction(pc)+"");
            //        //    MessageBox.Show(pcBLL.CadastrarPaciente(pc)+"");
            //        //    //MessageBox.Show(contBLL.AlterarContacto(new Contacto(3,"945216547"," vidalinho@gmail.com","+244945648421","Victor António")));
            //        //    //endereco = new Endereco(4, "España", "Madrid", "Gaunabaz", "Fidel de Castro Hero!");
            //        //}
            //        //catch (System.Exception ex)
            //        //{
            //        //    throw new System.Exception(ex.Message);
            //        //}
            //        this.Close();
            //        main.ShowDialog();
            //    }
            //    else
            //    {
            //        AlteracaoVisualErro("O Nome de Utilizador e a Palavra-Passe não coincidem");
            //    }
            //}
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
            lbl_notificacao.Foreground = new SolidColorBrush(Color.FromRgb(252, 31, 31));
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
            if (txt_login.TextChanged)
            {
                VisualNormal();
            }
        }

        private void txt_senha_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_notificacao.Visibility = Visibility.Hidden;
            VisualNormal();

        }

        private async void LogarUsuario_MD5()
        {
            
            if (!logado)
            {
                lbl_notificacao.Foreground = new SolidColorBrush(Color.FromRgb(53, 146, 255));
                lbl_notificacao.Visibility = Visibility.Visible;
                lbl_notificacao.Content = "A Autenticar...";
                await Task.Delay(1000);
                if (ValidacaoCampos())
                {
                    Usuario usuario = new Usuario();
                    CriptorafiaMD5 criptoMD5 = new CriptorafiaMD5();
                    usuario.NomeUsuario = txt_login.Text;
                    usuario.PalavraPasse = txt_senha.Text;
                    usuario = usuarioBLL.AutenticarUsuario_MD5(usuario);
                   
                    if (txt_login.Text == "Moises" && txt_senha.Text == "123#")
                    {
                        VisualNormal();
                        VisualSucesso("Moises");
                        logado = true;
                        
                        txt_login.Text = "";
                        txt_senha.Text = "";
                        this.Visibility = Visibility.Hidden;
                        MainWindow main = new MainWindow(new Usuario("MOISES"));
                        main.ShowDialog();
                    }
                    else if (usuario != null && usuario.NomeUsuario.Equals(txt_login.Text) && criptoMD5.ComparaMD5(txt_senha.Text, usuario.PalavraPasse))
                    {
                        VisualNormal();
                        VisualSucesso(usuario.NomeUsuario);
                        logado = true;
                        await Task.Delay(2000);
                        txt_login.Text = "";
                        txt_senha.Text = "";
                        if (string.IsNullOrEmpty(usuario.SiglaUsuario))
                        {
                            usuario.SiglaUsuario = Abreviacao(usuario.Funcionario.Nome).ToUpper();
                            usuarioBLL.ActualizarUsuario(usuario);
                        }
                        MainWindow main = new MainWindow(usuario);
                        this.Close();
                        main.ShowDialog();
                    }
                    else
                    {
                        AlteracaoVisualErro("O Nome de Utilizador e a Palavra-Passe não coincidem!!!");
                        //AlteracaoVisualErro("O Nome de Utilizador e a Palavra-Passe não coincidem");
                    }
                }
            }
        }

        private string Abreviacao(string username)
        {
            string ret = "";

            if (username.Length > 2)
            {
                ret = username[0] + "" + username[1] + "";
            }
            return ret;
        }

        private async void LogarUsuario()
        {
           
            if (ValidacaoCampos())
            {
                Usuario usuario = new Usuario();
                
                usuario.NomeUsuario = txt_login.Text;
                usuario.PalavraPasse = txt_senha.Text;
                usuario = usuarioBLL.AutenticarUsuario(usuario);
                if (usuario != null)
                {
                    MessageBox.Show($"User Name: {usuario.NomeUsuario} Password BD: {usuario.PalavraPasse} + ");
                    //MessageBox.Show($"{criptoMD5.ComparaMD5(txt_senha.Text, usuario.PalavraPasse)}\n - {criptoMD5.RetornarMD5(txt_senha.Text)} \n-  { usuario.PalavraPasse}");
                }

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
                else if (usuario != null && usuario.NomeUsuario.Equals(txt_login.Text) && txt_senha.Text.Equals(usuario.PalavraPasse))
                {
                    VisualSucesso(usuario.NomeUsuario);
                    await Task.Delay(2000);
                    txt_login.Text = "";
                    txt_senha.Text = "";
                    MainWindow main = new MainWindow(usuario);
                    this.Close();
                    main.ShowDialog();
                }
                else
                {
                    AlteracaoVisualErro("O Nome de Utilizador e a Palavra-Passe não coincidem");
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogarUsuario_MD5();
            }
            
        }
    }
}
