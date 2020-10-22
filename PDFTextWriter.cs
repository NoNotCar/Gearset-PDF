using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace GearsetPDF
{
    public class PDFTextWriter
    {
        const float DEFAULT_FONT_SIZE = 14;
        private Paragraph currentParagraph;
        private Document doc;
        private bool bold = false;
        private bool italic = false;
        private TextAlignment alignment = TextAlignment.LEFT;
        private float size = DEFAULT_FONT_SIZE;
        private int indentation = 0;
        public PDFTextWriter(string outputFile)
        {
            var pdfDocument = new PdfDocument(new PdfWriter(new FileStream(outputFile, FileMode.Create, FileAccess.Write)));
            doc = new Document(pdfDocument);
            currentParagraph = new Paragraph();
        }
        public void SetBold(bool b)
        {
            bold = b;
        }
        public void SetItalic(bool i)
        {
            italic = i;
        }
        public void SetAlignment(TextAlignment t)
        {
            alignment = t;
        }
        public void AddIndent(int delta)
        {

            //indentation can't be below 0
            indentation = Math.Max(indentation + delta, 0);
        }
        public void SetFontScale(float scale)
        {
            size = DEFAULT_FONT_SIZE * scale;
        }
        public void WriteText(string toWrite)
        {
            //add space between the start of words on different lines of the input file
            if (!(currentParagraph.IsEmpty() || char.IsPunctuation(toWrite[0])))
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
        public void NewParagraph()
        {
            if (!(currentParagraph is null))
            {
                currentParagraph.SetTextAlignment(alignment);
                currentParagraph.SetMarginLeft(indentation * 25);
                doc.Add(currentParagraph);
                currentParagraph = new Paragraph();
            }
        }
        public void Save()
        {
            NewParagraph();
            doc.Close();
        }
    }
}