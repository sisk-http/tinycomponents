//#define HELPER_METHODS_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

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
                formattedData[i] = RenderableText.EscapeXmlLikeLiteral(htmlString.GetArgument(i)?.ToString());
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
        /// Gets or sets the collection of child elements (IRenderable) within this element.
        /// </summary>
        public ICollection<IRenderable> Children { get; set; } = new List<IRenderable>();

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
            TagName = tagName.ToLower();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, IRenderable content)
        {
            TagName = tagName.ToLower();
            this.WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, Action<HtmlElement> content)
        {
            TagName = tagName.ToLower();
            this.WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, string? content)
        {
            TagName = tagName.ToLower();
            this.WithContent(content);
        }

        /// <inheritdoc/>
        public static HtmlElement operator +(HtmlElement a, HtmlElement? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        /// <inheritdoc/>
        public static HtmlElement operator +(HtmlElement a, IRenderable? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        /// <inheritdoc/>
        public static HtmlElement operator +(HtmlElement a, string? b)
        {
            if (b == null) return a;
            a.Children.Add(new RenderableText(b));
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

            return attributes;
        }

        /// <summary>
        /// Renders the HTML element into string.
        /// </summary>
        /// <returns>The rendered HTML string.</returns>
        public virtual string Render()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(TagName))
            {
                sb.Append('<');
                sb.Append(TagName);

                var attr = GetAttributes();
                if (attr.Count > 0)
                {
                    sb.Append(' ');
                    foreach (KeyValuePair<string, object?> at in attr)
                    {
                        sb.Append(at.Key);

                        string? value = at.Value?.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            sb.Append('=');
                            sb.Append('"');
                            sb.Append(RenderableText.EscapeXmlLikeLiteral(value));
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

            foreach (IRenderable children in Children)
            {
                string? result = children.Render();
                sb.Append(result);
            }

            sb.Append("</");
            sb.Append(TagName);
            sb.Append('>');

            return sb.ToString();
        }
    }

}