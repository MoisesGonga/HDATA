﻿using CamadaNegocio;
using CamadaObjectoTransferecia;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for SessaoHemodialise.xaml
    /// </summary>
    public partial class SessaoHemodialise : UserControl
    {
        PacienteBLL pacienteBLL;
        usc_registo_dialise userControRegistoDialise;

        public SessaoHemodialise()
        {
            InitializeComponent();

            pacienteBLL = new PacienteBLL();
            userControRegistoDialise = new Views.usc_registo_dialise();
            datepicker_escala.SelectedDate = DateTime.Now.Date;
            CarregarPacientesEscaladosSessãoHemodialise();

        }

        private void CarregarPacientesEscaladosSessãoHemodialise()
        {
            RegistoHemodialiseBLL reghemod = new RegistoHemodialiseBLL();
            DateTime date_value = datepicker_escala.SelectedDate.Value;
            if (string.IsNullOrEmpty(date_value.ToString()))
            {
                date_value = DateTime.Now.Date;
                datepicker_escala.SelectedDate = date_value;
                dataGrid_PacietesEscalados.ItemsSource = reghemod.ListarPacientesEscalados(date_value).DefaultView;
            }
            else
            {
                dataGrid_PacietesEscalados.ItemsSource = reghemod.ListarPacientesEscalados(date_value).DefaultView;

            }
        }

        private void dataGrid_PacietesEscalados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_PacietesEscalados.SelectedItems.Count > 0)
            {
                btn_registrar_sessão_Hemodialise.IsEnabled = true;
            }
            else
            {
                btn_registrar_sessão_Hemodialise.IsEnabled = false;
            }
        }

        private void btn_registrar_sessão_Hemodialise_Click(object sender, RoutedEventArgs e)
        {
            Janela_Transicao_Telas janelaRegistoDialise = new Views.Janela_Transicao_Telas();
            if (dataGrid_PacietesEscalados.SelectedItems.Count > 0)
            {
            var item = dataGrid_PacietesEscalados.SelectedItem;
            Paciente p = pacienteBLL.ObterPacientePeloCodigo(Convert.ToInt32((dataGrid_PacietesEscalados.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text));
                 janelaRegistoDialise = new Views.Janela_Transicao_Telas();
                NavigationService.GridNavigationUsercontrol(janelaRegistoDialise.grid_main, userControRegistoDialise);
                janelaRegistoDialise.ShowDialog();
            }
            janelaRegistoDialise = new Views.Janela_Transicao_Telas();
            userControRegistoDialise = new Views.usc_registo_dialise();
            janelaRegistoDialise.Title = "REGISTO DE SESSÃO DE HEMODIÁLISE";
            NavigationService.GridNavigationUsercontrol(janelaRegistoDialise.grid_main, userControRegistoDialise);
            janelaRegistoDialise.ShowDialog();

        }

        private void datepicker_escala_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(datepicker_escala.SelectedDate.ToString()))
            {
                CarregarPacientesEscaladosSessãoHemodialise();
            }
            
        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
