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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using CamadaAcessoDados;
using CamadaObjectoTransferecia;
using CamadaNegocio;
using System.Windows.Media.Effects;
using System.Management;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for Listar_Pacientes.xaml
    /// </summary>
    public partial class Listar_Pacientes : UserControl
    {
        
        private Paciente p;
        AcessoDadosPostgreSQL AcessoDados;
        public List<String> Nomes_ { get; set; }
        public string selectedNomes_ { get; set; }
        private MainPaciente_UserControl mainPaciente_UserControl;
        //private Centro_Hemodialise centro_Hemodialise;
        PacienteBLL pacienteBLL;
        private usc_cadastro_paciente cad_pac;
        public Listar_Pacientes()
        {
            
            InitializeComponent();
            AcessoDados = new AcessoDadosPostgreSQL();
            CarregarTodosPacientes();
            //centro_Hemodialise = new Centro_Hemodialise();
            pacienteBLL = new PacienteBLL();
        }

        public Listar_Pacientes(MainPaciente_UserControl mainPaciente_usercontrol)
        {
            InitializeComponent();
            AcessoDados = new AcessoDadosPostgreSQL();
            pacienteBLL = new PacienteBLL();
            this.mainPaciente_UserControl = mainPaciente_usercontrol;
            CarregarTodosPacientes();
        }

        public Listar_Pacientes(Paciente p, MainPaciente_UserControl mainPaciente_usercontrol)
        {
            InitializeComponent();
            AcessoDados = new AcessoDadosPostgreSQL();
            this.mainPaciente_UserControl = mainPaciente_usercontrol;
            CarregarTodosPacientes();
            this.p = p;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"{selectedNomes_} segundo {text_buscar.SelectedItem}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CarregarTodosPacientes();

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count > 0)
            {
                btn_editar.IsEnabled = true;
                btn_eliminar.IsEnabled = true;

                //var item = dataGrid1.SelectedItem;
                //  string id = ((DataRowView)item).Row["id"].ToString();
                //MessageBox.Show(id);
                // 2 - SELECIONAR VALOR NA GRID EM WPF
                // MessageBox.Show($"{(dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text} - {(dataGrid1.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text} - {(dataGrid1.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text}");
            }
            else
            {
                btn_editar.IsEnabled = false;
                btn_eliminar.IsEnabled = false;
            }
        }
        public void CarregarTodosPacientes()
        {
            try
            {
                dataGrid1.Items.Clear();
                dataGrid1.ItemsSource = pacienteBLL.BuscarTodosPaciente().AsDataView();
                lb_total_de_Paciente.Content = dataGrid1.Items.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Listar os dados dos Pacientes!!!","Listar Pacientes",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void btn_editar_Click(object sender, RoutedEventArgs e)
        {
            //usc_cadastro_paciente cad_pac = null;
            //Paciente p = new Paciente();
            //p.Nome = "Adilson Silva";
            //p.Data_Entrada = DateTime.Now;
            //p.Genero_ = EnumGenero.Masculino;
            //cad_pac = new usc_cadastro_paciente(p, Tipo_Operacao.Actualizacao, mainPaciente_UserControl, centro_Hemodialise);
            //mainPaciente_UserControl.label_title.Content = "Actualizar Paciente";
            //NavigationService.GridNavigationUsercontrol(grid_sub, cad_pac);

            if (dataGrid1.SelectedItems.Count > 0)
            {
            var item = dataGrid1.SelectedItem;
                //($"{} - {(dataGrid1.SelectedCells[1].Column.GetCellContent(item) as TextBloc
            Paciente p = pacienteBLL.ObterPacientePeloCodigo(Convert.ToInt32((dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text));
            //p.Nome = "Adilson Silva";
            //p.Data_Entrada = DateTime.Now;
            //p.Genero_ = EnumGenero.Masculino;
            //p.Data_nasc = new DateTime(1995, 01, 26);
            cad_pac = new usc_cadastro_paciente(p, EnumTipoOperacao_Manipulacao.Actualizar, mainPaciente_UserControl);
            mainPaciente_UserControl.NovoUserControl(cad_pac);
            mainPaciente_UserControl.label_title.Content = "Prontuário Paciente";
                //mainPaciente_UserControl.label_title.Content = "Prontuário Paciente - Actualizar Paciente";
            }
            //NavigationService.GridNavigationUsercontrol(grid_sub, cad_pac);
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem;
              
            if (dataGrid1.SelectedItems.Count > 0 )
            {
               
                var blur = new BlurEffect();
                blur.Radius = 8;
                var current = this.Background;
                this.Background = new SolidColorBrush(Colors.White);
                this.Effect = blur;
               
                Paciente p = pacienteBLL.ObterPacientePeloCodigo(Convert.ToInt32((dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text));
                if (MessageBox.Show($"Tem a Certeza que pretende eliminar todos os dados referente ao paciente: {p.ToString()}?", "Eliminar Paciente", MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
                {
                    try
                    {
                        if (pacienteBLL.EliminarPaciente(p))
                        {
                            CarregarTodosPacientes();
                            MessageBox.Show($"Paciente {p.ToString()} eliminado com sucesso!!!", "Sucesso Eliminar Paciente", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Não foi possivel eliminar o paciente {p.ToString()} !!!", "Erro Eliminar Paciente", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                this.Effect = null;
                this.Background = current;
            }
            
        }

        private void dataGrid1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count > 0)
            {
                btn_editar.IsEnabled = true;
                btn_eliminar.IsEnabled = true;
            }
            else
            {
                btn_editar.IsEnabled = false;
                btn_eliminar.IsEnabled = false;
            }
        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = pacienteBLL.ConsultarPacientePorNome(text_buscar.Text).AsDataView();
        }

        private void btn_buscar_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private bool VerificarTextoEnumero(string texto)
        {
            foreach (char c in texto) {
                if (Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void BuscarPeloID() {
            if (!string.IsNullOrEmpty(text_buscar.Text))
            {


                if (VerificarTextoEnumero(text_buscar.Text))
                {
                    dataGrid1.ItemsSource = pacienteBLL.ConsultarPacientePorID(text_buscar.Text).AsDataView();
                }
            }
        }

        private void text_buscar_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void BuscarPeloNome()
        {
            dataGrid1.ItemsSource = pacienteBLL.ConsultarPacientePorNome(text_buscar.Text).AsDataView();
        }

        private void text_buscar_TextChanged(object sender, RoutedEventArgs e)
        {
            if (text_buscar.Text.Trim().Length == 0)
            {
                CarregarTodosPacientes();
            }
            else
            {
                if (rb_nomePaciente.IsChecked == true)
                {
                    BuscarPeloNome();
                }

                if (rb_idCentro.IsChecked == true)
                {
                    BuscarPeloID();

                }
            }
            
            
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (dataGrid1.SelectedItems.Count > 0)
            {
                var item = dataGrid1.SelectedItem;
              
                Paciente p = pacienteBLL.ObterPacientePeloCodigo(Convert.ToInt32((dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text));
                cad_pac = new usc_cadastro_paciente(p, EnumTipoOperacao_Manipulacao.Actualizar, mainPaciente_UserControl);
                mainPaciente_UserControl.NovoUserControl(cad_pac);
                mainPaciente_UserControl.label_title.Content = "Prontuário Paciente";
            }
        }
    }
}
