using CamadaNegocio;
using CrystalDecisions.CrystalReports.Engine;
using HDATA.Reports;
using HDATA.Reports.ReportDataSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Shapes;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for ViewReportSessaoHemodialise.xaml
    /// </summary>
    public partial class ViewReportSessaoHemodialise : Window
    {
        public ViewReportSessaoHemodialise()
        {
            InitializeComponent();
            CarregarDadosReport();
            
        }

        public void CarregarDadosReport()
        {
            try
            {
                string file_ = @"..\HDATA\HDATA\Reports\ListaPacitenteReport.rpt";

                if (File.Exists(file_))
                {
                    ReportDocument reportdocument = new ReportDocument();
                    PacienteBLL pacienteBLL = new PacienteBLL();
                    ListarPacienteDataSet listarPacienteDataSet = new ListarPacienteDataSet();
                    listarPacienteDataSet.Merge(pacienteBLL.BuscarTodosPaciente());
                    reportdocument.Load(file_);
                    reportdocument.SetDataSource(listarPacienteDataSet);
                    crystalReportViewr.ViewerCore.ReportSource = reportdocument;
                }
                    
                
                
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao carregar dados no Relatório...");
                
            }
           
        }
    }
}
