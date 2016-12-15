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
using CamadaNegocio;
using CamadaObjectoTransferecia;
using System.Text.RegularExpressions;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for Cadastro_Paciente.xaml
    /// </summary>
    public partial class Cadastro_Paciente : UserControl
    {
        private Paciente p;
        private EnumTipoOperacao_Manipulacao tipo_operacao;
        private MainPaciente_UserControl mainPaciente_UserControl;
        private Centro_Hemodialise centro_Hemodialise;
        private ProvenienciaBLL provenienciaBLL;
        

        public Cadastro_Paciente(Paciente p, EnumTipoOperacao_Manipulacao operacao, MainPaciente_UserControl mainPaciente_UserControl)
        {
            InitializeComponent();
            this.p = p;
            lbl_RotuloPaciente.Visibility = Visibility.Visible;
            
            this.tipo_operacao = operacao;
            this.mainPaciente_UserControl = mainPaciente_UserControl;
            this.centro_Hemodialise = new Centro_Hemodialise();
            provenienciaBLL = new ProvenienciaBLL();

            CarregarDados(tipo_operacao);
        }

        public Cadastro_Paciente(EnumTipoOperacao_Manipulacao operacao, MainPaciente_UserControl mainPaciente_UserControl)
        {
            InitializeComponent();
            
            this.tipo_operacao = operacao;
            lbl_RotuloPaciente.Visibility = Visibility.Hidden;
            this.mainPaciente_UserControl = mainPaciente_UserControl;
            this.centro_Hemodialise = new Centro_Hemodialise();
            CarregarDados(tipo_operacao);
        }

        public void CarregarDados(EnumTipoOperacao_Manipulacao tipo)
        {
            if (tipo == EnumTipoOperacao_Manipulacao.Cadastrar)
            {
                //Carregar Combobox com Dados dos Enumeradores
                CarregarDadosPadrao();
            }
            else if(tipo == EnumTipoOperacao_Manipulacao.Actualizar)
            {
                CarregarDadosPadrao();
                CarregarDadosActualizacao();
            }
        }

        private void CarregarDadosActualizacao()
        {

            lbl_RotuloPaciente.Content = p.ToString();
            //DADOS PESSOAIS

            //DADOS DOS CONTACTOS

            //DADOS ENDERECO

            //DADOS ENTIDADE RESPONSAVEL
            p.Contacto_ = new Contacto();
            p.Endereco_ = new Endereco();
            p.Nome_Entidade = "Ministério da Saúde";
            p.Nr_Term_Resp  = "M120-2016";
            cmb_estado_civil.SelectedValue = p.Estado_civil;
            cmb_raca.SelectedValue = p.Raca;
            cmb_tipo_insuficiencia.SelectedValue = p.TipoInsuficiencia;
            txt_nome_pessoa.Text = p.Nome;
            txt_nome_pai.Text = p.Nome_pai;
            txt_nome_mae.Text = p.Nome_mae;
            txt_bilhete_identidade.Text = p.Num_BI;
            Date_Data_nascimento.Text = p.Data_nasc.ToString("d");
            txt_email.Text = p.Contacto_.email;
            txt_entidade_responsavel.Text = p.Nome_Entidade;
            txt_habilitacao_literaria.Text = p.Habilitacao_literaria;
            txt_IdCentroHD.Text = p.Id_pessoa.ToString();
            txt_identificacao_hp.Text = p.Identificacao_hp;
            txt_medico_enviou.Text = p.Medico_Enviou;
            txt_municipio.Text = p.Endereco_.municipio;
            txt_nacionalidade.Text = p.Nacionalidade;
            txt_naturalidade.Text = p.Naturalidade;
            txt_nome_parente.Text = p.Contacto_.nome_parente;
            txt_nr_term_responsabilidade.Text = p.Nr_Term_Resp;
            txt_pais.Text = p.Endereco_.pais;
            txt_provincia.Text = p.Endereco_.provincia;
            txt_rua.Text = p.Endereco_.rua;
            txt_telefone_1.Text = p.Contacto_.telefone_1;
            txt_telefone_parente.Text = p.Contacto_.telefone_parente;

        }

        private void CadastrarPaciente()
        {
            if (ValidarCampos())
            {
                EnumGenero genero = EnumGenero.Masculino;
                genero = Rb_Genero_M.IsChecked == true ? EnumGenero.Masculino : EnumGenero.Feminino;

                
                //+EnumEstadoCivil enuestadocivil = Enum.Parse(typeof(EnumEstadoCivil), cmb_estado_civil.Text));
                //cmb_estado_civil.SelectedItem
                EnumEstadoCivil estado_civil = EnumEstadoCivil.Solteiro;
                if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Casado.ToString()))
                {
                    estado_civil =  EnumEstadoCivil.Casado;
                }
                else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Companheiro.ToString()))
                {
                    estado_civil = EnumEstadoCivil.Companheiro;
                }
                else if(cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Divorciado.ToString()))
                {
                    estado_civil = EnumEstadoCivil.Divorciado;
                }
                else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Separado.ToString()))
                {
                    estado_civil = EnumEstadoCivil.Separado;
                }
                else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil. Viuvo.ToString()))
                {
                    estado_civil = EnumEstadoCivil.Viuvo;
                }
               
                else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Divorciado.ToString()))
                {
                    estado_civil = EnumEstadoCivil.Companheiro;
                }

                Paciente paciente = new Paciente(txt_nome_pessoa.Text, txt_nome_pai.Text, txt_nome_mae.Text, txt_naturalidade.Text, txt_nacionalidade.Text, Date_Data_nascimento.SelectedDate.Value, estado_civil, genero, txt_bilhete_identidade.Text, txt_habilitacao_literaria.Text, new Contacto(txt_telefone_1.Text, txt_email.Text, txt_telefone_parente.Text, txt_nome_parente.Text), new Endereco(txt_pais.Text,txt_provincia.Text, txt_municipio.Text, txt_rua.Text), txt_identificacao_hp.Text, date_Data_Inicio_Tratamento_Centro.SelectedDate.Value, txt_medico_enviou.Text, Date_Data_Inicio_Tratamento.SelectedDate.Value, cmb_raca.Text, new Proveniencia(Convert.ToInt32(cmb_proveniencia.SelectedValuePath)), txt_entidade_responsavel.Text,txt_nr_term_responsabilidade.Text);
                paciente.Id_pessoa =  centro_Hemodialise.CadastrarPaciente(paciente);
                txt_IdCentroHD.Text = paciente.Id_pessoa.ToString();
            }
        }
       
        private bool ValidarCampos()
        {
            bool var_ = true;
            if (Regex.IsMatch(txt_nome_pessoa.Text, @"^[a-zA-Z]+$") || string.IsNullOrEmpty(txt_nome_pessoa.Text.Trim()))
            {
                var_ = false;
            }
            if(string.IsNullOrEmpty(txt_nome_pessoa.Text.Trim()) )
            {
                txt_nome_pessoa.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }
            if(string.IsNullOrEmpty(txt_nome_pai.Text.Trim())  ||string.IsNullOrEmpty(txt_nome_mae.Text))
            {
                txt_nome_pai.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }
            if (string.IsNullOrEmpty(txt_nome_mae.Text))
            {
                txt_nome_mae.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }
            if (string.IsNullOrEmpty(Date_Data_nascimento.Text.Trim()))
            {
                Date_Data_nascimento.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }
            if (string.IsNullOrEmpty(txt_bilhete_identidade.Text.Trim()))
            {
                txt_bilhete_identidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }
            if (string.IsNullOrEmpty(cmb_estado_civil.Text.Trim()))
            {
                cmb_estado_civil.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }

            if (string.IsNullOrEmpty(txt_naturalidade.Text.Trim()))
            {
                txt_naturalidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }

            if (string.IsNullOrEmpty(txt_nacionalidade.Text.Trim()))
            {
                txt_nacionalidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }

            if (string.IsNullOrEmpty(cmb_raca.Text.Trim()))
            {
                cmb_raca.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }

            if (string.IsNullOrEmpty(txt_medico_enviou.Text.Trim()))
            {
                txt_medico_enviou.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
            }

            if (var_ == false)
            {
                MessageBox.Show("Por favor preencha correctamente os campos destacados...");
            }
            //COR NORMAL 171,173,179
            return var_;
        }

        private void ActualizarDados()
        {

        }

        private void CarregarDadosPadrao()
        {
            //DADOS PADRAO DA TABELA PESSOA
            cmb_tipo_insuficiencia.ItemsSource = Enum.GetValues(typeof(EnumTipoInsuficiencia));
            cmb_tipo_insuficiencia.SelectedIndex = 0;
            cmb_estado_civil.ItemsSource = Enum.GetValues(typeof(EnumEstadoCivil));
            cmb_estado_civil.SelectedIndex = 0;
            cmb_raca.ItemsSource = Enum.GetValues(typeof(EnumRaca));
            cmb_raca.SelectedIndex = 0;
            cmb_proveniencia.ItemsSource = provenienciaBLL.ListarProveniencia();
            cmb_proveniencia.SelectedValuePath = "Id_Proveniencia";
            cmb_proveniencia.SelectedIndex = 0;
            //
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            NavigationService.GridNavigationUsercontrol(mainPaciente_UserControl.GetSubGrid(), new Listar_Pacientes());
            mainPaciente_UserControl.label_title.Content = "Lista de Pacientes";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(cmb_estado_civil.SelectedValue.ToString() + "- "+ cmb_raca.SelectedValue.ToString()+" - "+ cmb_tipo_insuficiencia.SelectedValue.ToString());
            CadastrarPaciente();
        }

        private void Date_Data_nascimento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_idade.Text = MostrarCalcularIdade();
        }

        private string MostrarCalcularIdade()
        {
            DateTime date = Date_Data_nascimento.SelectedDate.Value;
            string res = "";
            if (date != null || string.IsNullOrEmpty(Date_Data_nascimento.Text))
            {
             res =  txt_idade.Text = $"{DateTime.Now.Date.Year - date.Year} ano(s)";
            }
            return res;
        }

        private void txt_Nome_pessoa_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if ((Rb_Genero_F.IsChecked != Rb_Genero_F.IsChecked) && !(string.IsNullOrEmpty(txt_nome_pessoa.Text)) && !(string.IsNullOrEmpty(date_Data_Inicio_Tratamento_Centro.Text)))
            //{
            //    Paciente pc = new Paciente();
            //    pc.Nome = txt_nome_pessoa.Text;
            //    pc.Data_Entrada = (DateTime) date_Data_Inicio_Tratamento_Centro.SelectedDate;
            //    pc.Genero_ = EnumGenero.Masculino;
            //    lbl_RotuloPaciente.Visibility = Visibility.Visible;
            //    lbl_RotuloPaciente.Content =  pc.ToString();


            //}
        }

       

        private void cmb_proveniencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_proveniencia.SelectedValue != null)
            {
                MessageBox.Show(cmb_proveniencia.SelectedValue + " - "+cmb_proveniencia.SelectedItem);
            }
        }

        private void btn_novo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
