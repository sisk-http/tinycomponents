using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace TypedComponents;

public sealed class Element : IRenderable
{
    // Metadata
    public static Element Title(string content) => new Element("title", content);
    public static Element Head(Action<Element> builder) => new Element("head", builder);
    public static Element Link(string href, string rel) => new Element("link", new { href, rel }, false) { SelfClosing = true };
    public static Element Meta(string? name, string? content, string? charset, string? httpEquiv) => new Element("meta", new { name, content, charset, httpEquiv }, false) { SelfClosing = true };
    public static Element CssStyle(Action<Stylesheet> action) => Stylesheet.StyleTag(action);
    public static Element JsScript(string script) => new Element("script", s => { s.Children.Add(new Text(script, false)); });
    public static Element JsExternalScript(string src) => new Element("script", new { src }, "");

    // Content sectioning
    public static Element Div(Action<Element> builder) => new Element("div", builder);
    public static Element Div(object? content) => new Element("div", content);
    public static Element Article(Action<Element> builder) => new Element("article", builder);
    public static Element Aside(Action<Element> builder) => new Element("aside", builder);
    public static Element Header(Action<Element> builder) => new Element("header", builder);
    public static Element Footer(Action<Element> builder) => new Element("footer", builder);
    public static Element Body(Action<Element> builder) => new Element("body", builder);
    public static Element Html(Action<Element> builder) => new Element("html", builder);
    public static Element Main(Action<Element> builder) => new Element("main", builder);
    public static Element Section(Action<Element> builder) => new Element("section", builder);
    public static Element Search(Action<Element> builder) => new Element("search", builder);
    public static Element Nav(Action<Element> builder) => new Element("nav", builder);
    public static Element HeaderGroup(Action<Element> builder) => new Element("hgroup", builder);
    public static Element Fragment(Action<Element> builder) => new Element("", builder);
    public static Element Empty() => new Element("");

    // Content
    public static Element Image(string src, string? alt) => new Element("img", new { src, alt }, false) { SelfClosing = true };
    public static Element Video(string? src) => new Element("video", new { src }, false);
    public static Element Source(string src) => new Element("source", new { src }, false) { SelfClosing = true };
    public static Element Paragraph(Action<Element> builder) => new Element("p", builder);
    public static Element Paragraph(object? content) => new Element("p", content);
    public static Element Li(Action<Element> builder) => new Element("li", builder);
    public static Element Li(object? content) => new Element("li", content);
    public static Element Span(Action<Element> builder) => new Element("span", builder);
    public static Element Span(object? content) => new Element("span", content);
    public static Element Strong(Action<Element> builder) => new Element("strong", builder);
    public static Element Strong(object? content) => new Element("strong", content);
    public static Element Label(Action<Element> builder) => new Element("label", builder);
    public static Element Label(object? content) => new Element("label", content);
    public static Element Italic(Action<Element> builder) => new Element("i", builder);
    public static Element Italic(object? content) => new Element("i", content);
    public static Element H1(object? content) => new Element("h1", content);
    public static Element H2(object? content) => new Element("h2", content);
    public static Element H3(object? content) => new Element("h3", content);
    public static Element H4(object? content) => new Element("h4", content);
    public static Element H5(object? content) => new Element("h5", content);
    public static Element H6(object? content) => new Element("h6", content);
    public static Element Anchor(string href, object? text) => new Element("a", new { href }, text);
    public static Element Anchor(string href, Action<Element> builder) => new Element("a", new { href }, builder);
    public static Element Blockquote(Action<Element> builder) => new Element("blockquote", builder);
    public static Element Blockquote(object? content) => new Element("blockquote", content);
    public static Element Ul(Action<Element> builder) => new Element("ul", builder);
    public static Element Ol(Action<Element> builder) => new Element("ol", builder);
    public static Element Button(Action<Element> builder) => new Element("button", builder);
    public static Element Button(object? content) => new Element("button", content);

    // Inputs
    public static class Input
    {
        public static Element Button(string name, string value) => new Element("input", new { type = "button", value }, false) { SelfClosing = true, Name = name };
        public static Element Checkbox(string name, bool isChecked = false) => new Element("input", new { type = "checkbox", @checked = isChecked }, false) { SelfClosing = true, Name = name };
        public static Element Color(string name, string? color) => new Element("input", new { type = "color", value = color }, false) { SelfClosing = true, Name = name };
        public static Element Date(string name, DateTime? value) => new Element("input", new { type = "date", value = value?.ToString("yyyy-MM-dd") }, false) { SelfClosing = true, Name = name };
        public static Element DatetimeLocal(string name, DateTime? value) => new Element("input", new { type = "datetime-local", value = value?.ToString("s") }, false) { SelfClosing = true, Name = name };
        public static Element Time(string name, TimeSpan? value) => new Element("input", new { type = "time", value = value?.ToString("hh:mm") }, false) { SelfClosing = true, Name = name };
        public static Element Email(string name, string? email) => new Element("input", new { type = "email", value = email }, false) { SelfClosing = true, Name = name };
        public static Element Text(string name, string? text) => new Element("input", new { type = "text", value = text }, false) { SelfClosing = true, Name = name };
        public static Element File(string name, string? accept = null) => new Element("input", new { type = "text", accept }, false) { SelfClosing = true, Name = name };
        public static Element Hidden(string name, object? value) => new Element("input", new { hidden = true, value }, false) { SelfClosing = true, Name = name };
        public static Element Month(string name, DateTime? value) => new Element("input", new { type = "month", value = value?.ToString("yyyy-MM") }, false) { SelfClosing = true, Name = name };
        public static Element Number(string name, Double? value) => new Element("input", new { type = "number", value }, false) { SelfClosing = true, Name = name };
        public static Element Password(string name) => new Element("input", new { type = "password" }, false) { SelfClosing = true, Name = name };
        public static Element Radio(string name, bool selected = false) => new Element("input", new { type = "radio", @checked = selected }, false) { SelfClosing = true, Name = name };
        public static Element Range(string name, double min, double max, double value) => new Element("input", new { type = "range", min, max, value }, false) { SelfClosing = true, Name = name };
        public static Element Submit(string name, string value) => new Element("input", new { type = "submit", value }, false) { SelfClosing = true, Name = name };
    }

    public string Tag { get; set; } = "div";
    public bool SelfClosing { get; set; } = false;
    public NameValueCollection Attributes { get; set; } = new NameValueCollection();
    public ICollection<IRenderable> Children { get; set; } = new List<IRenderable>();

    // shorthand for know attributes
    public int? TabIndex { get; set; }
    public string? TooltipTitle { get; set; }
    public string? Id { get; set; }
    public string? Name { get; set; }
    public object? Style { get; set; }
    public ICollection<string> ClassList { get; set; } = new List<string>();

    public Element(string tagName)
    {
        Tag = tagName.ToLower();
    }

    public Element(string tagName, Action<Element> builder)
    {
        Tag = tagName.ToLower();
        builder(this);
    }

    public Element(string tagName, object attributes, Action<Element> builder)
    {
        Tag = tagName.ToLower();
        GetAttributesFromObject(attributes);
        builder(this);
    }

    public Element(string tagName, object? contents)
    {
        Tag = tagName.ToLower();
        Children.Add(new Text(contents?.ToString()));
    }

    public Element(string tagName, object attributes, object? contents)
    {
        Tag = tagName.ToLower();
        GetAttributesFromObject(attributes);
        Children.Add(new Text(contents?.ToString()));
    }

    public Element HasClass(params string[] classNames)
    {
        foreach (string className in classNames) ClassList.Add(className);
        return this;
    }

    public Element HasId(string id)
    {
        Id = id;
        return this;
    }

    public Element HasName(string name)
    {
        Name = name;
        return this;
    }
    public Element HasStyle(object style)
    {
        Style = style;
        return this;
    }

    public Element HasAttribute(string key, object? value)
    {
        Attributes.Add(key, value?.ToString());
        return this;
    }

    public string? Render()
    {
        StringBuilder sb = new StringBuilder();

        if (Tag == "")
        {
            // is an fragment tag
            RenderChildren(sb);
            return sb.ToString();
        }

        string attributes = BuildAttributes();

        if (attributes != "")
        {
            sb.Append($"<{Tag} {attributes}>");
        }
        else
        {
            sb.Append($"<{Tag}>");
        }

        if (!SelfClosing)
        {
            RenderChildren(sb);
            sb.Append($"</{Tag}>");
        }

        return sb.ToString();
    }

    private void RenderChildren(StringBuilder sb)
    {
        foreach (IRenderable component in Children)
        {
            string? render = component.Render();
            sb.Append(render);
        }
    }

    private string BuildAttributes()
    {
        NameValueCollection clone = new NameValueCollection(Attributes);
        if (TabIndex != null) clone.Add("tabindex", TabIndex.ToString());
        if (TooltipTitle != null) clone.Add("title", TooltipTitle.ToString());
        if (Id != null) clone.Add("id", Id.ToString());
        if (Name != null) clone.Add("name", Name.ToString());
        if (ClassList?.Count > 0) clone.Add("class", string.Join(' ', ClassList));
        if (Style != null) clone.Add("style", Stylesheet.FromObject(Style).Render());

        if (clone.Count == 0) return "";

        List<string> attributeList = new List<string>();
        foreach (string rawName in clone)
        {
            string name = Stylesheet.PascalToKebabCase(rawName);
            string? value = clone[name];
            if (value == null)
            {
                attributeList.Add(name);
            }
            else
            {
                attributeList.Add($"{name}=\"{value}\"");
            }
        }
        return string.Join(' ', attributeList);
    }

    private void GetAttributesFromObject(object obj)
    {
        foreach (PropertyInfo pinfo in obj.GetType().GetProperties())
        {
            string name = pinfo.Name;
            string? txtValue;
            object? value = pinfo.GetValue(obj);
            if (value is bool b)
            {
                if (b == false)
                {
                    continue;
                }
                else
                {
                    txtValue = name;
                }
            }
            else
            {
                txtValue = value?.ToString();
            }
            Attributes.Add(name, txtValue);
        }
    }

    public static Element operator +(Element a, Element? b)
    {
        if (b == null) return a;
        a.Children.Add(b);
        return a;
    }

    public static Element operator +(Element a, IRenderable? b)
    {
        if (b == null) return a;
        a.Children.Add(b);
        return a;
    }
}
