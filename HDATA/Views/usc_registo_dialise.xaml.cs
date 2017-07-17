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
    /// Interaction logic for usc_registo_dialise.xaml
    /// </summary>
    public partial class usc_registo_dialise : UserControl
    {
        Paciente paciente;
        Prescricao prescricao;
        RegistoDialise registo_Dialise;

        public usc_registo_dialise()
        {
            InitializeComponent();
        }

        public usc_registo_dialise(Paciente p,Prescricao prescricao)
        {
            InitializeComponent();
            this.paciente = p;
            this.prescricao = prescricao;
        }
        private void rb_ausente_Unchecked(object sender, RoutedEventArgs e)
        {
            
            this.Background = new SolidColorBrush(new Color());
        }

        private void rb_ausente_Checked(object sender, RoutedEventArgs e)
        {
            //DESACTIVAR CONTROLES
            cmb_Sala.IsEnabled = false;
            cmb_Turno.IsEnabled = false;
            date_Data_Registo_Dialise.IsEnabled = false;
            txt_peso_entrada.IsEnabled = false;
            txt_peso_seco.IsEnabled = false;
            txt_peso_saida.IsEnabled = false;
            txt_objetivo_uf.IsEnabled = false;
            txt_primming.IsEnabled = false;
            txt_alimentacao.IsEnabled = false;
            txt_infusao_final.IsEnabled = false;
            txt_perfusoes.IsEnabled = false;
            txt_total_uf.IsEnabled = false;
            //Anticoagulação
            txt_heparina_inicio_dialise.IsEnabled = false;
            txt_interrup_heparina_dialise.IsEnabled = false;
            txt_tipo_tecnica_dialise.IsEnabled = false;
            txt_debito_sangue_dialise.IsEnabled = false;
            txt_pressao_arterial_dialise.IsEnabled = false;
            txt_objectivo_uf_dialise.IsEnabled = false;
            txt_heparina_hora_dialise.IsEnabled = false;
            txt_heparina_bpm_dialise.IsEnabled = false;
            txt_debito_sangue_tecnica.IsEnabled = false;
            //HORA REGISTO DIÁLISE
            txt_hora_dialise.IsEnabled = false;
            txt_tensao_arterial_dialise.IsEnabled = false;
            txt_temperatura_dialise.IsEnabled = false;
            txt_pressao_venosa_dialise.IsEnabled = false;
            btn_novo_registo_dialise.IsEnabled = false;
            btn_salvar_registo_dialise.IsEnabled = false;
            btn_alterar_registo_dialise.IsEnabled = false;
            btn_eliminar_registo_dialise.IsEnabled = false;
            listViewHoraRegistoDialise.IsEnabled = false;
            txt_notas_enfermagem.IsEnabled = false;
            txt_enfermeiro_Liga.IsEnabled = false;
            txt_enfermeiro_desliga.IsEnabled = false;
            txt_medico_responsável.IsEnabled = false;
            txt_coord_turno.IsEnabled = false;
        }

        

        private void rb_presente_Checked(object sender, RoutedEventArgs e)
        {
            //DESACTIVAR CONTROLES
            cmb_Sala.IsEnabled = true;
            cmb_Turno.IsEnabled = true;
            date_Data_Registo_Dialise.IsEnabled = true;
            txt_peso_entrada.IsEnabled = true;
            txt_peso_seco.IsEnabled = true;
            txt_peso_saida.IsEnabled = true;
            txt_objetivo_uf.IsEnabled = true;
            txt_primming.IsEnabled = true;
            txt_alimentacao.IsEnabled = true;
            txt_infusao_final.IsEnabled = true;
            txt_perfusoes.IsEnabled = true;
            txt_total_uf.IsEnabled = true;
            //Anticoagulação
            txt_heparina_inicio_dialise.IsEnabled = true;
            txt_interrup_heparina_dialise.IsEnabled = true;
            txt_tipo_tecnica_dialise.IsEnabled = true;
            txt_debito_sangue_dialise.IsEnabled = true;
            txt_pressao_arterial_dialise.IsEnabled = true;
            txt_objectivo_uf_dialise.IsEnabled = true;

            txt_heparina_hora_dialise.IsEnabled = true;
            txt_heparina_bpm_dialise.IsEnabled = true;
            txt_debito_sangue_tecnica.IsEnabled = true;
            //HORA REGISTO DIÁLISE
            txt_hora_dialise.IsEnabled = true;
            txt_tensao_arterial_dialise.IsEnabled = true;
            txt_temperatura_dialise.IsEnabled = true;
            txt_pressao_venosa_dialise.IsEnabled = true;


            btn_novo_registo_dialise.IsEnabled = true;
            btn_salvar_registo_dialise.IsEnabled = true;
            btn_alterar_registo_dialise.IsEnabled = true;
            btn_eliminar_registo_dialise.IsEnabled = true;
            listViewHoraRegistoDialise.IsEnabled = true;
            txt_enfermeiro_Liga.IsEnabled = true;
            txt_enfermeiro_desliga.IsEnabled = true;
            txt_medico_responsável.IsEnabled = true;
            txt_coord_turno.IsEnabled = true;
        }

        private void rb_presente_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rb_ausente_Unchecked_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_salvar_registo_dialise_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
