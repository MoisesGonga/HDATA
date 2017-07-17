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
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CamadaNegocio;
using System.Data;
using Npgsql;
using System.IO;
using HDATA.Reports.ReportDataSets;
using CrystalDecisions.CrystalReports.Engine;
using CamadaObjectoTransferecia;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for ViewReportProveniencia.xaml
    /// </summary>
    public partial class ViewReportProveniencia : Window
    {
        public ViewReportProveniencia()
        {
            InitializeComponent();
        }
        DataTable DataTableProveniencia;
        ReportDocument Documento = new ReportDocument();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTableProveniencia = new DataTable();

            ProvenienciaBLL provenienciaBll = new ProvenienciaBLL();
            DataTableProveniencia = provenienciaBll.ListarProvenienciaDataTable();

            ProvenicenciaDataSet PD = new ProvenicenciaDataSet();
            PD.Tables["Proveniencia"].Merge(DataTableProveniencia, true, MissingSchemaAction.Ignore);
            Documento.Load(Directory.GetCurrentDirectory()+ @"..\..\..\Reports\teste.rpt");
            Documento.SetDataSource(PD);
            crystalReportViewr.ViewerCore.ReportSource = Documento;
        }
    }
}
