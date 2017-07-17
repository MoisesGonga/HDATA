using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;

namespace HDATA.ReportService
{
    class ConfigureReportItextSharp: PdfPageEventHelper
    {
        // propriedade da fonte que será usada no cabeçalho
        public Font fonte { get; set; }
        public iTextSharp.text.Image logotipo;

        // a classe recebe a fonte no seu construtor a classe não possui construtor padrão, para obrigar
        // a passagem da fonte e evitar erros
        public ConfigureReportItextSharp(Font fonte_)
        {
            fonte = fonte_;
        }

        public ConfigureReportItextSharp(Font fonte_, iTextSharp.text.Image logo)
        {
            fonte = fonte_;
            logo.Alignment = 0;
            logo.ScalePercent(4);
            this.logotipo = logo;
        }
        // Este método cria um cabeçalho para o documento
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            // Cria um novo paragrafo com o texto do cabeçalho
            Paragraph ph = null;
            document.Add(logotipo);
            //Rectangle rect = new Rectangle(10f, 30f);
            //rect.BorderColor = BaseColor.LIGHT_GRAY;
            //rect.BackgroundColor = BaseColor.GREEN;
            //rect.BorderWidth = 2f;
            //rect.Chunks.Add(new Chunk("Exmo.(s) Sr.(s) "));
            //rect.Chunks.Add(new Chunk("SBDE, Lda. "));
            //document.Add(rect);
            // adiciono a linha e posteriormente mais linhas que podem ser necessárias em um cabeçalho de relatório
            ph = new Paragraph("Exmo.(s) Sr.(s) ", fonte);
            ph.Alignment = 2;
            document.Add(ph);
            ph = new Paragraph("SBDE, Lda. ", fonte);
            ph.Alignment = 2;
            document.Add(ph);
            ph = new Paragraph("Luanda", fonte);
            ph.Alignment = 2;
            document.Add(ph);

            // cria um novo paragrafo para imprimir um traço e uma linha em branco
            ph = new Paragraph();

            iTextSharp.text.pdf.draw.LineSeparator lineseparator = new iTextSharp.text.pdf.draw.LineSeparator();
            lineseparator.LineColor = BaseColor.LIGHT_GRAY;
            // cria um objeto sepatador (um traço)
            iTextSharp.text.pdf.draw.VerticalPositionMark seperator = lineseparator;

            // adiciona o separador ao paragravo
            ph.Add(seperator);

            // adiciona a linha em branco(enter) ao paragrafo
            ph.Add(new Chunk("\n"));

            // imprime o pagagrafo no documento
            document.Add(ph);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //// para o rodapé é um pouco diferente precisamos criar um PdfContentByte e uma BaseFont e
            //// setar as propriedades dos mesmos para então poder imprimir alinhado a direita

            //// cria uma instancia da classe PdfContentByte
            //PdfContentByte cb = writer.DirectContent;

            //// cria uma instancia da classe font
            //BaseFont font;

            //// seta as propriedades da fonte
            //font = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

            //// seta a fonte do objeto PdfContentByte
            //cb.SetFontAndSize(font, 9);
            //document.Add(new Paragraph("Texte - Linhas: "+document.PageNumber));
            //// escreve a linha para imprimir o numero da página
            //string texto = "Página: " + writer.PageNumber.ToString();

            //// imprime a linha no rodapé
            //cb.ShowTextAligned(Element.ALIGN_RIGHT, texto, document.Right, document.Bottom - 20, 0);
        }

    }
}
