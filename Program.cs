using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Gearset_PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream(Directory.GetCurrentDirectory()+"/hello.pdf", FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);

            String line = "Hello! Welcome to iTextPdf";
            document.Add(new Paragraph(line));
            document.Close();
            Console.WriteLine("Awesome PDF just got created.");
        }
    }
}
