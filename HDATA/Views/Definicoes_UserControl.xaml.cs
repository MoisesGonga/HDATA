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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for Definicoes_UserControl.xaml
    /// </summary>
    public partial class Definicoes_UserControl : UserControl
    {
        Usuario user;

        public Definicoes_UserControl()
        {
            InitializeComponent();
        }

        public Definicoes_UserControl(Usuario usuario)
        {
            
            InitializeComponent();
            this.user = usuario;
            if (usuario.Perfil_Usuario != "Administrador")
            {
                tab_gerir_usuario.Visibility = Visibility.Hidden;
            }


        }

        private void DefinicaoContaUtilizador_UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txt_nome_pessoa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_idade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_bilhete_identidade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_nome_pai_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_nome_mae_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmb_estado_civil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txt_naturalidade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_nacionalidade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmb_raca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txt_habilitacao_literaria_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_municipio_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_pais_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_provincia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_rua_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_telefone_1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void main_tab_gerir_utilizadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
