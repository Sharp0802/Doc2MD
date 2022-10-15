using System.Text;
using System.Xml;

namespace Doc2MD;

internal class Renderer
{
    
    private string T(string t, object[] p) => string.Format(t, p);
    
    public string ApplyTemplate(string key, string value)
    {
        return key switch
        {
            "summary" => $"{value}",
            "remarks" => $"## Remarks\n\n{value}",
            "returns" => $"### Returns\n\n{value}",
            "param" => $"### Parameters\n\n{value}",
            "typeparam" => $"### Type Parameters\n\n{value}",
            "exception" => $"### Exceptions\n\n{value}",
            "value" => $"### Property Value\n\n{value}",
            
            "seealso" => $"## See Also\n\n{value}"
        };
    }
    
    public string RenderMember(XmlElement element)
    {
        var sections = new Dictionary<string, string>();
        foreach (var section in element.ChildNodes
                     .OfType<XmlElement>()
                     .GroupBy(e => e.Name))
        {
            var builder = new StringBuilder();
            foreach (var item in section) 
                builder.Append(Render(item)).Append(" \n");

            sections.Add(section.Key, builder.ToString());
        }
    }
    
    public string Render(XmlNode node)
    {
        switch (node)
        {
            case XmlElement element:
                Render(element);
                break;
            case XmlText text:
                Render(text);
                break;
        }
    }

    public string Render(XmlElement element)
    {
        switch (element.Name)
        {
            
        }
    }

    public string Render(XmlText text) => text.Value;
}