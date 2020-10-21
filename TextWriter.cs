using iText.Layout;
using iText.Layout.Element;
class TextWriter
{
    private Paragraph currentParagraph;
    private Document doc;
    public TextWriter(Document document)
    {
        doc = document;
    }
    public void writeText(string text)
    {
        (currentParagraph??=new Paragraph()).Add(text);
    }
    public Document getOutput(){
        if (!(currentParagraph is null)){
            doc.Add(currentParagraph);
        }
        return doc;
    }
}