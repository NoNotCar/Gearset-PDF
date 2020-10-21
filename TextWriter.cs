using System;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Gearset_PDF
{
    class TextWriter
    {
        const int DEFAULT_FONT_SIZE = 14;
        private Paragraph currentParagraph;
        private Document doc;
        private bool bold = false;
        private bool italic = false;
        private TextAlignment alignment = TextAlignment.LEFT;
        private int size = DEFAULT_FONT_SIZE;
        private int indentation = 0;
        public TextWriter(Document document)
        {
            doc = document;
            currentParagraph = new Paragraph();
        }

        public void ProcessLine(string line)
        {
            if (line.StartsWith("."))
            {
                ProcessCommand(line.Substring(1));
            }
            else
            {
                WriteText(line);
            }
        }
        private void ProcessCommand(string command)
        {
            var split = command.Split(" ");
            var root = split[0];
            switch (root)
            {
                case "bold":
                    bold = true;
                    break;
                case "italics":
                    italic = true;
                    break;
                case "regular":
                    bold = false;
                    italic = false;
                    break;
                case "paragraph":
                    AddCurrentParagraph();
                    break;
                case "fill":
                    alignment = TextAlignment.JUSTIFIED;
                    break;
                case "nofill":
                    alignment = TextAlignment.LEFT;
                    //In the example given it ends the current paragraph as well...
                    AddCurrentParagraph();
                    break;
                case "large":
                    size = DEFAULT_FONT_SIZE * 2;
                    break;
                case "normal":
                    size = DEFAULT_FONT_SIZE;
                    break;
                case "indent":
                    try
                    {
                        indentation = Math.Max(indentation + int.Parse(split[1]), 0);
                    }
                    catch (FormatException)
                    {
                        //TODO: Exception handling
                    }
                    break;
                default:
                    //TODO: More Exception handling
                    Console.WriteLine("Unrecognised command: " + root);
                    break;
            }
        }
        private void WriteText(string toWrite)
        {
            if (!currentParagraph.IsEmpty() && !char.IsPunctuation(toWrite[0]))
            {
                toWrite = " " + toWrite;
            }
            var text = new Text(toWrite);
            if (bold)
            {
                text.SetBold();
            }
            if (italic)
            {
                text.SetItalic();
            }
            text.SetFontSize(size);
            currentParagraph.Add(text);
        }
        private void AddCurrentParagraph()
        {
            if (!(currentParagraph is null))
            {
                currentParagraph.SetTextAlignment(alignment);
                currentParagraph.SetMarginLeft(indentation * 25);
                doc.Add(currentParagraph);
                currentParagraph = new Paragraph();
            }
        }
        public Document GetOutput()
        {
            AddCurrentParagraph();
            return doc;
        }
    }
}