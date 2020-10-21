using System;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

class TextWriter
{
    private Paragraph currentParagraph;
    private Document doc;
    private bool bold = false;
    private bool italic = false;
    public TextWriter(Document document) {
        doc = document;
        currentParagraph = new Paragraph();
    }
    public void ProcessLine(String line)
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
    private void ProcessCommand(String command)
    {
        switch (command)
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
            case "nofill":
                currentParagraph.SetTextAlignment(TextAlignment.JUSTIFIED);
                AddCurrentParagraph();
                break;
        }
    }
    private void WriteText(String toWrite)
    {
        if (!currentParagraph.IsEmpty() && !Char.IsPunctuation(toWrite[0]))
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
        currentParagraph.Add(text);
    }
    private void AddCurrentParagraph()
    {
        if (!(currentParagraph is null))
        {
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