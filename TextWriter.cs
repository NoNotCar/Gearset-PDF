using iText.Layout;
using iText.Layout.Element;
class TextWriter
{
    public void writeText(Document document, string text)
    {
        document.Add(new Paragraph(text));
    }
}