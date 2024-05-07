using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TinyComponents
{
    /// <summary>
    /// Represents an renderable XML node.
    /// </summary>
    public class XmlNode : IXmlNode
    {
        /// <summary>
        /// Gets or sets the tag name of the XML node.
        /// </summary>
        public string TagName { get; set; } = "xml";

        /// <summary>
        /// Gets or sets a value indicating whether this XML node is self-closing.
        /// </summary>
        public bool SelfClosing { get; set; } = false;

        /// <summary>
        /// Gets or sets the collection of attributes for this node.
        /// </summary>
        public Dictionary<string, object?> Attributes { get; set; } = new Dictionary<string, object?>();

        /// <summary>
        /// Gets or sets the collection of child elements (<see cref="IRenderable"/>) within this node.
        /// </summary>
        public ICollection<IRenderable> Children { get; set; } = new List<IRenderable>();

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        public XmlNode(string tagName)
        {
            TagName = tagName.ToLower();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public XmlNode(string tagName, IRenderable content)
        {
            TagName = tagName.ToLower();
            this.WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public XmlNode(string tagName, Action<XmlNode> content)
        {
            TagName = tagName.ToLower();
            this.WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public XmlNode(string tagName, object? content)
        {
            TagName = tagName.ToLower();
            this.WithContent(content);
        }

        /// <inheritdoc/>
        public static XmlNode operator +(XmlNode a, XmlNode? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        /// <inheritdoc/>
        public static XmlNode operator +(XmlNode a, IRenderable? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        /// <inheritdoc/>
        public static XmlNode operator +(XmlNode a, string? b)
        {
            if (b == null) return a;
            a.Children.Add(new RenderableText(b));
            return a;
        }

        /// <summary>
        /// Renders this <see cref="XmlNode"/> into it's XML string representation.
        /// </summary>
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(TagName))
            {
                sb.Append('<');
                sb.Append(TagName);

                if (Attributes.Count > 0)
                {
                    sb.Append(' ');
                    foreach (KeyValuePair<string, object?> at in Attributes)
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

                if (SelfClosing)
                {
                    sb.Append('/');
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
