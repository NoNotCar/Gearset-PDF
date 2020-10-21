using System;
using System.Runtime.InteropServices.ComTypes;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
class TextWriter
{
    private Paragraph currentParagraph;
    private Document doc;
    private bool bold = false;
    private bool italic = false;
    public TextWriter(Document document) => doc = document;
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
        }
    }
    private void WriteText(String toWrite)
    {
        var text = new Text(toWrite);
        if (bold)
        {
            text.SetBold();
        }
        if (italic)
        {
            text.SetItalic();
        }
        (currentParagraph ??= new Paragraph()).Add(text);
    }
    private void AddCurrentParagraph()
    {
        if (!(currentParagraph is null))
        {
            doc.Add(currentParagraph);
            currentParagraph = null;
        }
    }
    public Document GetOutput()
    {
        AddCurrentParagraph();
        return doc;
    }
}