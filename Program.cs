using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

class Program
{
    static void Main(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var inputLines = System.IO.File.ReadAllLines(currentDirectory + "/input.txt");

        var pdfDocument = new PdfDocument(new PdfWriter(new FileStream(currentDirectory + "/output.pdf", FileMode.Create, FileAccess.Write)));
        var document = new Document(pdfDocument);
        var textWriter = new TextWriter();

        foreach (string line in inputLines)
        {
            textWriter.writeText(document, line);
        }
        document.Close();
        Console.WriteLine("PDF Creation Successful!");
    }
}
