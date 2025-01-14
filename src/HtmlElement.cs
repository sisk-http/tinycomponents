﻿//#define HELPER_METHODS_ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TinyComponents {
    /// <summary>
    /// Represents an HTML element for rendering
    /// </summary>
    public class HtmlElement : INode {

        static Regex EmmetTagPattern = new Regex ( @"^([a-zA-Z][\w-]*)", RegexOptions.Compiled );
        static Regex EmmetIdPattern = new Regex ( @"#([\w-]+)", RegexOptions.Compiled );
        static Regex EmmetClassPattern = new Regex ( @"\.([\w-]+)", RegexOptions.Compiled );
        static Regex EmmetAttrPattern = new Regex ( @"\[([^\]=]+)(?:=([^\]]+))?\]", RegexOptions.Compiled );

        /// <summary>
        /// Formats the specified HTML string format, escaping the string interpolation pieces.
        /// </summary>
        /// <param name="htmlString">The instance of <see cref="FormattableString"/>.</param>
        public static string Format ( FormattableString htmlString ) {
            string [] formattedData = new string [ htmlString.ArgumentCount ];
            for (int i = 0; i < formattedData.Length; i++) {
                formattedData [ i ] = RenderableText.SafeRenderSubject ( htmlString.GetArgument ( i )?.ToString () );
            }
            return string.Format ( htmlString.Format, formattedData );
        }

        /// <summary>
        /// Creates an fragment <see cref="HtmlElement"/> with specified children.
        /// </summary>
        /// <param name="children">An array of objects to put as children of the creating fragment.</param>
        public static HtmlElement Fragment ( params object? [] children ) {
            return new HtmlElement ( "", fragment => {
                foreach (object? child in children)
                    fragment.Children.Add ( child );
            } );
        }

        /// <summary>
        /// Creates an <see cref="HtmlElement"/> from the specified emmet template.
        /// </summary>
        /// <param name="emmetString"></param>
        /// <returns></returns>
        public static HtmlElement FromEmmet ( string emmetString ) {
            var result = new HtmlElement ( "div" );

            var tagMatch = EmmetTagPattern.Match ( emmetString );
            if (tagMatch.Success) {
                result.TagName = tagMatch.Groups [ 1 ].Value;
            }

            var idMatch = EmmetIdPattern.Match ( emmetString );
            if (idMatch.Success) {
                result.Id = idMatch.Groups [ 1 ].Value;
            }

            foreach (Match classMatch in EmmetClassPattern.Matches ( emmetString )) {
                result.ClassList.Add ( classMatch.Groups [ 1 ].ToString () );
            }

            foreach (Match attrMatch in EmmetAttrPattern.Matches ( emmetString )) {
                string key = attrMatch.Groups [ 1 ].Value.Trim ();
                string value = attrMatch.Groups [ 2 ].Value.Trim ();
                if (value == string.Empty) {
                    value = key;
                }
                result.Attributes [ key ] = value;
            }

            return result;
        }

        /// <summary>
        /// Creates an <see cref="HtmlElement"/> from the specified emmet template and adds the specified children.
        /// </summary>
        /// <param name="emmetString">The emmet template string.</param>
        /// <param name="children">An array of objects to put as children of the creating element.</param>
        /// <returns>A new <see cref="HtmlElement"/> based on the emmet template with the specified children.</returns>
        public static HtmlElement FromEmmet ( string emmetString, params object? [] children ) {
            return FromEmmet ( emmetString ).WithContent ( children );
        }

        /// <summary>
        /// Creates an <see cref="HtmlElement"/> from the specified emmet template and configures it using the specified action.
        /// </summary>
        /// <param name="emmetString">The emmet template string.</param>
        /// <param name="self">An action to configure the created <see cref="HtmlElement"/>.</param>
        /// <returns>A new <see cref="HtmlElement"/> based on the emmet template and configured using the specified action.</returns>
        public static HtmlElement FromEmmet ( string emmetString, Action<HtmlElement> self ) {
            return FromEmmet ( emmetString ).WithContent ( self );
        }

        /// <summary>
        /// Creates an fragment <see cref="HtmlElement"/> with specified self-action.
        /// </summary>
        /// <param name="action">An action that defines content for the creating HTML element.</param>
        public static HtmlElement Fragment ( Action<HtmlElement> action ) {
            return new HtmlElement ( "", action );
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
        public NodeAttributeCollection Attributes { get; set; } = new NodeAttributeCollection ();

        /// <summary>
        /// Gets or sets the collection of child elements within this element.
        /// </summary>
        public ICollection<object?> Children { get; set; } = new List<object?> ();

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
        public ICollection<string> ClassList { get; set; } = new List<string> ();

        /// <summary>
        /// Gets or sets the CSS style object used to render the style attribute.
        /// </summary>
        public object? Style { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with no container HTML element.
        /// </summary>
        public HtmlElement () {
            this.TagName = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        public HtmlElement ( string tagName ) {
            this.TagName = tagName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name and content.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement ( string tagName, object? content ) {
            this.TagName = tagName;
            this.WithContent ( content );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name and content.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement ( string tagName, string? content ) {
            this.TagName = tagName;
            this.WithContent ( content );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement ( string tagName, Action<HtmlElement> content ) {
            this.TagName = tagName;
            this.WithContent ( content );
        }

        /// <inheritdoc/>
        public static HtmlElement operator + ( HtmlElement a, object? b ) {
            if (b == null)
                return a;
            a.Children.Add ( b );
            return a;
        }

        /// <summary>
        /// Represents the protected method which gets the attributes to be rendered.
        /// </summary>
        protected virtual IDictionary<string, object?> GetAttributes () {
            Dictionary<string, object?> attributes = new Dictionary<string, object?> ( this.Attributes, StringComparer.OrdinalIgnoreCase );

            if (this.TabIndex is int tabindex)
                attributes [ "tabindex" ] = tabindex.ToString ();
            if (this.TooltipTitle is string title)
                attributes [ "title" ] = title;
            if (this.Id is string id)
                attributes [ "id" ] = id;
            if (this.Name is string name)
                attributes [ "name" ] = name;
            if (this.ClassList.Count > 0)
                attributes [ "class" ] = string.Join ( ' ', this.ClassList );

            if (this.GetStyleValue () is string styleValue) {
                attributes [ "style" ] = styleValue;
            }

            return attributes;
        }

        /// <summary>
        /// Renders the HTML element into a string with optional pretty formatting.
        /// </summary>
        /// <returns>The rendered HTML string.</returns>
        public override string ToString () {
            StringBuilder sb = new StringBuilder ();

            if (!string.IsNullOrEmpty ( this.TagName )) {
                sb.Append ( '<' );
                sb.Append ( this.TagName.ToLower () );

                var attr = this.GetAttributes ();
                if (attr.Count > 0) {
                    foreach (KeyValuePair<string, object?> at in attr) {
                        if (string.IsNullOrEmpty ( at.Key ))
                            continue;

                        sb.Append ( ' ' );
                        sb.Append ( at.Key );

                        string value = RenderableText.SafeRenderSubject ( at.Value );
                        if (string.Compare ( value, at.Key ) == 0) {
                            continue;
                        }
                        else {
                            sb.Append ( '=' );
                            sb.Append ( '"' );
                            sb.Append ( value );
                            sb.Append ( '"' );
                        }
                    }
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

        private string? GetStyleValue () {
            if (this.Style is null)
                return null;

            PropertyInfo [] properties = this.Style.GetType ()
                .GetProperties ( BindingFlags.Instance | BindingFlags.Public );

            StringBuilder stylesSb = new StringBuilder ();
            for (int i = 0; i < properties.Length; i++) {
                PropertyInfo propertyInfo = properties [ i ];
                object? value = propertyInfo.GetValue ( this.Style );
                string? valueStr = value?.ToString ();

                if (string.IsNullOrWhiteSpace ( valueStr )) {
                    continue;
                }
                else {
                    string name = string.Concat (
                        propertyInfo.Name
                            .Select ( ( x, i ) => i > 0 && char.IsUpper ( x ) ? "-" + char.ToLower ( x ) : char.ToLower ( x ).ToString () )
                    );
                    stylesSb.Append ( $"{name}:{valueStr};" );
                }
            }

            return stylesSb.ToString ();
            ;
        }
    }
}