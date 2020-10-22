using System;
using System.IO;

namespace GearsetPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var currentDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var inputParser = new InputParser(new PDFTextWriter(currentDirectory + "/output.pdf"));
            inputParser.Parse(currentDirectory + "/repeated.txt");
            Console.WriteLine("PDF Creation Successful!");
        }
    }
}