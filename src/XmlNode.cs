using System;
using System.Collections.Generic;
using System.Text;

namespace TinyComponents {
    /// <summary>
    /// Represents an renderable XML node.
    /// </summary>
    public class XmlNode : INode {
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
        public NodeAttributeCollection Attributes { get; set; } = new NodeAttributeCollection ();

        /// <summary>
        /// Gets or sets the collection of child elements within this node.
        /// </summary>
        public ICollection<object?> Children { get; set; } = new List<object?> ();

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the XML element. The tag name will be converted to lowercase.</param>
        public XmlNode ( string tagName ) {
            this.TagName = tagName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name and content.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the XML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating XML tag.</param>
        public XmlNode ( string tagName, object? content ) {
            this.TagName = tagName;
            this.WithContent ( content );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name and content.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the XML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating XML tag.</param>
        public XmlNode ( string tagName, string content ) {
            this.TagName = tagName;
            this.WithContent ( content );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlNode"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the XML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating XML tag.</param>
        public XmlNode ( string tagName, Action<XmlNode> content ) {
            this.TagName = tagName;
            this.WithContent ( content );
        }

        /// <inheritdoc/>
        public static XmlNode operator + ( XmlNode a, XmlNode? b ) {
            if (b == null)
                return a;
            a.Children.Add ( b );
            return a;
        }

        /// <inheritdoc/>
        public static XmlNode operator + ( XmlNode a, object? b ) {
            if (b == null)
                return a;
            a.Children.Add ( b );
            return a;
        }

        /// <inheritdoc/>
        public static XmlNode operator + ( XmlNode a, string? b ) {
            if (b == null)
                return a;
            a.Children.Add ( new RenderableText ( b ) );
            return a;
        }

        /// <summary>
        /// Renders this <see cref="XmlNode"/> into it's XML string representation.
        /// </summary>
        public override string ToString () {
            StringBuilder sb = new StringBuilder ();

            if (!string.IsNullOrEmpty ( this.TagName )) {
                sb.Append ( '<' );
                sb.Append ( this.TagName );

                if (this.Attributes.Count > 0) {
                    foreach (KeyValuePair<string, object?> at in this.Attributes) {
                        if (string.IsNullOrEmpty ( at.Key ))
                            continue;

                        sb.Append ( ' ' );
                        sb.Append ( at.Key );

                        string value = RenderableText.SafeRenderSubject ( at.Value );
                        sb.Append ( '=' );
                        sb.Append ( '"' );
                        sb.Append ( value );
                        sb.Append ( '"' );
                    }
                }

                if (this.SelfClosing) {
                    sb.Append ( '/' );
                }
                sb.Append ( '>' );
            }

            if (this.SelfClosing) {
                return sb.ToString ();
            }

            foreach (object? children in this.Children) {
                sb.Append ( children?.ToString () );
            }

            sb.Append ( "</" );
            sb.Append ( this.TagName );
            sb.Append ( '>' );

            return sb.ToString ();
        }
    }
}
