using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CamadaNegocio;
using CamadaObjectoTransferecia;
using System.Windows.Media.Effects;
using System.Text.RegularExpressions;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for usc_cadastro_paciente.xaml
    /// </summary>
    public partial class usc_cadastro_paciente : UserControl
    {
        private Paciente p;
        private PacienteBLL pacienteBLL;
        private ProvenienciaBLL provenienciaBLL;
        private AcessoVascularBLL acessoVascularBLL;
        private TipoAcessoVascularBLL tipoAcessoVascularBLL;
        private Prescricao prescrical;
        private PrescricaoBLL prescricaoBLL;
        private EnumTipoOperacao_Manipulacao tipo_operacao;
        private MainPaciente_UserControl mainPaciente_UserControl;
        Prescricao_UserControl prescricao_UserControl;
        private Tipo_Operacao tipo_operacaoAcessoVascular;

        public usc_cadastro_paciente(Paciente p, EnumTipoOperacao_Manipulacao operacao, MainPaciente_UserControl mainPaciente_UserControl)
        {
            InitializeComponent();
            acessoVascularBLL = new AcessoVascularBLL();
            tipoAcessoVascularBLL = new TipoAcessoVascularBLL();
            lbl_RotuloPaciente.Visibility = Visibility.Visible;
            this.tipo_operacao = operacao;
            this.mainPaciente_UserControl = mainPaciente_UserControl;
            pacienteBLL = new PacienteBLL();
            provenienciaBLL = new ProvenienciaBLL();
            this.p = p;
            CarregarDados(tipo_operacao);
            if (p != null)
            {
            prescricao_UserControl = new Prescricao_UserControl(this.p,EnumTipoOperacao_Manipulacao.Cadastrar,this);
            }
            NavigationService.GridNavigationUsercontrol(grid_prescricao, prescricao_UserControl);
            prescricaoBLL = new PrescricaoBLL();
            ListarPrescriçãoPaciente();
        }

        public usc_cadastro_paciente(EnumTipoOperacao_Manipulacao operacao, MainPaciente_UserControl mainPaciente_UserControl)
        {
            InitializeComponent();
            pacienteBLL = new PacienteBLL();
            acessoVascularBLL = new AcessoVascularBLL();
            tipoAcessoVascularBLL = new TipoAcessoVascularBLL();
            this.tipo_operacao = operacao;
            lbl_RotuloPaciente.Visibility = Visibility.Hidden;
            this.mainPaciente_UserControl = mainPaciente_UserControl;
            provenienciaBLL = new ProvenienciaBLL();
            prescricao_UserControl = new Prescricao_UserControl();

            NavigationService.GridNavigationUsercontrol(grid_prescricao, prescricao_UserControl);
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
                CarregarListViewAcessoVascular();
                CarregarComboboxTipoAcessoVascular();
            }
        }

        private void CarregarDadosActualizacao()
        {
            lbl_RotuloPaciente.Content = p.ToString();
            cmb_estado_civil.SelectedValue = p.Estado_civil;
            EnumRaca raca = EnumRaca.Amarela;
            if (p.Raca.ToString().Equals(EnumRaca.Negra))
            {
                raca = EnumRaca.Negra ;
            }
            else if (p.Raca.ToString().Equals(EnumRaca.Branca))
            {
                raca = EnumRaca.Branca;
            }

            if (p.Genero_.Equals(EnumGenero.Masculino)) {
                Rb_Genero_M.IsChecked = true;
                Rb_Genero_F.IsChecked = false;
            }
            else
            {
                Rb_Genero_M.IsChecked = false;
                Rb_Genero_F.IsChecked = true;
            }
            
            cmb_raca.SelectedValue = raca;
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
            Date_Data_Inicio_Tratamento.Text = p.Data_Inicio_HD.ToShortDateString();
            date_Data_Inicio_Tratamento_Centro.Text = p.Data_Entrada.ToShortDateString();
            date_Data_Saida.Text = p.Data_Saida.ToShortDateString();
        }

        private void CadastrarPaciente()
        {
            if (ValidarCamposDadosPacientes())
            {
                try
                {
                    EnumGenero genero = EnumGenero.Masculino;
                    genero = Rb_Genero_M.IsChecked == true ? EnumGenero.Masculino : EnumGenero.Feminino;
                    //+EnumEstadoCivil enuestadocivil = Enum.Parse(typeof(EnumEstadoCivil), cmb_estado_civil.Text));
                    //cmb_estado_civil.SelectedItem
                    EnumEstadoCivil estado_civil = EnumEstadoCivil.Solteiro;
                    if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Casado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Casado;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Companheiro.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Companheiro;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Divorciado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Divorciado;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Separado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Separado;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Viuvo.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Viuvo;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Divorciado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Companheiro;
                    }

                    //Paciente paciente = new Paciente(txt_nome_pessoa.Text, txt_nome_pai.Text, txt_nome_mae.Text, txt_naturalidade.Text, txt_nacionalidade.Text, Date_Data_nascimento.SelectedDate.Value, estado_civil, genero, txt_bilhete_identidade.Text, txt_habilitacao_literaria.Text, new Contacto(txt_telefone_1.Text, txt_email.Text, txt_telefone_parente.Text, txt_nome_parente.Text), new Endereco(txt_pais.Text, txt_provincia.Text, txt_municipio.Text, txt_rua.Text), txt_identificacao_hp.Text, date_Data_Inicio_Tratamento_Centro.SelectedDate.Value, date_Data_Saida.SelectedDate.Value, txt_medico_enviou.Text, Date_Data_Inicio_Tratamento.SelectedDate.Value, cmb_raca.Text, new Proveniencia(Convert.ToInt32(cmb_proveniencia.SelectedValuePath)), txt_entidade_responsavel.Text, txt_nr_term_responsabilidade.Text);
                    Paciente paciente = new Paciente();
                    paciente.Nome = txt_nome_pessoa.Text;
                    paciente.Nome_pai = txt_nome_pai.Text;
                    paciente.Nome_mae = txt_nome_mae.Text;
                    paciente.Naturalidade = txt_naturalidade.Text;
                    paciente.Nacionalidade = txt_nacionalidade.Text;
                    paciente.TipoInsuficiencia = (cmb_tipo_insuficiencia.SelectedItem.ToString() == EnumTipoInsuficiencia.Aguda.ToString()) ? EnumTipoInsuficiencia.Aguda : EnumTipoInsuficiencia.Cronica;
                    paciente.Data_nasc = Date_Data_nascimento.SelectedDate.Value;
                    paciente.Estado_civil = estado_civil;
                    paciente.Genero_ = genero;
                    paciente.Num_BI = txt_bilhete_identidade.Text;
                    paciente.Habilitacao_literaria = txt_habilitacao_literaria.Text;
                    paciente.Contacto_ = new Contacto(txt_telefone_1.Text, txt_email.Text, txt_telefone_parente.Text, txt_nome_parente.Text);
                    paciente.Endereco_ = new Endereco(txt_pais.Text, txt_provincia.Text, txt_municipio.Text, txt_rua.Text);
                    //if (string.IsNullOrEmpty(txt_identificacao_hp.Text))
                    //{
                        Random r = new Random();
                        txt_identificacao_hp.Text = "H-" + r.Next(1, 1000) + "/" + DateTime.Now.Year;
                        paciente.Identificacao_hp = txt_identificacao_hp.Text;
                    //}
                        if (string.IsNullOrEmpty(date_Data_Inicio_Tratamento_Centro.Text))
                        {
                            date_Data_Inicio_Tratamento_Centro.Text = DateTime.Now.ToShortDateString();
                        }
                        paciente.Data_Inicio_HD = Date_Data_Inicio_Tratamento.SelectedDate.Value;

                        paciente.Data_Entrada = date_Data_Inicio_Tratamento_Centro.SelectedDate.Value;

                        if (string.IsNullOrEmpty(date_Data_Saida.Text))
                        {
                            date_Data_Saida.Text = DateTime.MinValue.ToString("d");
                        }

                        paciente.Data_Saida = date_Data_Saida.SelectedDate.Value;
                        paciente.Medico_Enviou = txt_medico_enviou.Text;
                        paciente.Raca = cmb_raca.Text;
                        paciente.Proveniencia_ = new Proveniencia(Convert.ToInt32(cmb_proveniencia.SelectedValue));
                        paciente.Nome_Entidade = txt_entidade_responsavel.Text;
                        paciente.Nr_Term_Resp = txt_nr_term_responsabilidade.Text;
                        lbl_RotuloPaciente.Visibility = Visibility.Visible;
                        lbl_RotuloPaciente.Content = paciente.ToString();
                        int idpaciente = pacienteBLL.CadastrarPaciente(paciente);
                        txt_IdCentroHD.Text = idpaciente + "";
                    paciente.Id_pessoa = idpaciente;
                    // ContactoBLL contBLL = new ContactoBLL();
                    //MessageBox.Show(endBLL.AlterarEndereco(new Endereco(3, "Cuba", "Habana", "Gaunabaz", "Fidel de Castro Hero!")));
                    //MessageBox.Show(contBLL.AlterarContacto(new Contacto(3,"945216547"," vidalinho@gmail.com","+244945648421","Victor António")));
                    //PessoaBLL pBLL = new PessoaBLL();
                    // PacienteBLL pcBLL = new PacienteBLL();
                    //Paciente pc = new Paciente("2 Piniel Gonga", "Daniel Gonga", "Joana Caculo Dala", "Luanda", "Angolana", new DateTime(1995, 01, 26), EnumEstadoCivil.Solteiro, EnumGenero.Masculino, "004002082LA034", "Técnico Médio de Informatica", new Contacto("+244941808111", "moitimdg95@gmail.com", "+244922095655", "Daniela António"), new Endereco("Angola", "Luanda", "Luanda", "Rua Dr. António Agostinho Neto"), "H-15-2016", DateTime.Now.Date, DateTime.MinValue, "Dra. Mara", new DateTime(2008, 04, 24), "Negra", new Proveniencia(1), "Ministério das Telecomunicações", "TERM Nº 125/2016");
                    //MessageBox.Show(pBLL.CadastrarPessoaFunction(pc)+"");
                    //MessageBox.Show(pcBLL.CadastrarPaciente(pc) + "");
                    //MessageBox.Show(contBLL.AlterarContacto(new Contacto(3,"945216547"," vidalinho@gmail.com","+244945648421","Victor António")));
                    //endereco = new Endereco(4, "España", "Madrid", "Gaunabaz", "Fidel de Castro Hero!");
                    this.p = paciente;
                    string str_msg = paciente.Genero_.Equals(EnumGenero.Masculino) ? $"O Paciente {paciente.Nome} foi cadastrado com sucesso no sistema!!! " : $"A Paciente {paciente.Nome} foi cadastrado com sucesso no sistema";

                    MessageBox.Show(str_msg, "Sucesso Cadastro",MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.OK,MessageBoxOptions.ServiceNotification);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Não foi possivel cadastrar o paciente, por favor verifique os dados: {ex.Message}", "Cadastro Paciente", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool ValidarCamposDadosPacientes()
        {
            int count = 0;
            string mensagem_ = "";
            bool var_ = true;
           
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
                var_ = false;
            }
            if (string.IsNullOrEmpty(txt_bilhete_identidade.Text.Trim()))
            {
                txt_bilhete_identidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }
             if (!Regex.IsMatch(txt_bilhete_identidade.Text, "(\\d){9}\\D{2}\\d{3}"))
            {
                count++;
                txt_bilhete_identidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
                mensagem_ = $"{count}. Erro no formato do Nº do bilhete de identidade.\n";
            }
            if (string.IsNullOrEmpty(cmb_estado_civil.Text.Trim()))
            {
                cmb_estado_civil.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_naturalidade.Text.Trim()))
            {
                txt_naturalidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_nacionalidade.Text.Trim()))
            {
                txt_nacionalidade.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(cmb_raca.Text.Trim()))
            {
                cmb_raca.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_medico_enviou.Text.Trim()))
            {
                txt_medico_enviou.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_telefone_1.Text.Trim()))
            {
                txt_telefone_1.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_nome_parente.Text.Trim()))
            {
                txt_nome_parente.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_rua.Text.Trim()))
            {
                txt_rua.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_municipio.Text.Trim()))
            {
                txt_municipio.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_pais.Text.Trim()))
            {
                txt_pais.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_provincia.Text.Trim()))
            {
                txt_provincia.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (string.IsNullOrEmpty(txt_telefone_parente.Text.Trim()))
            {
                txt_telefone_parente.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                var_ = false;
            }

            if (!string.IsNullOrEmpty(date_Data_Saida.Text))
            {
                if ((date_Data_Saida.SelectedDate.Value < date_Data_Inicio_Tratamento_Centro.SelectedDate.Value))
                {
                    count++;
                    mensagem_ += $"{count}. A data do Inicio do Tratamento não pode ser maior do que a Data de Saída do Paciente.\n";
                    //mensagem_ = "2. A data do Inicio do Tratamento não pode ser maior do que Data de Inicio do Tratamento no Centro.";
                    date_Data_Saida.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                    date_Data_Inicio_Tratamento_Centro.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                    var_ = false;
                }

            }

            if (!string.IsNullOrEmpty(date_Data_Inicio_Tratamento_Centro.Text) && !string.IsNullOrEmpty(Date_Data_Inicio_Tratamento.Text))
            {
                if ((Date_Data_Inicio_Tratamento.SelectedDate.Value > date_Data_Inicio_Tratamento_Centro.SelectedDate.Value))
                {
                    count++;
                    mensagem_ += $"{count}. A data do Inicio do Tratamento não pode ser maior do que Data de Inicio do Tratamento no Centro.\n";
                    //mensagem_ = "2. A data do Inicio do Tratamento não pode ser maior do que Data de Inicio do Tratamento no Centro.";
                    Date_Data_Inicio_Tratamento.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                    date_Data_Inicio_Tratamento_Centro.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                    var_ = false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(date_Data_Inicio_Tratamento_Centro.Text))
                {
                    date_Data_Inicio_Tratamento_Centro.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                    var_ = false;
                }
                if (string.IsNullOrEmpty(Date_Data_Inicio_Tratamento.Text))
                {
                    Date_Data_Inicio_Tratamento.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                    var_ = false;
                }
            }
            
            if (var_ == false)
            {
                //MessageBox.Show("Os campos destacados não podem estar vazios...");
                count++;
                mensagem_ += $"{count}. O(s) campos destacados não podem estar vazios";
                MessageBox.Show(mensagem_, "Verificar os Campos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //COR NORMAL 171,173,179
            return var_;
        }

        private void CarregarDadosPadrao()
        {
            //DADOS PADRAO DA TABELA PESSOA - PACIENTE - PROVENIÊNICA
            cmb_tipo_insuficiencia.ItemsSource = Enum.GetValues(typeof(EnumTipoInsuficiencia));
            cmb_tipo_insuficiencia.SelectedIndex = 0;
            cmb_estado_civil.ItemsSource = Enum.GetValues(typeof(EnumEstadoCivil));
            cmb_estado_civil.SelectedIndex = 0;
            cmb_raca.ItemsSource = Enum.GetValues(typeof(EnumRaca));
            cmb_raca.SelectedIndex = 0;
            cmb_proveniencia.ItemsSource = provenienciaBLL.ListarProveniencia();
            cmb_proveniencia.SelectedValuePath = "Id_Proveniencia";
            cmb_proveniencia.SelectedIndex = 0;

            //CARREGAR DADOS PADRÃO ACESSO VASCULAR
                     
            
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
        }

        private void Date_Data_nascimento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_idade.Text = MostrarCalcularIdade();
            Date_Data_nascimento.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private string MostrarCalcularIdade()
        {
            string res = "";
            if (!string.IsNullOrEmpty(Date_Data_nascimento.Text))  {
                DateTime date = Date_Data_nascimento.SelectedDate.Value;

                if (date != null || string.IsNullOrEmpty(Date_Data_nascimento.Text))
                {
                    res = txt_idade.Text = $"{DateTime.Now.Date.Year - date.Year} ano(s)";
                }

            }
            return res;
        }

        private void txt_Nome_pessoa_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_nome_pessoa.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
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
         //   cmb_proveniencia.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
           //if (cmb_proveniencia.SelectedValue != null)
           //{
           //     MessageBox.Show(cmb_proveniencia.SelectedValue + " - " + cmb_proveniencia.SelectedItem);
           // }
        }

        private void btn_novo_Click(object sender, RoutedEventArgs e)
        {
            tipo_operacao = EnumTipoOperacao_Manipulacao.Cadastrar;
            lbl_RotuloPaciente.Visibility = Visibility.Hidden;
            mainPaciente_UserControl.label_title.Content = "Prontuário Paciente";
            lbl_RotuloPaciente.Content = "";
            //LimparCampoDadosPaciente();
        }

        public void LimparCampoDadosPaciente()
        {
            txt_nome_pessoa.Clear();
            txt_nome_pai.Clear();
            txt_nome_mae.Clear();
            txt_naturalidade.Clear();
            txt_nacionalidade.Clear();
            Date_Data_nascimento.Text = "";
            txt_bilhete_identidade.Clear();
            txt_habilitacao_literaria.Clear();
            txt_telefone_1.Clear();
            txt_email.Clear();
            txt_telefone_parente.Clear();
            txt_nome_parente.Clear();
            txt_pais.Clear();
            txt_provincia.Clear();
            txt_municipio.Clear();
            txt_rua.Clear();
            txt_identificacao_hp.Clear();
            date_Data_Inicio_Tratamento_Centro.Text = "";
            Date_Data_Inicio_Tratamento.Text = "";
            date_Data_Inicio_Tratamento_Centro.Text = "";
            date_Data_Saida.Text = "";
            txt_medico_enviou.Clear();
            cmb_raca.SelectedIndex = 0;
            cmb_proveniencia.SelectedIndex = 0;
            cmb_estado_civil.SelectedIndex = 0;
            cmb_tipo_insuficiencia.SelectedIndex = 0;
            txt_entidade_responsavel.Clear();
            txt_nr_term_responsabilidade.Clear();
            txt_IdCentroHD.Clear();
        }

        private void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            if (tipo_operacao == EnumTipoOperacao_Manipulacao.Cadastrar)
            {
                CadastrarPaciente();
                tipo_operacao = EnumTipoOperacao_Manipulacao.Actualizar;
            }
            if (tipo_operacao == EnumTipoOperacao_Manipulacao.Actualizar)
            {
                ActualizarPaciente();
            }
        }

        private void ActualizarPaciente()
        {
            if (ValidarCamposDadosPacientes())
            {
                try
                {
                    EnumGenero genero = EnumGenero.Masculino;
                    genero = Rb_Genero_M.IsChecked == true ? EnumGenero.Masculino : EnumGenero.Feminino;
                    //+EnumEstadoCivil enuestadocivil = Enum.Parse(typeof(EnumEstadoCivil), cmb_estado_civil.Text));
                    //cmb_estado_civil.SelectedItem
                    EnumEstadoCivil estado_civil = EnumEstadoCivil.Solteiro;
                    if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Casado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Casado;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Companheiro.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Companheiro;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Divorciado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Divorciado;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Separado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Separado;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Viuvo.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Viuvo;
                    }
                    else if (cmb_estado_civil.SelectedValue.ToString().Equals(EnumEstadoCivil.Divorciado.ToString()))
                    {
                        estado_civil = EnumEstadoCivil.Companheiro;
                    }

                    //Paciente paciente = new Paciente(txt_nome_pessoa.Text, txt_nome_pai.Text, txt_nome_mae.Text, txt_naturalidade.Text, txt_nacionalidade.Text, Date_Data_nascimento.SelectedDate.Value, estado_civil, genero, txt_bilhete_identidade.Text, txt_habilitacao_literaria.Text, new Contacto(txt_telefone_1.Text, txt_email.Text, txt_telefone_parente.Text, txt_nome_parente.Text), new Endereco(txt_pais.Text, txt_provincia.Text, txt_municipio.Text, txt_rua.Text), txt_identificacao_hp.Text, date_Data_Inicio_Tratamento_Centro.SelectedDate.Value, date_Data_Saida.SelectedDate.Value, txt_medico_enviou.Text, Date_Data_Inicio_Tratamento.SelectedDate.Value, cmb_raca.Text, new Proveniencia(Convert.ToInt32(cmb_proveniencia.SelectedValuePath)), txt_entidade_responsavel.Text, txt_nr_term_responsabilidade.Text);
                    Paciente paciente = new Paciente();
                    paciente.Id_pessoa = p.Id_pessoa;
                    paciente.Nome = txt_nome_pessoa.Text;
                    paciente.Nome_pai = txt_nome_pai.Text;
                    paciente.Nome_mae = txt_nome_mae.Text;
                    paciente.Naturalidade = txt_naturalidade.Text;
                    paciente.Nacionalidade = txt_nacionalidade.Text;
                    paciente.TipoInsuficiencia = (cmb_tipo_insuficiencia.SelectedItem.ToString() == EnumTipoInsuficiencia.Aguda.ToString()) ? EnumTipoInsuficiencia.Aguda : EnumTipoInsuficiencia.Cronica;
                    paciente.Data_nasc = Date_Data_nascimento.SelectedDate.Value;
                    paciente.Estado_civil = estado_civil;
                    paciente.Genero_ = genero;
                    paciente.Num_BI = txt_bilhete_identidade.Text;
                    paciente.Habilitacao_literaria = txt_habilitacao_literaria.Text;
                    paciente.Contacto_ = new Contacto(txt_telefone_1.Text, txt_email.Text, txt_telefone_parente.Text, txt_nome_parente.Text);
                    paciente.Endereco_ = new Endereco(txt_pais.Text, txt_provincia.Text, txt_municipio.Text, txt_rua.Text);
                    //if (string.IsNullOrEmpty(txt_identificacao_hp.Text))
                    //{
                   
                    paciente.Identificacao_hp = txt_identificacao_hp.Text;
                    //}
                    if (string.IsNullOrEmpty(date_Data_Inicio_Tratamento_Centro.Text))
                    {
                        date_Data_Inicio_Tratamento_Centro.Text = DateTime.Now.ToShortDateString();
                    }
                    paciente.Data_Inicio_HD = Date_Data_Inicio_Tratamento.SelectedDate.Value;

                    paciente.Data_Entrada = date_Data_Inicio_Tratamento_Centro.SelectedDate.Value;

                    if (string.IsNullOrEmpty(date_Data_Saida.Text))
                    {
                        date_Data_Saida.Text = DateTime.MinValue.ToString("d");
                    }

                    paciente.Data_Saida = date_Data_Saida.SelectedDate.Value;
                    paciente.Medico_Enviou = txt_medico_enviou.Text;
                    paciente.Raca = cmb_raca.Text;
                    paciente.Proveniencia_ = new Proveniencia(Convert.ToInt32(cmb_proveniencia.SelectedValue));
                    paciente.Nome_Entidade = txt_entidade_responsavel.Text;
                    paciente.Nr_Term_Resp = txt_nr_term_responsabilidade.Text;
                    lbl_RotuloPaciente.Visibility = Visibility.Visible;
                    lbl_RotuloPaciente.Content = paciente.ToString();
                    paciente.Id_pessoa = int.Parse(txt_IdCentroHD.Text);
                    txt_IdCentroHD.Text = pacienteBLL.ActualizarPaciente(paciente).ToString();
                    this.p = paciente;
                    this.tipo_operacao = EnumTipoOperacao_Manipulacao.Actualizar;

                    string str_msg = paciente.Genero_.Equals(EnumGenero.Masculino) ? $"Dados Administrativos do Paciente {paciente.Nome} Actualizados com sucesso!!!": $"Dados Administrativos da Paciente {paciente.Nome} Actualizados com sucesso!!!";

                    MessageBox.Show(str_msg, "Sucesso Actualização", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception("Erro ao Actualizar Paciente: " + ex.Message);
                }
            }
        }

        private void txt_bilhete_identidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_bilhete_identidade.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_nome_pai_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_nome_pai.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_nome_mae_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_nome_mae.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void cmb_estado_civil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_estado_civil.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_naturalidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_naturalidade.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_nacionalidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_nacionalidade.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_telefone_parente_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_telefone_parente.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_nome_parente_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_nome_parente.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_entidade_responsavel_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_entidade_responsavel.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void Date_Data_Inicio_Tratamento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Date_Data_Inicio_Tratamento.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void date_Data_Inicio_Tratamento_Centro_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            date_Data_Inicio_Tratamento_Centro.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void date_Data_Saida_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            date_Data_Saida.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_telefone_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_telefone_1.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_medico_enviou_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_medico_enviou.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_rua_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_rua.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_municipio_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_municipio.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_provincia_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_provincia.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_pais_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_pais.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void cmb_Tipo_Acesso_Vascular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //MessageBox.Show("NOME: "+ cmb_Tipo_Acesso_Vascular.SelectedItem + " - ID "+ cmb_Tipo_Acesso_Vascular.SelectedValue);
        }

        /// <summary>
        /// METODOS REFERENTES AO ACESSO VASCULAR
        /// </summary>
        /// <returns></returns>
        /// 

        private void CarregarListViewAcessoVascular()
        {
           
            if (!string.IsNullOrEmpty(p.Id_pessoa.ToString()))
            {
                AcessoVascularListView.ItemsSource = null;
                AcessoVascularListView.ItemsSource = acessoVascularBLL.ListarAcessosVascular(p);
            }
        }

        private void CarregarComboboxTipoAcessoVascular()
        {
            cmb_Tipo_Acesso_Vascular.ItemsSource = tipoAcessoVascularBLL.ListaTipoAcessoVascular();
            cmb_Tipo_Acesso_Vascular.SelectedValuePath = "Id_tipo_acesso";
            cmb_Tipo_Acesso_Vascular.SelectedIndex = 0;
        }

        private bool ValidarCamposAcessoVascular()
        {
            bool validar = true;

            if (string.IsNullOrEmpty(date_Data_realizacao.Text))
            {
                date_Data_realizacao.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                validar = false;
            }
             if (string.IsNullOrEmpty(txt_clinica_hospital.Text))
            {
                txt_clinica_hospital.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                validar = false;
            }
             if (string.IsNullOrEmpty(txt_local_acessoVascular.Text))
            {
                txt_local_acessoVascular.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                validar = false;
            }
             if (string.IsNullOrEmpty(txt_cirugiao_nefrologista.Text))
            {
                txt_cirugiao_nefrologista.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                validar = false;
            }

            if (string.IsNullOrEmpty(txt_director_clinico.Text))
            {
                txt_director_clinico.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                validar = false;
            }

            if (!string.IsNullOrEmpty(date_dataFalencia.Text) && string.IsNullOrEmpty(txt_motivos_falencia.Text) )
            {
                date_dataFalencia.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                txt_motivos_falencia.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                validar = false;
            }

            if (validar == false)
            {
                //MessageBox.Show("Os campos destacados não podem estar vazios...");

                MessageBox.Show("1. O(s) campos destacados não podem estar vazios. ", "Verificar os Campos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return validar;
        }

        private void btn_novo_acessoVascular_Click(object sender, RoutedEventArgs e)
        {
            LimparCamposAcessoVascular();
            tipo_operacaoAcessoVascular = Tipo_Operacao.Cadastro;
            btn_salvar_acessoVascular.IsEnabled = true;
            date_Data_realizacao.SelectedDate = DateTime.Now.Date;
        }

        private void txt_local_acessoVascular_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_local_acessoVascular.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_cirugiao_nefrologista_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_cirugiao_nefrologista.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_clinica_hospital_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_clinica_hospital.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void txt_director_clinico_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_director_clinico.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void date_Data_realizacao_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            date_Data_realizacao.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void date_dataFalencia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            date_dataFalencia.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
            if (!string.IsNullOrEmpty(date_dataFalencia.Text)) {
                txt_motivos_falencia.IsEnabled = true;
            }
            if (string.IsNullOrEmpty(date_dataFalencia.Text))
            {
                txt_motivos_falencia.IsEnabled = true;
            }
        }

        private void LimparCamposAcessoVascular()
        {
            this.cmb_Tipo_Acesso_Vascular.SelectedIndex = 0;
            date_Data_realizacao.Text = "";
            txt_recuperacao_Cirugica.Clear();
            txt_director_clinico.Clear();
            txt_director_clinico.Clear();
            txt_clinica_hospital.Clear();
            txt_complicacao.Clear(); ;
            date_dataFalencia.Text = "";
            txt_cirugiao_nefrologista.Clear(); ;
            txt_local_acessoVascular.Clear();
        }

        private void btn_salvar_acessoVascular_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposAcessoVascular())
            {
                if (tipo_operacaoAcessoVascular == Tipo_Operacao.Cadastro)
                {
                    AcessoVascular acessoVascular = new AcessoVascular();
                    acessoVascular.tipoAcesso = new TipoAcessoVascular(Convert.ToInt32(this.cmb_Tipo_Acesso_Vascular.SelectedValue.ToString()));
                    if (string.IsNullOrEmpty(date_Data_realizacao.Text)) {
                        date_Data_realizacao.SelectedDate = DateTime.Now.Date;
                    }
                    else
                    {
                        acessoVascular.Data_Realizacao = date_Data_realizacao.SelectedDate.Value;
                    }
                    acessoVascular.Recuperacao_cirugica = txt_recuperacao_Cirugica.Text;
                    acessoVascular.Director_clinico = txt_director_clinico.Text;
                    acessoVascular.Director_clinico = txt_director_clinico.Text;
                    acessoVascular.Clinica_hospital = txt_clinica_hospital.Text;
                    acessoVascular.Complicacao_av = txt_complicacao.Text;
                    acessoVascular.MotivoFalencia = txt_motivos_falencia.Text;
                    if (string.IsNullOrEmpty(date_dataFalencia.Text)) {   }
                    else
                    {
                        acessoVascular.Data_falencia = date_dataFalencia.SelectedDate.Value;
                    }
                    acessoVascular.Paciente_ = p;
                    acessoVascular.Cirugiao_nefrologista = txt_cirugiao_nefrologista.Text;
                    acessoVascular.Local_acesso = txt_local_acessoVascular.Text;
                    acessoVascularBLL.CadastrarAcessoVascular(acessoVascular);
                    CarregarListViewAcessoVascular();
                    btn_salvar_acessoVascular.IsEnabled = false;
                    MessageBox.Show("Acesso Vascular cadastrado com sucesso!!!", "Sucesso Cadastro", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else if (tipo_operacaoAcessoVascular == Tipo_Operacao.Actualizar)
                {
                    if (AcessoVascularListView.SelectedItems.Count > 0)
                    {
                        AcessoVascular acessoVascular = AcessoVascularListView.SelectedItem as AcessoVascular;
                        acessoVascular = AcessoVascularListView.SelectedItem as AcessoVascular;
                        this.cmb_Tipo_Acesso_Vascular.SelectedItem = AcessoVascularListView.Items[AcessoVascularListView.SelectedIndex];
                        if (string.IsNullOrEmpty(date_Data_realizacao.Text)) { }
                        else
                        {
                            acessoVascular.Data_Realizacao = date_Data_realizacao.SelectedDate.Value;
                        }
                        acessoVascular.Recuperacao_cirugica = txt_recuperacao_Cirugica.Text;
                        acessoVascular.Director_clinico = txt_director_clinico.Text;
                        acessoVascular.Director_clinico = txt_director_clinico.Text;
                        acessoVascular.Clinica_hospital = txt_clinica_hospital.Text;
                        acessoVascular.Complicacao_av = txt_complicacao.Text;
                        if (string.IsNullOrEmpty(date_dataFalencia.Text)) { }
                        else
                        {
                            acessoVascular.Data_falencia = date_dataFalencia.SelectedDate.Value;
                        }
                        acessoVascular.MotivoFalencia = txt_motivos_falencia.Text;
                        acessoVascular.Paciente_ = p;
                        acessoVascular.Cirugiao_nefrologista = txt_cirugiao_nefrologista.Text;
                        acessoVascular.Local_acesso = txt_local_acessoVascular.Text;
                        acessoVascularBLL.ActualizarAcessoVascular(acessoVascular);
                        CarregarListViewAcessoVascular();
                        btn_salvar_acessoVascular.IsEnabled = false;
                        MessageBox.Show("Acesso Vascular actualizado com sucesso!!!", "Sucesso Actualização", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                        //selectedProveniencia = listview_proveniencia.SelectedItem as Proveniencia;
                        //txt_nome_proveniencia.Text = selectedProveniencia.Nome_Proveniencia;
                        //txt_descricao_proveniencia.Text = selectedProveniencia.Descricao;
                        //lbl_notificacao.Content += tipo_Operacao_Proveniencia.ToString();
                        //this.tipo_Operacao_Proveniencia = EnumTipoOperacao_Manipulacao.Actualizar;

                    }
                    
                    
                }
            }
        }

        private void AcessoVascularListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AcessoVascular acessoVascular = AcessoVascularListView.SelectedItem as AcessoVascular;
           // MessageBox.Show(acessoVascular.ID_AcessoVascular+"");
            if (AcessoVascularListView.SelectedItems.Count > 0)
            {

                this.eliminar_acesso_vascular.IsEnabled = true;
                this.alterar_Acesso_Vascular.IsEnabled = true;
                tab_acesso_vasc_imagens.IsEnabled = true;
            }
            else
            {
                this.eliminar_acesso_vascular.IsEnabled = false;
                this.alterar_Acesso_Vascular.IsEnabled = false;
                tab_acesso_vasc_imagens.IsEnabled = false;
            }
        }

        private void eliminar_acesso_vascular_Click(object sender, RoutedEventArgs e)
        {
            if (AcessoVascularListView.SelectedItems.Count > 0)
            {
                AcessoVascular acessoVascular = AcessoVascularListView.SelectedItem as AcessoVascular;
                
                var blur = new BlurEffect();
                blur.Radius = 8;
                var current = this.Background;
                this.Background = new SolidColorBrush(Colors.White);
                this.Effect = blur;
                if (MessageBox.Show($"Tem a Certeza que pretende eliminar o registo Acesso Vascular: {acessoVascular.tipoAcesso.ToString()} - {acessoVascular.Data_Realizacao.ToShortDateString()} ?", "Eliminar Acesso Vascular", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
                {
                    acessoVascularBLL.EliminarAcessoVascular(acessoVascular);
                    CarregarListViewAcessoVascular();
                }
                this.Effect = null;
                this.Background = current;
                
            }
        }

        private void alterar_Acesso_Vascular_Click(object sender, RoutedEventArgs e)
        {
            if (AcessoVascularListView.SelectedItems.Count > 0)
            {
            AcessoVascular acessoVascular = AcessoVascularListView.SelectedItem as AcessoVascular;
            acessoVascular = AcessoVascularListView.SelectedItem as AcessoVascular;
            this.cmb_Tipo_Acesso_Vascular.SelectedValue = acessoVascular.tipoAcesso.Id_tipo_acesso;
            date_Data_realizacao.Text = acessoVascular.Data_Realizacao.ToShortDateString();
            txt_recuperacao_Cirugica.Text = acessoVascular.Recuperacao_cirugica;
            txt_director_clinico.Text = acessoVascular.Director_clinico;
            txt_director_clinico.Text = acessoVascular.Director_clinico;
            txt_clinica_hospital.Text = acessoVascular.Clinica_hospital;
            txt_complicacao.Text = acessoVascular.Complicacao_av;
            txt_motivos_falencia.Text = acessoVascular.MotivoFalencia;
                txt_cirugiao_nefrologista.Text = acessoVascular.Cirugiao_nefrologista;
             txt_local_acessoVascular.Text = acessoVascular.Local_acesso;
            date_dataFalencia.Text = acessoVascular.Data_falencia.ToShortDateString();
            acessoVascular.Cirugiao_nefrologista = txt_cirugiao_nefrologista.Text;
            acessoVascular.Local_acesso = txt_local_acessoVascular.Text;
                this.tipo_operacaoAcessoVascular = Tipo_Operacao.Actualizar;
                btn_salvar_acessoVascular.IsEnabled = true;
            }
        }

        private void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(Date_Data_inicial.Text) && string.IsNullOrEmpty(Date_Data_final.Text)))
            {
                //CarregarListViewAcessoVascular();
                AcessoVascularListView.ItemsSource = null;
                if (string.IsNullOrEmpty(Date_Data_inicial.Text) )
                {
                    Date_Data_inicial.Text = DateTime.Now.ToShortDateString();
                    
                }

                if (string.IsNullOrEmpty(Date_Data_final.Text))
                {
                    Date_Data_final.Text = DateTime.Now.ToShortDateString();

                }
                AcessoVascularListView.ItemsSource =  acessoVascularBLL.ConsultarAcessoVascularPelaData(Date_Data_inicial.SelectedDate.Value, Date_Data_final.SelectedDate.Value,p.Id_pessoa);
            }
            else
            {
            if(string.IsNullOrEmpty(Date_Data_inicial.Text.Trim()))
                {
                Date_Data_inicial.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                }
            if (string.IsNullOrEmpty(Date_Data_final.Text.Trim()))
                {
                    Date_Data_final.BorderBrush = new SolidColorBrush(Color.FromRgb(252, 31, 31));
                }
              MessageBox.Show("1. O(s) campos destacados não podem estar vazios, por favor selecione a data inicial e a data final!!!", "Verificar os Campos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void listar_acesso_vascular_Click(object sender, RoutedEventArgs e)
        {
            CarregarListViewAcessoVascular();
        }

        private void Date_Data_inicial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Date_Data_inicial.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void Date_Data_final_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Date_Data_final.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        #region Codigos Referentes a Prescrição

        public void ListarPrescriçãoPaciente()
        {
            datagrid_prescricao.ItemsSource = prescricaoBLL.ListarPrescricao(this.p);
        }


        
        private void datagrid_prescricao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid_prescricao.SelectedItems.Count > 0)
            {
                btn_Eliminar_Prescricao.IsEnabled = true;
                btn_Editar_Prescricao.IsEnabled = true;
            }
            else
            {
                btn_Eliminar_Prescricao.IsEnabled = true;
                btn_Editar_Prescricao.IsEnabled = true;
            }
        }

        #endregion

        private void btn_Editar_Prescricao_Click(object sender, RoutedEventArgs e)
        {
            //prescricricao_UserControl.
            Prescricao prescricao = datagrid_prescricao.SelectedItem as Prescricao;
            prescricao_UserControl.txt_peso_seco.Text =  prescricao.peso_seco;
            prescricao_UserControl.txt_uf_total_maxima.Text = prescricao.uf_total_max;
            prescricao_UserControl.txt_ektv.Text = prescricao.ektv_prescrito;
            prescricao_UserControl.txt_nr_sessao_semana.Text = prescricao.nr_sessao_semana;
            prescricao_UserControl.txt_nr_horas_sessao.Text = prescricao.nr_hora_sessao;
            prescricao_UserControl.txt_temperatura.Text = prescricao.temperatura;
            prescricao_UserControl.txt_debito_sangue.Text = prescricao.debito;
            prescricao_UserControl.txt_glucose.Text = prescricao.glucose;
            prescricao_UserControl.date_DataPrescricao.Text = prescricao.data_prescricao.ToShortDateString();
            prescricao_UserControl.txt_tecnica_hd.Text = prescricao.tipo_tecnica;
            //Anticoagulação
            prescricao_UserControl.txt_heparina_hora.Text =  prescricao.heparina_hora;
            prescricao_UserControl.txt_heparina_inicial.Text = prescricao.heparina_inicial;
            prescricao_UserControl.txt_interrupcao_heparina.Text = prescricao.interrupcao_heparina;
            prescricao_UserControl.txt_heparina_bpm.Text = prescricao.heparina_bpm;
            prescricao_UserControl.paciente = this.p;
            //Prescrição - Escala
            prescricao_UserControl.cmb_Nome_Escala.SelectedItem = prescricao.idescala;
            //Prescrição Sal Mineral
            prescricao_UserControl.list_Prescricao_Sal_Mineral = prescricaoBLL.ObterListPrescricao_Sal_Mineral(prescricao);//
            prescricao_UserControl.CarregarPrescricaoSalMineralListview();
            //Prescricao Material
            prescricao_UserControl.listPrescricao_Material = prescricaoBLL.ObterListPrescricao_Material(prescricao); //
            prescricao_UserControl.CarregarListBoxPrescricaoMaterial();
            //Prescrição - Terapeutica --- Medicamento
            prescricao_UserControl.listPrescricao_Medicamento = prescricaoBLL.ObterListPrescricao_Medicamento(prescricao);
            prescricao_UserControl.CarregarListViewPrescricaoMedicamento();

            prescricao_UserControl.tipoOperacao_Manipulacao = EnumTipoOperacao_Manipulacao.Actualizar;
            prescricao_UserControl.prescricao.id_prescricao_dialise = prescricao.id_prescricao_dialise;

            main_tab_prescricao.SelectedIndex = 0;

            //prescricricao_UserControl.txt_temperatura.Text = "500";
        }

        private void btn_Eliminar_Prescricao_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid_prescricao.SelectedItems.Count > 0)
            {

                var blur = new BlurEffect();
                blur.Radius = 8;
                var current = this.Background;
                this.Background = new SolidColorBrush(Colors.White);
                this.Effect = blur;
                Prescricao prescricaox = datagrid_prescricao.SelectedItem as Prescricao;
                prescricaox.paciente = this.p;
                if (MessageBox.Show($"Tem a Certeza que pretende eliminar todos os dados referente a prescrição: {prescricaox.id_prescricao_dialise} do Paciente {prescricaox.paciente.Nome} ?", "Eliminar Prescrição", MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
                {
                    try
                    {
                        if (prescricaoBLL.EliminarPrescricao(prescricaox))
                        {
                            ListarPrescriçãoPaciente();
                           MessageBox.Show($"Prescrição {prescricaox.id_prescricao_dialise} eliminado com sucesso!!!", "Sucesso Eliminar Prescrição", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Não foi possivel eliminar a prescrição {prescricaox.id_prescricao_dialise.ToString()} !!!", "Erro Eliminar Prescrição", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                this.Effect = null;
                this.Background = current;
            }


        }

       
    }


}
