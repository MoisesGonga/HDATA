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
        private Centro_Hemodialise centro_Hemodialise;
        private Cadastro_Paciente cad_pac;
        public Listar_Pacientes()
        {
            InitializeComponent();
            AcessoDados = new AcessoDadosPostgreSQL();
            Conectar();
            centro_Hemodialise = new Centro_Hemodialise();
            Nomes_ = new List<string>();
            Nomes_.Add("Moisés");
            Nomes_.Add("Otalicio");
            Nomes_.Add("Aida");
            Nomes_.Add("Risa");

            Nomes_.Add("Orisa");
            Nomes_.Add("Orisa");
        }

        public Listar_Pacientes(Centro_Hemodialise centro_hemod, MainPaciente_UserControl mainPaciente_usercontrol)
        {
            InitializeComponent();
            AcessoDados = new AcessoDadosPostgreSQL();
            this.centro_Hemodialise = centro_hemod;
            this.mainPaciente_UserControl = mainPaciente_usercontrol;
            Conectar();
            centro_Hemodialise = new Centro_Hemodialise();
            Nomes_ = new List<string>();
            Nomes_.Add("Moisés");
            Nomes_.Add("Otalicio");
            Nomes_.Add("Aida");
            Nomes_.Add("Risa");

            Nomes_.Add("Orisa");
            Nomes_.Add("Orisa");
        }

        public Listar_Pacientes(Paciente p, MainPaciente_UserControl mainPaciente_usercontrol)
        {
            InitializeComponent();
            AcessoDados = new AcessoDadosPostgreSQL();
            this.mainPaciente_UserControl = mainPaciente_usercontrol;
            Conectar();
            this.p = p;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{selectedNomes_} segundo {text_buscar.SelectedItem}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Conectar();

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count > 0)
            {
                var item = dataGrid1.SelectedItem;
                //  string id = ((DataRowView)item).Row["id"].ToString();

                //MessageBox.Show(id);

                // 2 - SELECIONAR VALOR NA GRID EM WPF
                MessageBox.Show($"{(dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text} - {(dataGrid1.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text} - {(dataGrid1.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text}");
            }
        }
        public void Conectar()
        {

            
            try
            {

                //DataTable dt = AcessoDados.ExecututarConsulta(CommandType.Text, "Select id_diasemana As ID, nome As Nome, nome_abrev As abreviacao from \"DiaSemana\"");
                DataTable dt = AcessoDados.ExecututarConsulta(CommandType.Text, "select nome as Nome,naturalidade Naturalidade from dados_pessoais_view");
                dataGrid1.ItemsSource = dt.AsDataView();
               // text_buscar.ItemsSource = Nomes_;
               // text_buscar.SelectedItem = selectedNomes_;
                //text_buscar.FilterMode = AutoCompleteFilterMode.Contains;







            }
            catch (NpgsqlException ex)
            {

                MessageBox.Show("Não possivel se conectar ao Banco de Dados... " + ex.MessageText);
            }
        }

        private void btn_editar_Click(object sender, RoutedEventArgs e)
        {
            //Cadastro_Paciente cad_pac = null;
            //Paciente p = new Paciente();
            //p.Nome = "Adilson Silva";
            //p.Data_Entrada = DateTime.Now;
            //p.Genero_ = EnumGenero.Masculino;
            //cad_pac = new Cadastro_Paciente(p, Tipo_Operacao.Actualizacao, mainPaciente_UserControl, centro_Hemodialise);
            //mainPaciente_UserControl.label_title.Content = "Actualizar Paciente";
            //NavigationService.GridNavigationUsercontrol(grid_sub, cad_pac);

            
            Paciente p = new Paciente();
            p.Nome = "Adilson Silva";
            p.Data_Entrada = DateTime.Now;
            p.Genero_ = EnumGenero.Masculino;
            p.Data_nasc = new DateTime(1995, 01, 26);
            cad_pac = new Cadastro_Paciente(p, EnumTipoOperacao_Manipulacao.Actualizar, mainPaciente_UserControl);
            mainPaciente_UserControl.NovoUserControl(cad_pac);
            mainPaciente_UserControl.label_title.Content = "Actualizar Paciente";
            
            //NavigationService.GridNavigationUsercontrol(grid_sub, cad_pac);

        }
    }
}
