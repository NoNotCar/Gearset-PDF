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
            var workingDirectory = Environment.CurrentDirectory;
            var currentDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var inputLines = File.ReadAllLines(currentDirectory + "/repeated.txt");

            var pdfDocument = new PdfDocument(new PdfWriter(new FileStream(currentDirectory + "/output.pdf", FileMode.Create, FileAccess.Write)));
            var textWriter = new TextWriter(new Document(pdfDocument));
            foreach (string line in inputLines)
            {
                textWriter.ProcessLine(line);
            }
            var document = textWriter.GetOutput();
            document.Close();
            Console.WriteLine("PDF Creation Successful!");
        }
    }
}