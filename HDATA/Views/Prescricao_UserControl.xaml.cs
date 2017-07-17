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
using CamadaNegocio;
using CamadaObjectoTransferencia;
using HDATA.Views;

namespace HDATA
{
    /// <summary>
    /// Interaction logic for Prescricao_UserControl.xaml
    /// </summary>
    public partial class Prescricao_UserControl : UserControl
    {

        MaterialBLL materialBLL;
        public Paciente paciente { get; set; }
        public Prescricao prescricao;
        MedicamentoBLL medicamentoBLL;
        EscalaBLL escalaBLL;
        DiaSemanaBLL diaSemanaBLL;
        Sal_MineralBLL sal_MineralBLL;
        PrescricaoBLL prescricaoBLL;
        public List<Prescricao_Sal_Mineral> list_Prescricao_Sal_Mineral { get; set; }
        public List<Prescricao_Material> listPrescricao_Material { get; set; }
        public List<Prescricao_Medicamento> listPrescricao_Medicamento { get; set; }

        public EnumTipoOperacao_Manipulacao tipoOperacao_Manipulacao;
        private usc_cadastro_paciente cadastro_Paciente;

        public Prescricao_UserControl(Paciente paciente, EnumTipoOperacao_Manipulacao tipo_operacao)
        {
            InitializeComponent();
            materialBLL = new MaterialBLL();
            this.paciente = paciente;
            this.medicamentoBLL = new MedicamentoBLL();
            this.escalaBLL = new EscalaBLL();
            this.diaSemanaBLL = new DiaSemanaBLL();
            this.sal_MineralBLL = new Sal_MineralBLL();
            this.prescricaoBLL = new PrescricaoBLL();
            this.tipoOperacao_Manipulacao = tipo_operacao;
            CarregarDadosPadrao();
            prescricao = new Prescricao();
            list_Prescricao_Sal_Mineral = new List<Prescricao_Sal_Mineral>();
            listPrescricao_Material = new List<Prescricao_Material>();
            listPrescricao_Medicamento = new List<Prescricao_Medicamento>();

        }

        public Prescricao_UserControl()
        {
            InitializeComponent();
            materialBLL = new MaterialBLL();
            this.escalaBLL = new EscalaBLL();
            this.medicamentoBLL = new MedicamentoBLL();
            this.diaSemanaBLL = new DiaSemanaBLL();
            this.sal_MineralBLL = new Sal_MineralBLL();
            CarregarDadosPadrao();
            this.prescricaoBLL = new PrescricaoBLL();
            prescricao = new Prescricao();
            list_Prescricao_Sal_Mineral = new List<Prescricao_Sal_Mineral>();
            listPrescricao_Material = new List<Prescricao_Material>();
            listPrescricao_Medicamento = new List<Prescricao_Medicamento>();
        }

        public Prescricao_UserControl(Paciente paciente, EnumTipoOperacao_Manipulacao tipo_operacao, usc_cadastro_paciente cadastro_Paciente)
        {
            this.cadastro_Paciente = cadastro_Paciente;
            InitializeComponent();
            materialBLL = new MaterialBLL();
            this.paciente = paciente;
            this.medicamentoBLL = new MedicamentoBLL();
            this.escalaBLL = new EscalaBLL();
            this.diaSemanaBLL = new DiaSemanaBLL();
            this.sal_MineralBLL = new Sal_MineralBLL();
            this.prescricaoBLL = new PrescricaoBLL();
            this.tipoOperacao_Manipulacao = tipo_operacao;
            CarregarDadosPadrao();
            prescricao = new Prescricao();
            list_Prescricao_Sal_Mineral = new List<Prescricao_Sal_Mineral>();
            listPrescricao_Material = new List<Prescricao_Material>();
            listPrescricao_Medicamento = new List<Prescricao_Medicamento>();
        }

        private void CarregarDadosPadrao()
        {
            CarregarListboxMaterial();
            CarregarComboboxEscala();
            CarregarListboxMedicamentos();
            CarregarListBoxSalMineral();
            date_DataPrescricao.SelectedDate = DateTime.Now.Date;
        }

        #region Codigo Padrão -  Controles

        private void CarregarListboxMaterial()
        {
            this.Material_Todas_listBox.DisplayMemberPath = "nome_material";
            this.Material_Todas_listBox.SelectedValuePath = "id_material";
            this.Material_Todas_listBox.ItemsSource = materialBLL.ListarMateriais();
            
        }

        private void CarregarListboxMedicamentos()
        {
            
            this.Terapeutica_listBox.DisplayMemberPath = "nome";
            this.Terapeutica_listBox.SelectedValuePath = "id_medicamento";
            this.Terapeutica_listBox.ItemsSource = medicamentoBLL.ListarMedicamentos();

        }

        private void CarregarListBoxSalMineral()
        {

            this.Sal_Mineral_listBox.DisplayMemberPath = "nome";
            this.Sal_Mineral_listBox.SelectedValuePath = "id_sal_mineral";
            this.Sal_Mineral_listBox.ItemsSource = sal_MineralBLL.ListarSal_Mineral();
        }

        private void CarregarComboboxEscala()
        {
            
            cmb_Nome_Escala.ItemsSource = escalaBLL.ListarEscala();
            cmb_Nome_Escala.DisplayMemberPath = "nome_escala";
            cmb_Nome_Escala.SelectedValuePath = "idescala";
            cmb_Nome_Escala.SelectedIndex = 0;
        }

        private void GerarDiasTratamento()
        {
            Calendario_Escala.SelectionMode = CalendarSelectionMode.MultipleRange;
            SelectedDatesCollection selectedDates = new SelectedDatesCollection(Calendario_Escala);
            Calendario_Escala.SelectedDates.Clear();
            
            selectedDates.AddRange(DateTime.Now, DateTime.Now.AddMonths(1));
            List<DateTime> DatasDeTratamento = new List<DateTime>();

            for (int i = 0; i < selectedDates.Count; i++)
            {
                DateTime data = selectedDates[i];
                //MessageBox.Show(selectedDates[i].DayOfWeek.ToString());
                if (data.DayOfWeek != DayOfWeek.Sunday)
                {
                if (data.DayOfWeek.ToString().Equals(DayOfWeek.Monday.ToString()) && SegundaFeira.IsChecked == true)
                {
                    Calendario_Escala.SelectedDates.Add(data);
                    DatasDeTratamento.Add(selectedDates[i]);

                }else if (selectedDates[i].DayOfWeek.ToString().Equals(DayOfWeek.Tuesday.ToString()) && TercaFeira.IsChecked == true)
                {
                    Calendario_Escala.SelectedDates.Add(selectedDates[i]);
                    DatasDeTratamento.Add(selectedDates[i]);
                } else if (selectedDates[i].DayOfWeek.Equals(DayOfWeek.Wednesday) && QuartaFeira.IsChecked == true)
                {
                    Calendario_Escala.SelectedDates.Add(selectedDates[i]);
                    DatasDeTratamento.Add(selectedDates[i]);
                } else if (selectedDates[i].DayOfWeek.Equals(DayOfWeek.Thursday) && QuintaFeira.IsChecked == true)
                {
                    Calendario_Escala.SelectedDates.Add(selectedDates[i]);
                    DatasDeTratamento.Add(selectedDates[i]);
                } else if (selectedDates[i].DayOfWeek.Equals(DayOfWeek.Friday) && SextaFeira.IsChecked == true)
                {
                    Calendario_Escala.SelectedDates.Add(selectedDates[i]);
                    DatasDeTratamento.Add(selectedDates[i]);
                } else if (selectedDates[i].DayOfWeek.Equals(DayOfWeek.Saturday) && Sabado.IsChecked == true)
                {
                    Calendario_Escala.SelectedDates.Add(selectedDates[i]);
                    DatasDeTratamento.Add(selectedDates[i]);
                }
            }
            }

            txt_dias_tratamento.Text = "";
            Label_DiasDefinidos.Content = "Dias de Tratamento: ";
            int linha = 0;
            for (int i = 0; i < DatasDeTratamento.Count; i++)
            {
                txt_dias_tratamento.Text += DatasDeTratamento[i].ToString("d");
                if (i + 1 != DatasDeTratamento.Count)
                {
                    txt_dias_tratamento.Text += " - ";
                }
                linha += 1;
                if (linha == 5)
                {
                    txt_dias_tratamento.Text += "\n";
                }
            }
            Label_TotalDiasDefinidos.Content = "Total Dias: " + DatasDeTratamento.Count;
        }

        private void Sal_Mineral_padrao_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            
            List<Sal_Mineral> sal_mineral_padrao = sal_MineralBLL.ListarSal_Mineral();
            list_Prescricao_Sal_Mineral = new List<Prescricao_Sal_Mineral>();
            foreach (Sal_Mineral item in sal_mineral_padrao)
            {
                if (item.tipo_uso.Equals("padrao"))
                {
                    Prescricao_Sal_Mineral prescr_sal = new Prescricao_Sal_Mineral();
                    prescr_sal.sal_Mineral  = item;
                    prescr_sal.valor_prescrito = item.valor_padrao;
                    list_Prescricao_Sal_Mineral.Add(prescr_sal);
                }
         }

            CarregarPrescricaoSalMineralListview();
            //  txt_procurar_sal_Mineral.IsEnabled = false;
            //Sal_Mineral_listBox.IsEnabled = false;
            //txt_valor_prescrito_Sal_Mineral.IsEnabled = false;
            // btn_Adicionar_Sal_Mineral.IsEnabled = false;
            // btn_Remover_Sal_Mineral.IsEnabled = false;
        }

        private void Sal_Mineral_padrao_checkBox_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Sal_Mineral_padrao_checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            txt_procurar_sal_Mineral.IsEnabled = true;
            Sal_Mineral_listBox.IsEnabled = true;
            txt_valor_prescrito_Sal_Mineral.IsEnabled = true;
           // btn_Adicionar_Sal_Mineral.IsEnabled = true;
            //btn_Remover_Sal_Mineral.IsEnabled = true;
            Sal_Mineral_listView.IsEnabled = true;
        }

        private void btn_gerar_dias_Tratamento_Click(object sender, RoutedEventArgs e)
        {
                GerarDiasTratamento();
                //Calendario_Escala.SelectedDates.AddRange(DateTime.Now, DateTime.Now.AddMonths(1));
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Nome_Escala.Items.Count > 0 && cmb_Nome_Escala.SelectedItem != null)
            {
                DeSeleccionarCheckBoxDiaSemana();

                Escala escala = cmb_Nome_Escala.SelectedItem as Escala;
                VerificarDiaSemana(diaSemanaBLL.ConsultarDiasSemanaDaEscala(escala));
            }
        }

        public void DeSeleccionarCheckBoxDiaSemana()
        {
                this.SegundaFeira.IsChecked =false;
                this.TercaFeira.IsChecked = false;
                this.QuartaFeira.IsChecked = false;
                this.QuintaFeira.IsChecked = false;
                this.SextaFeira.IsChecked = false;
                this.Sabado.IsChecked = false;
        }
              
        public void VerificarDiaSemana( List<DiaSemana> ListaDiaSemana)
        {
            foreach ( DiaSemana item in ListaDiaSemana)
            {
                if (item.Abrev_DiaSemana.ToString().Equals(SegundaFeira.Content.ToString()))
                {
                    this.SegundaFeira.IsChecked = true;
                }
                else if (item.Abrev_DiaSemana.ToString().Equals(TercaFeira.Content.ToString()))
                {
                    this.TercaFeira.IsChecked = true;
                }
                else if (item.Abrev_DiaSemana.ToString().Equals(QuartaFeira.Content.ToString()))
                {
                    this.QuartaFeira.IsChecked = true;
                }
                else if (item.Abrev_DiaSemana.ToString().Equals(QuintaFeira.Content.ToString()))
                {
                    this.QuintaFeira.IsChecked = true;
                }
                else if (item.Abrev_DiaSemana.ToString().Equals(SextaFeira.Content.ToString()))
                {
                    this.SextaFeira.IsChecked = true;
                }
                else if (item.Abrev_DiaSemana.ToString().Equals(Sabado.Content.ToString()))
                {
                    this.Sabado.IsChecked = true;
                }
            }
        }

        private void Sal_Mineral_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sal_Mineral_listBox.Items.Count > 0 && Sal_Mineral_listBox.SelectedItems.Count > 0 && txt_valor_prescrito_Sal_Mineral.Text.Length > 0)
            {
                btn_Adicionar_Sal_Mineral.IsEnabled = true;
            }
            else
            {
                btn_Adicionar_Sal_Mineral.IsEnabled = false;
            }
        }

        private void txt_valor_prescrito_Sal_Mineral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_valor_prescrito_Sal_Mineral.Text.Length > 0)
            {
                btn_Adicionar_Sal_Mineral.IsEnabled = true;
            }
            else
            {
                btn_Adicionar_Sal_Mineral.IsEnabled = false;
            }
        }

        private void Sal_Mineral_listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sal_Mineral_listView.Items.Count > 0 && Sal_Mineral_listView.SelectedItems.Count > 0)
            {
                btn_Remover_Sal_Mineral.IsEnabled = true;
              
            }
            else
            {
                btn_Remover_Sal_Mineral.IsEnabled = false;
            }
        }

        private void Listar_Prescricao_Sal_Mineral(List<Prescricao_Sal_Mineral> list_prescricao_sal_Mineral)
        {

        }

        private void btn_Adicionar_Sal_Mineral_Click(object sender, RoutedEventArgs e)
        {
            if (Sal_Mineral_listBox.SelectedItems.Count > 0)
            {
            Prescricao_Sal_Mineral prescricao_SalMineral = new Prescricao_Sal_Mineral();
            prescricao_SalMineral.sal_Mineral =  Sal_Mineral_listBox.SelectedItem as Sal_Mineral;
            prescricao_SalMineral.valor_prescrito = txt_valor_prescrito_Sal_Mineral.Text;
                txt_valor_prescrito_Sal_Mineral.Clear();
                bool cadastro_ = true;
            foreach (Prescricao_Sal_Mineral item in list_Prescricao_Sal_Mineral)
            {
                if (item.sal_Mineral.id_sal_mineral == prescricao_SalMineral.sal_Mineral.id_sal_mineral)
                {
                    cadastro_ = false;
                }
            }
            if (cadastro_)
            {
                list_Prescricao_Sal_Mineral.Add(prescricao_SalMineral);
                CarregarPrescricaoSalMineralListview();
            }
            btn_Adicionar_Sal_Mineral.IsEnabled = false;
            }
        }

        public void CarregarPrescricaoSalMineralListview()
        {
            Sal_Mineral_listView.ItemsSource = null;
            Sal_Mineral_listView.ItemsSource = list_Prescricao_Sal_Mineral;
        }

        private void btn_Remover_Sal_Mineral_Click(object sender, RoutedEventArgs e)
        {
            btn_Remover_Sal_Mineral.IsEnabled = false;
            
            if (Sal_Mineral_listView.SelectedItems.Count > 0)
            {
                list_Prescricao_Sal_Mineral.Remove(Sal_Mineral_listView.SelectedItem as Prescricao_Sal_Mineral);
                CarregarPrescricaoSalMineralListview();
            }
        }

        private void txt_procurar_sal_Mineral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_procurar_sal_Mineral.Text.Length > 0)
            {
                this.Sal_Mineral_listBox.DisplayMemberPath = "nome";
                this.Sal_Mineral_listBox.SelectedValuePath = "id_sal_mineral";
                this.Sal_Mineral_listBox.ItemsSource = sal_MineralBLL.ConsultarSalMineralPeloNome(txt_procurar_sal_Mineral.Text);
            }
            else
            {
                CarregarListBoxSalMineral();
            }
        }

        private void txt_procurar_terapia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_procurar_terapia.Text.Length > 0)
            {
                this.Terapeutica_listBox.DisplayMemberPath = "nome";
                this.Terapeutica_listBox.SelectedValuePath = "id_medicamento";
                this.Terapeutica_listBox.ItemsSource = medicamentoBLL.ConsultarMedicamentoPeloNome(txt_procurar_terapia.Text);
            }
            else
            {
                CarregarListboxMedicamentos();
            }
        }

        private void txt_procurar_Material_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_procurar_Material.Text.Length > 0)
            {
                this.Material_Todas_listBox.DisplayMemberPath = "nome_material";
                this.Material_Todas_listBox.SelectedValuePath = "id_material";
                this.Material_Todas_listBox.ItemsSource = materialBLL.ConsultarMaterialPeloNome(txt_procurar_Material.Text);
            }
            else
            {
                CarregarListboxMaterial();
            }
        }

        private void btn_Adicionar_Material_Click(object sender, RoutedEventArgs e)
        {
            Prescricao_Material prescricao_Material = new Prescricao_Material();
            prescricao_Material.id_material = Material_Todas_listBox.SelectedItem as Material;
            bool cadastro_ = true;
            foreach (Prescricao_Material item in listPrescricao_Material)
            {
                if (item.id_material == prescricao_Material.id_material || item.id_material.tipo_material == prescricao_Material.id_material.tipo_material)
                {
                    cadastro_ = false;
                    break;
                }
            }
            if (cadastro_)
            {
                listPrescricao_Material.Add(prescricao_Material);
                CarregarListBoxPrescricaoMaterial();
            }
            btn_Adicionar_Sal_Mineral.IsEnabled = false;

        }

        private void btn_Remover_Material_Click(object sender, RoutedEventArgs e)
        {

            if (Material_Prescricao_listBox.SelectedItems.Count > 0)
            {
                listPrescricao_Material.Remove(Material_Prescricao_listBox.SelectedItem as Prescricao_Material);
                CarregarListBoxPrescricaoMaterial();
            }

            btn_Remover_Material.IsEnabled = false;
        }

        public void CarregarListBoxPrescricaoMaterial()
        {
            this.Material_Prescricao_listBox.DisplayMemberPath = "id_material.nome_material";
            this.Material_Prescricao_listBox.SelectedValuePath = "id_material.id_material";
            Material_Prescricao_listBox.ItemsSource = null;
            Material_Prescricao_listBox.ItemsSource = listPrescricao_Material;
        }

        private void Material_Todas_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Material_Todas_listBox.Items.Count > 0 && Material_Todas_listBox.SelectedItems.Count > 0)
            {
                btn_Adicionar_Material.IsEnabled = true;
            }
            else
            {
                btn_Adicionar_Material.IsEnabled = false;
            }
        }

        private void Material_Prescricao_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Material_Prescricao_listBox.Items.Count > 0 && Material_Prescricao_listBox.SelectedItems.Count > 0)
            {
                btn_Remover_Material.IsEnabled = true;
            }
            else
            {
                btn_Remover_Material.IsEnabled = false;
            }
        }

        private void Terapeutica_listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Terapeutica_listBox.Items.Count > 0 && Terapeutica_listBox.SelectedItems.Count > 0 && txt_valor_prescrito_Terapeutica.Text.Length > 0)
            {
                btn_Adicionar_Terapeutica.IsEnabled = true;
            }
            else
            {
                btn_Adicionar_Terapeutica.IsEnabled = false;
            }
        }

        private void btn_Adicionar_Terapeutica_Click(object sender, RoutedEventArgs e)
        {
            if (Terapeutica_listBox.SelectedItems.Count > 0)
            {
            Prescricao_Medicamento prescricao_Medicamento= new Prescricao_Medicamento();
            prescricao_Medicamento.id_medicamento = Terapeutica_listBox.SelectedItem as Medicamento;
            prescricao_Medicamento.valor_prescrito = txt_valor_prescrito_Terapeutica.Text;
                txt_valor_prescrito_Terapeutica.Clear();
            bool cadastro_ = true;
            foreach (Prescricao_Medicamento item in listPrescricao_Medicamento)
            {
                if (item.id_medicamento == prescricao_Medicamento.id_medicamento)
                {
                    cadastro_ = false;
                    break;
                }
            }
            if (cadastro_)
            {
                listPrescricao_Medicamento.Add(prescricao_Medicamento);
                CarregarListViewPrescricaoMedicamento();
            }
            btn_Adicionar_Terapeutica.IsEnabled = false;
            }
        }

        public void CarregarListViewPrescricaoMedicamento()
        {
            this.Terapeutica_listView.ItemsSource = null;
            this.Terapeutica_listView.ItemsSource = listPrescricao_Medicamento;
        }

        private void btn_Remover_Terapeutica_Click(object sender, RoutedEventArgs e)
        {
            
           
            if (Terapeutica_listView.SelectedItems.Count > 0)
            {
                listPrescricao_Medicamento.Remove(Terapeutica_listView.SelectedItem as Prescricao_Medicamento);
                CarregarListViewPrescricaoMedicamento();
            }

            btn_Remover_Terapeutica.IsEnabled = false;
        }

        private void Terapeutica_listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Terapeutica_listView.Items.Count > 0 && Terapeutica_listView.SelectedItems.Count > 0)
            {
                btn_Remover_Terapeutica.IsEnabled = true;
            }
            else
            {
                btn_Remover_Terapeutica.IsEnabled = false;
            }
        }

        private void txt_valor_prescrito_Terapeutica_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_valor_prescrito_Terapeutica.Text.Length > 0)
            {
                btn_Adicionar_Terapeutica.IsEnabled = true;
            }
            else
            {
                btn_Adicionar_Terapeutica.IsEnabled = false;
            }
        }

        #endregion

        private void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                if (tipoOperacao_Manipulacao.Equals(EnumTipoOperacao_Manipulacao.Cadastrar))
                {
                    prescricao = new Prescricao();
                    prescricao.peso_seco = txt_peso_seco.Text;
                    prescricao.uf_total_max = txt_uf_total_maxima.Text;
                    prescricao.ektv_prescrito = txt_ektv.Text;
                    prescricao.nr_sessao_semana = txt_nr_sessao_semana.Text;
                    prescricao.nr_hora_sessao = txt_nr_horas_sessao.Text;
                    prescricao.temperatura = txt_temperatura.Text;
                    prescricao.debito = txt_debito_sangue.Text;
                    prescricao.glucose = txt_glucose.Text;
                    prescricao.data_prescricao = date_DataPrescricao.SelectedDate.Value;
                    prescricao.tipo_tecnica = txt_tecnica_hd.Text;
                    //Anticoagulação
                    prescricao.heparina_hora = txt_heparina_hora.Text;
                    prescricao.heparina_inicial = txt_heparina_inicial.Text;
                    prescricao.interrupcao_heparina = txt_interrupcao_heparina.Text;
                    prescricao.heparina_bpm = txt_heparina_bpm.Text;
                    prescricao.paciente = paciente;
                    //Prescrição - Escala
                    Escala escala_ = new Escala();
                    escala_ = cmb_Nome_Escala.SelectedItem as Escala;
                    prescricao.idescala = escala_;
                    //  List<Escala_DiaSemana> List_Escala_DiaSemana = new List<Escala_DiaSemana>();

                    // Escala DiaSemana - Prescrição
                    prescricaoBLL = new CamadaNegocio.PrescricaoBLL();
                   prescricao.id_prescricao_dialise = prescricaoBLL.CadastrarPrescricao(prescricao);

                    //Prescrição Sal Mineral
                    Prescricao_Sal_Mineral_BLL prescricao_Sal_Mineral_BLL = new Prescricao_Sal_Mineral_BLL();
                    prescricao_Sal_Mineral_BLL.Cadastrar_Prescricao_Sal_Mineral(list_Prescricao_Sal_Mineral,prescricao);

                    //Prescrição - Terapeutica --- Medicamento
                    Prescricao_Medicamento_BLL prescricao_Medicamento_BLL = new Prescricao_Medicamento_BLL();
                    prescricao_Medicamento_BLL.Cadastrar_Prescricao_Medicamento(listPrescricao_Medicamento,prescricao);

                    //Prescricao Material
                    Prescricao_Material_BLL prescricao_Material_BLL = new Prescricao_Material_BLL();
                    prescricao_Material_BLL.Cadastrar_PrescricaoMaterial(listPrescricao_Material,prescricao);

                    cadastro_Paciente.ListarPrescriçãoPaciente();
                    
                    MessageBox.Show("Prescrição do Paciente " + paciente.Nome + " Cadastrada com sucesso!!!", "Sucesso Cadastro", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);


                }

                if (tipoOperacao_Manipulacao.Equals(EnumTipoOperacao_Manipulacao.Actualizar))
                {

                    //prescricao = new Prescricao();
                    prescricao.peso_seco = txt_peso_seco.Text;
                    prescricao.uf_total_max = txt_uf_total_maxima.Text;
                    prescricao.ektv_prescrito = txt_ektv.Text;
                    prescricao.nr_sessao_semana = txt_nr_sessao_semana.Text;
                    prescricao.nr_hora_sessao = txt_nr_horas_sessao.Text;
                    prescricao.temperatura = txt_temperatura.Text;
                    prescricao.debito = txt_debito_sangue.Text;
                    prescricao.glucose = txt_glucose.Text;
                    prescricao.data_prescricao = date_DataPrescricao.SelectedDate.Value;
                    prescricao.tipo_tecnica = txt_tecnica_hd.Text;
                    //Anticoagulação
                    prescricao.heparina_hora = txt_heparina_hora.Text;
                    prescricao.heparina_inicial = txt_heparina_inicial.Text;
                    prescricao.interrupcao_heparina = txt_interrupcao_heparina.Text;
                    prescricao.heparina_bpm = txt_heparina_bpm.Text;
                    prescricao.paciente = paciente;
                    //Prescrição - Escala
                    Escala escala_ = new Escala();
                    escala_ = cmb_Nome_Escala.SelectedItem as Escala;
                    prescricao.idescala = escala_;
                    //  List<Escala_DiaSemana> List_Escala_DiaSemana = new List<Escala_DiaSemana>();

                    // Escala DiaSemana - Prescrição
                    prescricaoBLL = new CamadaNegocio.PrescricaoBLL();
                    prescricao.id_prescricao_dialise = prescricaoBLL.ActualizarPrescricao(prescricao);

                    //Prescrição Sal Mineral
                    Prescricao_Sal_Mineral_BLL prescricao_Sal_Mineral_BLL = new Prescricao_Sal_Mineral_BLL();
                    prescricao_Sal_Mineral_BLL.Actualizar_Prescricao_Sal_Mineral(list_Prescricao_Sal_Mineral);

                    //Prescrição - Terapeutica --- Medicamento
                    Prescricao_Medicamento_BLL prescricao_Medicamento_BLL = new Prescricao_Medicamento_BLL();
                    prescricao_Medicamento_BLL.Actualizar_Prescricao_Medicamento(listPrescricao_Medicamento);

                    //Prescricao Material
                    Prescricao_Material_BLL prescricao_Material_BLL = new Prescricao_Material_BLL();
                    prescricao_Material_BLL.Actualizar_PrescricaoMaterial(listPrescricao_Material);

                    cadastro_Paciente.ListarPrescriçãoPaciente();

                    MessageBox.Show("Prescrição do Paciente " + paciente.Nome + " Actualizada com sucesso!!!", "Sucesso Actualização", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }

            }
        }

        private bool Validar()
        {
            return true;
        }

        private void Sal_Mineral_listView_LostFocus(object sender, RoutedEventArgs e)
        {
            Sal_Mineral_listView.UnselectAll();
        }

        private void Sal_Mineral_listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Prescricao_Sal_Mineral intem = Sal_Mineral_listView.SelectedItem as Prescricao_Sal_Mineral;
            txt_valor_prescrito_Sal_Mineral.Text = intem.valor_prescrito;
            txt_procurar_sal_Mineral.Text = intem.sal_Mineral.nome;
            list_Prescricao_Sal_Mineral.Remove(intem);
            CarregarPrescricaoSalMineralListview();
            Sal_Mineral_listBox.SelectedIndex = 0;
        }

        private void btn_novo_Click(object sender, RoutedEventArgs e)
        {
            tipoOperacao_Manipulacao = EnumTipoOperacao_Manipulacao.Cadastrar;
        }
    }
}
