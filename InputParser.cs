using iText.Layout.Properties;
using System;
using System.IO;

namespace GearsetPDF
{
    public class InputParser
    {
        private PDFTextWriter textWriter;
        public InputParser(PDFTextWriter text) => textWriter = text;
        public void Parse(string filename)
        {
            var inputLines = File.ReadAllLines(filename);
            foreach (string line in inputLines)
            {
                ProcessLine(line);
            }
            textWriter.Save();
        }
        private void ProcessLine(string line)
        {
            if (line.StartsWith("."))
            {
                ProcessCommand(line.Substring(1));
            }
            else
            {
                textWriter.WriteText(line);
            }
        }
        private void ProcessCommand(string command)
        {
            if (command.Length == 0)
            {
                //If length 0, split[0] will fail
                Console.WriteLine("Null command encountered, continuing...");
                return;
            }
            //separate commands from their arguments
            var split = command.Split(" ");
            var root = split[0];
            switch (root)
            {
                case "bold":
                    textWriter.SetBold(true);
                    break;
                case "italics":
                    textWriter.SetItalic(true);
                    break;
                case "regular":
                    textWriter.SetBold(false);
                    textWriter.SetItalic(false);
                    break;
                case "paragraph":
                    textWriter.NewParagraph();
                    break;
                case "fill":
                    textWriter.SetAlignment(TextAlignment.JUSTIFIED);
                    break;
                case "nofill":
                    textWriter.SetAlignment(TextAlignment.LEFT);
                    //In the example given it ends the current paragraph as well...
                    textWriter.NewParagraph();
                    break;
                case "large":
                    textWriter.SetFontScale(2);
                    break;
                case "normal":
                    textWriter.SetFontScale(1);
                    break;
                case "indent":
                    if (split.Length < 2)
                    {
                        Console.WriteLine(".indent called with no argument, ignoring...");
                    }
                    else
                    {
                        try
                        {
                            textWriter.AddIndent(int.Parse(split[1]));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Couldn't parse {split[1]} as an integer indentation!");
                        }
                    }
                    break;
                default:
                    Console.WriteLine($"Unrecognised command: {root}\nContinuing on...");
                    break;
            }
        }
    }
}
