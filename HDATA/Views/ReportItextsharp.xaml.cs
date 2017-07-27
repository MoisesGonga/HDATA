using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.testutils;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Diagnostics;

using System.IO;
using HDATA.ReportService;
using iTextSharp.text;
using System.ComponentModel;
using CamadaNegocio;
using System.Data;
using CamadaObjectoTransferecia;

namespace HDATA.Views
{
    /// <summary>
    /// Interaction logic for ReportItextsharp.xaml
    /// </summary>
    public partial class ReportItextsharp : Window
    {
        PacienteBLL pacienteBLL;
        DataTable datatablePaciente;
        public ReportItextsharp()
        {
            pacienteBLL = new PacienteBLL();
            datatablePaciente = new DataTable();
            InitializeComponent();
            UseBackgroundWorker();
        }

        private void CriarDocCabecalhoRodape()
        {
            Document document = new Document(PageSize.A4,40f,40f,20f,40f);
            
            string docname = $"{DateTime.Now.Second}teste.pdf";
            try
            {
                
                // cria o arquivo pdf
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(docname, FileMode.Create));

                // cria um objeto do tipo FontFamily, que contem as propriedades de uma fonte
                Font.FontFamily familha = new Font.FontFamily();

                // atribui a familia da fonte, no caso Courier
                familha = iTextSharp.text.Font.FontFamily.HELVETICA;

                // cria uma fonte atribuindo a familha, o tamanho da fonte e o estilo (normal, negrito...)
                Font fonte = new Font(familha, 8, (int)System.Drawing.FontStyle.Bold);

                // cria uma instancia da classe eventos, é uma classe que mostrarei posteriormente
                // esta clase trata a criação do cabeçalho e rodapé da página

                //ADICIONAR O LOGOTIPO
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("Logo SBDE.jpg");
                ConfigureReportItextSharp configuredocument = new ConfigureReportItextSharp(fonte,img);

                // seta o atributo de eventos da classe com a variavel de eventos criada antes
                writer.PageEvent = configuredocument;
               

                // altera a fonte para normal, a negrito era apenas para o cabeçalho e rodapé da página
                fonte = new Font(familha, 8, (int)System.Drawing.FontStyle.Regular);

                // abre o documento para começar a escrever o pdf
                document.Open();
                //iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle( new System.util.RectangleJ(document.PageSize.Width - 200f,document.PageSize.Height -140f,140f,90f));
                //rect.BorderColor = BaseColor.LIGHT_GRAY;
                //rect.BackgroundColor = BaseColor.GREEN;
                //rect.BorderWidth = 2f;
                
                
                
                //document.Add(rect);

                // aqui faz um for para simular diversas linhas de um relatorio
                for (int i = 0; i < 100; i++)
                {
                    // adiciona um novo paragrafo com o texto da respectiva linha.
                    document.Add(new Paragraph("Teste linha", fonte));
                }
            }
            catch (Exception de)
            {
                MessageBox.Show(de.Message);
            }

            // fecha o documento
            document.Close();

            // manda abrir o pdf
            Process.Start(docname);
        }


        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            

            Document doc = new Document(iTextSharp.text.PageSize.A4,20,20,20,20);
            doc.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
            PdfWriter pdfwriter = PdfWriter.GetInstance(doc, new FileStream(($"{txtNomePaciente.Text}.pdf"),FileMode.Create));
            doc.Open();//Open Document to Write
                       // Write some Content to the pdf file
            Paragraph Titulo = new Paragraph("RELATÓRIO MÉDICO\n", new Font(Font.FontFamily.HELVETICA, 14,0));
            //Aligment - 0 - Justified; 1 - Center ; 2 - Right
            //Font Style : 0 - ;1 - Negrito; 2 - 

            //HEADER & FOOTER

            Header head = new Header("first Header", "Exemplo de Cabeçalho... Teste 1");

            //HeaderFooter header = new HeaderFooter(new Phrase("Texto do Cabeçalho"), false);
            //pdfDoc.Header = header;

            

            //HeaderFooter footer = new HeaderFooter(new Phrase("Texto do Rodapé: "), true);
            //pdfDoc.Footer = footer;


            //Using Images
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("Logo SBDE.jpg");
            img.Alignment = 1;
            //doc.Add(img);
            
            img.Alignment = 0;
            img.ScalePercent(10);
            doc.Add(img);

            img.Alignment = 1;
            img.ScalePercent(5);
            doc.Add(img);

            Titulo.Alignment = 1;
            doc.PageCount = 3;
            Paragraph paragraph = new Paragraph("Bem o sistema que estou trabalhando atualmente exige bastante Ajax e manipulação DOM, logo comecei a aprender (a não muito tempo), JQuery. exige bastante Ajax e manipulação DOM, logo comecei a aprender (a não muito tempo), JQuery.exige bastante Ajax e manipulação DOM, logo comecei a aprender (a não muito tempo), JQuery.\n \n \n");
            Paragraph paragraph2 = new Paragraph("Na feature que estou desenvolvendo atualmente , existe um plugin Jquery chamado multi-select, que tem como objetivo selecionar alguns destinos para posteriormente salva-los. \n \n \n", new Font(Font.FontFamily.HELVETICA,12));
            paragraph.Alignment = 0;
            doc.AddTitle("Relatório Médico");
            //doc.AddHeader("Cabeçalho 1", "Centro de Hemodiálise do Hospital Josina Machel \n");

            //doc.Add(Titulo);

            //Titulo = new Paragraph("RELATÓRIO MÉDICO\n", new Font(Font.FontFamily.HELVETICA, 14, 1));
            //doc.Add(Titulo);
            //Titulo = new Paragraph("RELATÓRIO MÉDICO\n", new Font(Font.FontFamily.HELVETICA, 14, 2));
            //doc.Add(paragraph);
            //doc.Add(paragraph2);
            //doc.NewPage();
            doc.SetMargins(30, 30, 30, 30);
            string directorClinico = "O Director Clínico \n";
            Paragraph directorAssignature = new Paragraph(directorClinico, new Font(Font.FontFamily.HELVETICA, 14, 3));
            
            directorAssignature.Alignment = 2;
            directorAssignature.IndentationLeft = 90;
            doc.Add(directorAssignature);
            directorAssignature.PaddingTop = 10;
            doc.Add(directorAssignature);
            //____________________________

            directorAssignature.IndentationRight = 50;
           
            directorAssignature = new Paragraph("____________________________", new Font(Font.FontFamily.HELVETICA, 14, 0));
            directorAssignature.Alignment = 2;

            doc.Add(directorAssignature);
            
            //doc.Add(Titulo);
            //doc.Add(paragraph);
            //doc.Add(paragraph2);
            //doc.NewPage();
            string dadosPac = $" \t \t O Paciente {txtNomePaciente.Text} de 49 anos de idade, do sexo masculino e de raça negra, encontrava-se aparentemente bem até 2008 altura em que foi enviado de urgência ao hospital Josina Machel com quadro clínico de hpertensão arterial grave tendo ficado internado a 05/11/2008. Durante o internamento verificou-se ter uma insuficiencia renal crónica. Constatou-se uma retenção azotada grave com valores de ureia 27 mg/dl e creatinina 15.4 mg/dl, e anemia com hemoglobina 7.7g/dl. Como não houve recuperação da função renal com terapêutica médica, decidiu-se iniciar terapeutica substitutiva da função renal, hemodiálise.";
            dadosPac += "\nO doente tem tido uma aderência positiva ao tratamento dialítico.";
            dadosPac += "\nO facto de fazer tratamento dialítico não impede a sua capacidade laboral actual. \n \n \n \n";
            paragraph = new Paragraph(dadosPac, new Font(Font.FontFamily.HELVETICA, 12));
            paragraph.Alignment = 1;
            doc.Add(paragraph);
            paragraph.Alignment = 2;
            doc.Add(paragraph);
            paragraph.IndentationLeft = 50;
            paragraph.Alignment = 3;
            doc.Add(paragraph);
            doc.AddAuthor("Moisés Daniel Gonga - Direitos Autorais");
            doc.Close();//Close document
            
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CriarDocCabecalhoRodape();
            if (this.popInformation.IsOpen)
            {
                popInformation.IsOpen = false;
            }
            else
            {

                popInformation.IsOpen = true;
                popInformation.Focus();

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popInformation.IsOpen = false;
        }

        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void popInformation_LostFocus(object sender, RoutedEventArgs e)
        {
            popInformation.IsOpen = false;
        }

        private void txtNomePaciente_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtNomePaciente.Focus();
        }

        private void Window_GotFocus_1(object sender, RoutedEventArgs e)
        {
            popInformation.IsOpen = false;
        }

        private void UseBackgroundWorker()
        {
            painel_carregando.Visibility = Visibility.Visible;
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.DoWork += new DoWorkEventHandler(worker_DoWorkEventHandler);
            worker.RunWorkerAsync();

        }

        public void CarregarTodosPacientes()
        {
            try
            {
                dataGrid1.Items.Clear();
                DataView dataview = pacienteBLL.BuscarTodosPaciente().AsDataView();
                dataGrid1.ItemsSource = dataview;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Listar os dados dos Pacientes!!!", "Listar Pacientes", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void worker_DoWorkEventHandler(object sender, DoWorkEventArgs e)
        {
            datatablePaciente = pacienteBLL.BuscarTodosPaciente();
            ConsultarpeloNome();
        }

        private async void ConsultarpeloNome()
        {
            //datatablePaciente = pacienteBLL.ConsultarPacientePorNome(buscarPaciente.Text);
            await Task.Delay(1000);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGrid1.ItemsSource = datatablePaciente.AsDataView();
            painel_carregando.Visibility = Visibility.Collapsed;
        }

        private void worker1_DoWorkEventHandler(object sender, DoWorkEventArgs e)
        {
            //List<Paciente> ListaPaciente = new List<Paciente>();
            //List<Paciente> pacientesPesquisados = ListaPaciente.Where(p => p.Nome.Contains(buscarPaciente.Text));
           datatablePaciente = pacienteBLL.ConsultarPacientePorNome(buscarPaciente.Text);
        }

        private void worker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            dataGrid1.ItemsSource = datatablePaciente.AsDataView();
            painel_carregando.Visibility = Visibility.Collapsed;
        }

        private void btn_carregar_pacient_Click(object sender, RoutedEventArgs e)
        {
            //CarregarTodosPacientes();
            //var filteredTable = (from n in datatablePaciente.AsEnumerable() where n.Field<>("nome") = "Amaro Buta").Select.

            UseBackgroundWorker();
        }

        private void buscarPaciente_TextChanged(object sender, TextChangedEventArgs e)
        {
            painel_carregando.Visibility = Visibility.Visible;
            BackgroundWorker worker1 = new BackgroundWorker();
            worker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker1_RunWorkerCompleted);
            worker1.DoWork += new DoWorkEventHandler(worker1_DoWorkEventHandler);
            worker1.RunWorkerAsync();

        }
    }
}
