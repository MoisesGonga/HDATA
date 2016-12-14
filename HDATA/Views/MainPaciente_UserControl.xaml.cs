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
using CamadaNegocio;
using CamadaObjectoTransferecia;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for MainPaciente_UserControl.xaml
    /// </summary>
    public partial class MainPaciente_UserControl : UserControl
    {

        
      //  string conectionString = "server = localhost; port = 5432; Database=bd_hemodialise; User id = postgres; Password=Administrador";
        AcessoDadosPostgreSQL AcessoDados = new AcessoDadosPostgreSQL();
        Listar_Pacientes list_pac;
        Cadastro_Paciente cad_pac;
        Centro_Hemodialise centro_Hemodialise;

        public MainPaciente_UserControl()
        {
            InitializeComponent();
            list_pac = new Views.Listar_Pacientes(centro_Hemodialise, this);
            cad_pac = null;
            NavigationService.GridNavigationUsercontrol(grid_sub, list_pac);
        }

        public void EntrarFacil()
        {
            NavigationService.GridNavigationUsercontrol(this.grid_sub, this.list_pac);
        }

        public void NovoUserControl (UserControl control)
        {
            NavigationService.GridNavigationUsercontrol(grid_sub, control);
        }
        
       

            

        private void btn_list_Click(object sender, RoutedEventArgs e)
        {
            list_pac = new Views.Listar_Pacientes(centro_Hemodialise, this);
            NavigationService.GridNavigationUsercontrol(grid_sub, list_pac);
            label_title.Content = "Lista de Pacientes";
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {

          Paciente p = new Paciente();
           p.Nome = "Adilson Silva";
           p.Data_Entrada = DateTime.Now;
           p.Genero_ = EnumGenero.Masculino;
            cad_pac = new Cadastro_Paciente(p, EnumTipoOperacao_Manipulacao.Actualizar, this);
            label_title.Content = "Actualizar Paciente";
            NavigationService.GridNavigationUsercontrol(grid_sub, cad_pac);
        }
        
        public Grid GetSubGrid()
        {
            return grid_sub;
        }

        private void btn_cad_Click(object sender, RoutedEventArgs e)
        {
            cad_pac = new Cadastro_Paciente(EnumTipoOperacao_Manipulacao.Cadastrar, this);
            label_title.Content = "Cadastrar Paciente";
            NavigationService.GridNavigationUsercontrol(grid_sub,cad_pac);
            
        }
                private void button_Click(object sender, RoutedEventArgs e)
        {
            Paciente p = new Paciente();
            p.Nome = "Adilson Silva";
            p.Data_Entrada = DateTime.Now;
            p.Genero_ = EnumGenero.Masculino;
            cad_pac = new Cadastro_Paciente(p, EnumTipoOperacao_Manipulacao.Actualizar, this);
            label_title.Content = "Actualizar Paciente";
            NavigationService.GridNavigationUsercontrol(grid_sub, cad_pac);
        }
    }
}
