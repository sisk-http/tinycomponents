//#define HELPER_METHODS_ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace TinyComponents
{
    /// <summary>
    /// Represents an HTML element for rendering
    /// </summary>
    public class HtmlElement : IXmlNode
    {
        /// <summary>
        /// Formats the specified HTML string format, escaping the string interpolation pieces.
        /// </summary>
        /// <param name="htmlString">The instance of <see cref="FormattableString"/>.</param>
        public static string Format(FormattableString htmlString)
        {
            string[] formattedData = new string[htmlString.ArgumentCount];
            for (int i = 0; i < formattedData.Length; i++)
            {
                formattedData[i] = RenderableText.SafeRenderSubject(htmlString.GetArgument(i)?.ToString());
            }
            return string.Format(htmlString.Format, formattedData);
        }

        /// <summary>
        /// Gets or sets the tag name of the HTML element (e.g., "div", "span").
        /// </summary>
        public string TagName { get; set; } = "div";

        /// <summary>
        /// Gets or sets a value indicating whether the element is self-closing.
        /// </summary>
        public bool SelfClosing { get; set; } = false;

        /// <summary>
        /// Gets or sets the collection of HTML attributes for the element.
        /// </summary>
        public Dictionary<string, object?> Attributes { get; set; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets or sets the collection of child elements within this element.
        /// </summary>
        public ICollection<object?> Children { get; set; } = new List<object?>();

        /// <summary>
        /// Gets or sets the tab index of the HTML element.
        /// </summary>
        public int? TabIndex { get; set; }

        /// <summary>
        /// Gets or sets the tooltip text to display for the HTML element.
        /// </summary>
        public string? TooltipTitle { get; set; }

        /// <summary>
        /// Gets or sets the ID attribute of the HTML element. 
        /// Used to uniquely identify the element within the page.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name attribute of the HTML element.
        /// The name is used to reference elements in JavaScript, or to reference form data after a form is submitted.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the list of CSS classes for the HTML element. Initializes with an empty list.
        /// Use this to apply CSS class names to the element.
        /// </summary>
        public ICollection<string> ClassList { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the CSS style object used to render the style attribute.
        /// </summary>
        public object? Style { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with no container HTML element.
        /// </summary>
        public HtmlElement()
        {
            TagName = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        public HtmlElement(string tagName)
        {
            TagName = tagName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name and content.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, object? content)
        {
            TagName = tagName;
            this.WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name and content.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, string content)
        {
            TagName = tagName;
            this.WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, Action<HtmlElement> content)
        {
            TagName = tagName;
            this.WithContent(content);
        }

        /// <inheritdoc/>
        public static HtmlElement operator +(HtmlElement a, object? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        /// <summary>
        /// Represents the protected method which gets the attributes to be rendered.
        /// </summary>
        protected virtual IDictionary<string, object?> GetAttributes()
        {
            Dictionary<string, object?> attributes = new Dictionary<string, object?>(Attributes, StringComparer.OrdinalIgnoreCase);

            if (TabIndex is int tabindex) attributes["tabindex"] = tabindex.ToString();
            if (TooltipTitle is string title) attributes["title"] = title;
            if (Id is string id) attributes["id"] = id;
            if (Name is string name) attributes["name"] = name;
            if (ClassList.Count > 0) attributes["class"] = string.Join(' ', ClassList);

            if (GetStyleValue() is string styleValue)
            {
                attributes["style"] = styleValue;
            }

            return attributes;
        }

        /// <summary>
        /// Renders the HTML element into string.
        /// </summary>
        /// <returns>The rendered HTML string.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(TagName))
            {
                sb.Append('<');
                sb.Append(TagName.ToLower());

                var attr = GetAttributes();
                if (attr.Count > 0)
                {
                    foreach (KeyValuePair<string, object?> at in attr)
                    {
                        if (string.IsNullOrEmpty(at.Key))
                            continue;

                        sb.Append(' ');
                        sb.Append(at.Key);

                        string value = RenderableText.SafeRenderSubject(at.Value);
                        if (string.Compare(value, at.Key) == 0)
                        {
                            continue;
                        }
                        else
                        {
                            sb.Append('=');
                            sb.Append('"');
                            sb.Append(value);
                            sb.Append('"');
                        }
                    }
                }

                sb.Append('>');
            }

            if (SelfClosing)
            {
                return sb.ToString();
            }

            foreach (object? children in Children)
            {
                sb.Append(children?.ToString());
            }

            sb.Append("</");
            sb.Append(TagName);
            sb.Append('>');

            return sb.ToString();
        }

        private string? GetStyleValue()
        {
            if (Style is null)
                return null;

            PropertyInfo[] properties = Style.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            StringBuilder stylesSb = new StringBuilder();
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo propertyInfo = properties[i];
                object? value = propertyInfo.GetValue(Style);
                string? valueStr = value?.ToString();

                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    continue;
                }
                else
                {
                    string name = string.Concat(
                        propertyInfo.Name
                            .Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + char.ToLower(x) : char.ToLower(x).ToString())
                    );
                    stylesSb.Append($"{name}:{valueStr};");
                }
            }

            return stylesSb.ToString(); ;
        }
    }
}