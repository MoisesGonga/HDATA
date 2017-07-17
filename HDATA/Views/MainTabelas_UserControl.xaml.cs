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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for MainTabelas_UserControl.xaml
    /// </summary>
    public partial class MainTabelas_UserControl : UserControl
    {
        
        ProvenienciaBLL provenienciaBLL;
        EnumTipoOperacao_Manipulacao tipo_Operacao_Proveniencia;
        Proveniencia selectedProveniencia;
        List<Proveniencia> ListaProveniencia;

        public MainTabelas_UserControl()
        {
            InitializeComponent();
            provenienciaBLL = new ProvenienciaBLL();
            tipo_Operacao_Proveniencia = EnumTipoOperacao_Manipulacao.Cadastrar;
            selectedProveniencia = new Proveniencia();
            CarregarDadosListaProveniencia();
        }

        

        public void CarregarDadosListaProveniencia()
        {
            ListaProveniencia = provenienciaBLL.ListarProveniencia();
            this.listview_proveniencia.ItemsSource = null;
            this.listview_proveniencia.ItemsSource = ListaProveniencia;
        }

        private int BuscarProvenienciaNaLista(string nomeProveniecia)
        {
            return 0;
        }

        private void tab_1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            CarregarDadosListaProveniencia();
        }

        private void listview_proveniencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listview_proveniencia.SelectedItems.Count > 0)
            {
                btn_eliminar.IsEnabled = btn_edit.IsEnabled = true;
                
            }
            else
            {
                btn_eliminar.IsEnabled = btn_edit.IsEnabled = false;
                
            }
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (listview_proveniencia.SelectedItems.Count > 0)
            {
                selectedProveniencia = listview_proveniencia.SelectedItem as Proveniencia;
                txt_nome_proveniencia.Text = selectedProveniencia.Nome_Proveniencia;
                txt_descricao_proveniencia.Text = selectedProveniencia.Descricao;
                lbl_notificacao.Content += tipo_Operacao_Proveniencia.ToString();
                this.tipo_Operacao_Proveniencia = EnumTipoOperacao_Manipulacao.Actualizar;
                
            }
        }

        private void txt_nome_proveniencia_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(this.txt_nome_proveniencia.Text) || !string.IsNullOrEmpty(txt_descricao_proveniencia.Text))
            {
                this.btn_limpar.IsEnabled = true;
                
            }
            else
            {
                this.btn_limpar.IsEnabled = false;
            }

            
            icon_exclamation_nome.Visibility = Visibility.Hidden;
            lbl_notificacao.Visibility = Visibility.Hidden;

        }

        private bool ValidacaoProveniencia()
        {

            if (string.IsNullOrEmpty(this.txt_nome_proveniencia.Text.Trim()) && string.IsNullOrEmpty(txt_descricao_proveniencia.Text.Trim()))
            {
                icon_exclamation_descricao.Visibility = Visibility.Visible;
                icon_exclamation_nome.Visibility = Visibility.Visible;

                lbl_notificacao.Visibility = Visibility.Visible;
                lbl_notificacao.Foreground = new SolidColorBrush(Colors.Red);
                lbl_notificacao.Content = $"Por favor Preencha os respectivos campos!!!";
                return false;
            }
            else if (string.IsNullOrEmpty(this.txt_nome_proveniencia.Text))
            {
                icon_exclamation_nome.Visibility = Visibility.Visible;
                lbl_notificacao.Visibility = Visibility.Visible;
                lbl_notificacao.Foreground = new SolidColorBrush(Colors.Red);
                lbl_notificacao.Content = $"O campo Nome não pode estar vazio!!!";
                return false;
            }
            return true;
        }

        private void btn_limpar_Click(object sender, RoutedEventArgs e)
        {
            btn_limpar.IsEnabled = false;
            ProvenienciaLimparTextBox();
            tipo_Operacao_Proveniencia = EnumTipoOperacao_Manipulacao.Cadastrar;
        }

        private void ProvenienciaLimparTextBox()
        {
            this.txt_nome_proveniencia.Text = "";
            this.txt_descricao_proveniencia.Text = "";
        }

        private void txt_descricao_proveniencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txt_nome_proveniencia.Text.Trim()) || !string.IsNullOrEmpty(txt_descricao_proveniencia.Text.Trim()))
            {
                this.btn_limpar.IsEnabled = true;
            }
            else
            {
                this.btn_limpar.IsEnabled = false;
            }

            icon_exclamation_descricao.Visibility = Visibility.Hidden;
            lbl_notificacao.Visibility = Visibility.Hidden;
        }
       
        private void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            Proveniencia p = null;
            if (ValidacaoProveniencia())
            {


            if (tipo_Operacao_Proveniencia == EnumTipoOperacao_Manipulacao.Cadastrar)
            {
                    p = new Proveniencia(txt_nome_proveniencia.Text.Trim(), txt_descricao_proveniencia.Text.Trim());
                    provenienciaBLL.CadastrarProveniencia(p).ToString();
                    MessageBox.Show("Proveniencia Cadastrada Com sucesso!!!","Cadastrar Sucesso",MessageBoxButton.OK,MessageBoxImage.Information);
                    //NotificacaoLabel(0, selectedProveniencia);
                    CarregarDadosListaProveniencia();
                    ProvenienciaLimparTextBox();
            }
            else
            {
                    p = new Proveniencia(selectedProveniencia.Id_Proveniencia, txt_nome_proveniencia.Text.Trim(), txt_descricao_proveniencia.Text.Trim());
                    MessageBox.Show("Proveniencia Actualizada Com sucesso!!!", "Actualizar Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    provenienciaBLL.ActualizarProveniencia(p);
                    //NotificacaoLabel(1, p);
                    CarregarDadosListaProveniencia();
                    ProvenienciaLimparTextBox();
                    tipo_Operacao_Proveniencia = EnumTipoOperacao_Manipulacao.Cadastrar;
            }
           }
            btn_limpar.IsEnabled = true;
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (listview_proveniencia.SelectedItems.Count > 0)
            {
                Proveniencia p = listview_proveniencia.SelectedItem as Proveniencia;
                var blur = new BlurEffect();
                blur.Radius = 8;
                var current = this.Background;
                this.Background = new SolidColorBrush(Colors.White);
                this.Effect = blur;
                if (MessageBox.Show($"Tem Certeza que pretende eliminar o item {p.Nome_Proveniencia}?", "Sair", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
                {
                    provenienciaBLL.EliminarProveniencia(p);
                }
                this.Effect = null;
                this.Background = current;
                
                NotificacaoLabel(2,p);
                CarregarDadosListaProveniencia();
            }
        }

        private void NotificacaoLabel(int tipo,Proveniencia p)
        {
            if (tipo == 0)
            {
                lbl_notificacao.Visibility = Visibility.Visible;
                lbl_notificacao.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                lbl_notificacao.Content = $"Proveniencia {p.Nome_Proveniencia} Cadastrada Com sucesso!!!";
            }
            else if (tipo==1)
            {
                lbl_notificacao.Visibility = Visibility.Visible;
                lbl_notificacao.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                lbl_notificacao.Content = $"Proveniencia {p.Nome_Proveniencia} Actualizada Com sucesso!!!";
            }
            else if(tipo == 2)
            {
                lbl_notificacao.Visibility = Visibility.Visible;
                lbl_notificacao.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113));
                lbl_notificacao.Content = $"Proveniencia {p.Nome_Proveniencia} Eliminada Com sucesso!!!";
            }
        }

        private void btn_eliminar_LostFocus(object sender, RoutedEventArgs e)
        {
            lbl_notificacao.Visibility = Visibility.Hidden;
            lbl_notificacao.Content = "";
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void txt_nome_proveniencia_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_notificacao.Visibility = Visibility.Hidden;
        }

        private void txt_descricao_proveniencia_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_notificacao.Visibility = Visibility.Hidden;
        }
    }
}
