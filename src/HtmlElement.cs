#define HELPER_METHODS_ENABLED

using System.Collections.Generic;
using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace TinyComponents
{
    /// <summary>
    /// Represents an HTML element for rendering
    /// </summary>
    public class HtmlElement : IRenderable
    {
#if HELPER_METHODS_ENABLED
        /// <summary>
        /// Represents the HTML &lt;title&gt; tag.
        /// </summary>
        /// <param name="content">The text contents of the tag.</param>
        /// <returns>An <see cref="HtmlElement" /> representation the title tag.</returns>
        public static HtmlElement Title(string content) => new HtmlElement("title").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;head&gt; tag.
        /// </summary>
        /// <param name="builder">Action to configure the head element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the head tag.</returns>
        public static HtmlElement Head(Action<HtmlElement> builder) => new HtmlElement("head").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;link&gt; tag, typically used for linking to external resources.
        /// </summary>
        /// <param name="href">The URL of the linked resource.</param>
        /// <param name="rel">The relationship between the current document and the linked resource.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the link tag.</returns>
        public static HtmlElement Link(string href, string rel) => new HtmlElement("link").SelfClosed().WithAttributes(new { href, rel });

        /// <summary>
        /// Represents the HTML &lt;meta&gt; tag, providing metadata about the HTML document.
        /// </summary>
        /// <param name="name">The name of the metadata.</param>
        /// <param name="content">The content of the metadata.</param>
        /// <param name="charset">Character set declaration.</param>
        /// <param name="httpEquiv">HTTP-equivalent header.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the meta tag.</returns>
        public static HtmlElement Meta(string? name = null, string? content = null, string? charset = null, string? httpEquiv = null) => new HtmlElement("meta").SelfClosed().WithAttributes(new { name, content, charset, httpEquiv });

        /// <summary>
        /// Represents an HTML &lt;style&gt; tag containing CSS.
        /// </summary>
        /// <param name="css">The CSS content.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the style tag with CSS.</returns>
        public static HtmlElement CssStyle(string css) => new HtmlElement("style").WithContent(new Text(css, false));

        /// <summary>
        /// Represents an HTML &lt;script&gt; tag containing JavaScript.
        /// </summary>
        /// <param name="script">The JavaScript code.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the script tag with JavaScript.</returns>
        public static HtmlElement JsScript(string script) => new HtmlElement("script").WithContent(new Text(script, false));

        /// <summary>
        /// Represents an HTML &lt;script&gt; tag for linking an external JavaScript file.
        /// </summary>
        /// <param name="src">The source URL of the external script.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the script tag with the external source.</returns>
        public static HtmlElement JsExternalScript(string src) => new HtmlElement("script").WithAttributes(new { src });

        /// <summary>
        /// Represents the HTML &lt;div&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the div element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the div tag.</returns>
        public static HtmlElement Div(Action<HtmlElement> builder) => new HtmlElement("div").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;div&gt; tag with content from an IRenderable.
        /// </summary>
        /// <param name="content">The IRenderable content to be placed inside the div.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the div tag with the specified content.</returns>
        public static HtmlElement Div(IRenderable content) => new HtmlElement("div").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;div&gt; tag with text content.
        /// </summary>
        /// <param name="content">The text content to be placed inside the div.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the div tag with the specified text content.</returns>
        public static HtmlElement Div(string content) => new HtmlElement("div").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;article&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the article element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the article tag.</returns>
        public static HtmlElement Article(Action<HtmlElement> builder) => new HtmlElement("article").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;aside&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the aside element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the aside tag.</returns>
        public static HtmlElement Aside(Action<HtmlElement> builder) => new HtmlElement("aside").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;header&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the header element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the header tag.</returns>
        public static HtmlElement Header(Action<HtmlElement> builder) => new HtmlElement("header").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;footer&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the footer element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the footer tag.</returns>
        public static HtmlElement Footer(Action<HtmlElement> builder) => new HtmlElement("footer").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;body&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the body element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the body tag.</returns>
        public static HtmlElement Body(Action<HtmlElement> builder) => new HtmlElement("body").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;html&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the html element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the html tag.</returns>
        public static HtmlElement Html(Action<HtmlElement> builder) => new HtmlElement("html").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;main&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the main element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the main tag.</returns>
        public static HtmlElement Main(Action<HtmlElement> builder) => new HtmlElement("main").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;section&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the section element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the section tag.</returns>
        public static HtmlElement Section(Action<HtmlElement> builder) => new HtmlElement("section").WithContent(builder);

        /// <summary>
        /// Represents a custom HTML &lt;search&gt; element with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the search element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the custom search tag.</returns>
        public static HtmlElement Search(Action<HtmlElement> builder) => new HtmlElement("search").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;nav&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the navigation element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the nav tag.</returns>
        public static HtmlElement Nav(Action<HtmlElement> builder) => new HtmlElement("nav").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;hgroup&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the header group element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the header group tag.</returns>
        public static HtmlElement HeaderGroup(Action<HtmlElement> builder) => new HtmlElement("hgroup").WithContent(builder);

        /// <summary>
        /// Represents a non-specific container for HTML content with configurable content via a builder action.
        /// This is typically used for creating a fragment of HTML.
        /// </summary>
        /// <param name="builder">Action to configure the fragment.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the fragment.</returns>
        public static HtmlElement Fragment(Action<HtmlElement> builder) => new HtmlElement("").WithContent(builder);

        /// <summary>
        /// Represents an empty non-specific container for HTML content. This can be used as a placeholder or container for dynamic content.
        /// </summary>
        /// <returns>An <see cref="HtmlElement" /> representing the empty fragment.</returns>
        public static HtmlElement Fragment() => new HtmlElement("");

        /// <summary>
        /// Represents the HTML &lt;img&gt; tag for embedding an image, with source and optional alt text.
        /// </summary>
        /// <param name="src">The URL of the image.</param>
        /// <param name="alt">Optional alternative text for the image.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the img tag.</returns>
        public static HtmlElement Image(string src, string? alt = null) => new HtmlElement("img").SelfClosed().WithAttributes(new { src, alt });

        /// <summary>
        /// Represents the HTML &lt;video&gt; tag for embedding a video file.
        /// </summary>
        /// <param name="src">The URL of the video file.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the video tag.</returns>
        public static HtmlElement Video(string? src) => new HtmlElement("video").WithAttributes(new { src });

        /// <summary>
        /// Represents the HTML &lt;source&gt; tag for specifying multiple media resources for video and audio elements.
        /// </summary>
        /// <param name="src">The URL of the media source.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the source tag.</returns>
        public static HtmlElement Source(string src) => new HtmlElement("source").SelfClosed().WithAttributes(new { src });

        /// <summary>
        /// Represents the HTML &lt;p&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the paragraph element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the paragraph tag.</returns>
        public static HtmlElement Paragraph(Action<HtmlElement> builder) => new HtmlElement("p").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;p&gt; tag with content from an IRenderable.
        /// </summary>
        /// <param name="content">The IRenderable content to be placed inside the paragraph.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the paragraph tag with the specified content.</returns>
        public static HtmlElement Paragraph(IRenderable content) => new HtmlElement("p").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;p&gt; tag with text content.
        /// </summary>
        /// <param name="content">The text content to be placed inside the paragraph.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the paragraph tag with the specified text content.</returns>
        public static HtmlElement Paragraph(string content) => new HtmlElement("p").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;li&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the list item element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the list item tag.</returns>
        public static HtmlElement Li(Action<HtmlElement> builder) => new HtmlElement("li").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;li&gt; tag with content from an IRenderable.
        /// </summary>
        /// <param name="content">The IRenderable content to be placed inside the list item.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the list item tag with the specified content.</returns>
        public static HtmlElement Li(IRenderable content) => new HtmlElement("li").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;li&gt; tag with text content.
        /// </summary>
        /// <param name="content">The text content to be placed inside the list item.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the list item tag with the specified text content.</returns>
        public static HtmlElement Li(string content) => new HtmlElement("li").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;span&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the span element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the span tag.</returns>
        public static HtmlElement Span(Action<HtmlElement> builder) => new HtmlElement("span").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;span&gt; tag with content from an IRenderable.
        /// </summary>
        /// <param name="content">The IRenderable content to be placed inside the span.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the span tag with the specified content.</returns>
        public static HtmlElement Span(IRenderable content) => new HtmlElement("span").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;span&gt; tag with text content.
        /// </summary>
        /// <param name="content">The text content to be placed inside the span.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the span tag with the specified text content.</returns>
        public static HtmlElement Span(string content) => new HtmlElement("span").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;strong&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the strong element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the strong tag.</returns>
        public static HtmlElement Strong(Action<HtmlElement> builder) => new HtmlElement("strong").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;strong&gt; tag with text content.
        /// </summary>
        /// <param name="content">The text content to be emphasized as strong.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the strong tag with the specified text content.</returns>
        public static HtmlElement Strong(string content) => new HtmlElement("strong").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;label&gt; tag with configurable content via a builder action.
        /// </summary>
        /// <param name="builder">Action to configure the label element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the label tag.</returns>
        public static HtmlElement Label(Action<HtmlElement> builder) => new HtmlElement("label").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;label&gt; tag for defining a label for an &lt;input&gt; element.
        /// </summary>
        /// <param name="content">The text content of the label.</param>
        /// <param name="forId">Optional ID of the input element this label is associated with.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the label tag with the specified content and 'for' attribute.</returns>
        public static HtmlElement Label(string content, string? forId = "") => new HtmlElement("label").WithContent(content).WithAttribute("for", forId);

        /// <summary>
        /// Represents the HTML &lt;i&gt; tag with configurable content via a builder action, typically used for italicizing text.
        /// </summary>
        /// <param name="builder">Action to configure the italic element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the italic tag.</returns>
        public static HtmlElement Italic(Action<HtmlElement> builder) => new HtmlElement("i").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;i&gt; tag with text content, typically used for italicizing text.
        /// </summary>
        /// <param name="content">The text content to be italicized.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the italic tag with the specified text content.</returns>
        public static HtmlElement Italic(string content) => new HtmlElement("i").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;h1&gt; tag with text content, used for main headings.
        /// </summary>
        /// <param name="content">The text content for the heading.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the h1 tag with the specified content.</returns>
        public static HtmlElement H1(string content) => new HtmlElement("h1").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;h2&gt; tag with text content, used for subheadings.
        /// </summary>
        /// <param name="content">The text content for the subheading.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the h2 tag with the specified content.</returns>
        public static HtmlElement H2(string content) => new HtmlElement("h2").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;h3&gt; tag with text content, used for third-level headings.
        /// </summary>
        /// <param name="content">The text content for the heading.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the h3 tag with the specified content.</returns>
        public static HtmlElement H3(string content) => new HtmlElement("h3").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;h4&gt; tag with text content, used for fourth-level headings.
        /// </summary>
        /// <param name="content">The text content for the heading.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the h4 tag with the specified content.</returns>
        public static HtmlElement H4(string content) => new HtmlElement("h4").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;h5&gt; tag with text content, used for fifth-level headings.
        /// </summary>
        /// <param name="content">The text content for the heading.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the h5 tag with the specified content.</returns>
        public static HtmlElement H5(string content) => new HtmlElement("h5").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;h6&gt; tag with text content, used for sixth-level headings.
        /// </summary>
        /// <param name="content">The text content for the heading.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the h6 tag with the specified content.</returns>
        public static HtmlElement H6(string content) => new HtmlElement("h6").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;form&gt; tag with specified action, method, and configurable content via a builder action.
        /// </summary>
        /// <param name="action">The URL where the form data is sent when submitted.</param>
        /// <param name="method">The HTTP method (e.g., "get" or "post") used when submitting the form.</param>
        /// <param name="builder">Action to configure the form element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the form tag.</returns>
        public static HtmlElement Form(string action, string method, Action<HtmlElement> builder) => new HtmlElement("form").WithAttributes(new { action, method }).WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;a&gt; (anchor) tag with text content and an href attribute.
        /// </summary>
        /// <param name="href">The URL the link goes to.</param>
        /// <param name="text">The text displayed for the link.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the anchor tag with the specified text and href.</returns>
        public static HtmlElement Anchor(string href, string text) => new HtmlElement("a").WithContent(text).WithAttribute("href", href);

        /// <summary>
        /// Represents the HTML &lt;a&gt; (anchor) tag with an href attribute and configurable content via a builder action.
        /// </summary>
        /// <param name="href">The URL the link goes to.</param>
        /// <param name="builder">Action to configure the anchor element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the anchor tag with the specified href and custom content.</returns>
        public static HtmlElement Anchor(string href, Action<HtmlElement> builder) => new HtmlElement("a").WithContent(builder).WithAttribute("href", href);

        /// <summary>
        /// Represents the HTML &lt;blockquote&gt; tag with configurable content via a builder action, typically used for quotations.
        /// </summary>
        /// <param name="builder">Action to configure the blockquote element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the blockquote tag.</returns>
        public static HtmlElement Blockquote(Action<HtmlElement> builder) => new HtmlElement("blockquote").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;blockquote&gt; tag with text content, typically used for quotations.
        /// </summary>
        /// <param name="content">The text content of the quotation.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the blockquote tag with the specified content.</returns>
        public static HtmlElement Blockquote(string content) => new HtmlElement("blockquote").WithContent(content);

        /// <summary>
        /// Represents the HTML &lt;ul&gt; tag with configurable content via a builder action, used for unordered lists.
        /// </summary>
        /// <param name="builder">Action to configure the unordered list element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the unordered list tag.</returns>
        public static HtmlElement Ul(Action<HtmlElement> builder) => new HtmlElement("ul").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;ol&gt; tag with configurable content via a builder action, used for ordered lists.
        /// </summary>
        /// <param name="builder">Action to configure the ordered list element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the ordered list tag.</returns>
        public static HtmlElement Ol(Action<HtmlElement> builder) => new HtmlElement("ol").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;button&gt; tag with configurable content via a builder action, used for interactive buttons.
        /// </summary>
        /// <param name="builder">Action to configure the button element.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the button tag.</returns>
        public static HtmlElement Button(Action<HtmlElement> builder) => new HtmlElement("button").WithContent(builder);

        /// <summary>
        /// Represents the HTML &lt;button&gt; tag with text content, used for interactive buttons.
        /// </summary>
        /// <param name="content">The text content of the button.</param>
        /// <returns>An <see cref="HtmlElement" /> representing the button tag with the specified content.</returns>
        public static HtmlElement Button(string content) => new HtmlElement("button").WithContent(content);
#endif

        /// <summary>
        /// Gets or sets the tag name of the HTML element (e.g., "div", "span").
        /// </summary>
        public string Tag { get; set; } = "div";

        /// <summary>
        /// Gets or sets a value indicating whether the element is self-closing.
        /// </summary>
        public bool SelfClosing { get; set; } = false;

        /// <summary>
        /// Gets or sets the collection of HTML attributes for the element.
        /// </summary>
        public NameValueCollection Attributes { get; set; } = new NameValueCollection();

        /// <summary>
        /// Gets or sets the collection of child elements (IRenderable) within this element.
        /// </summary>
        public ICollection<IRenderable> Children { get; set; } = new List<IRenderable>();

        /// <summary>
        /// Gets or sets the tab index of the HTML element.
        /// </summary>
        public int? TabIndex { get; set; }

        /// <summary>
        /// Gets or sets the tooltip text to display for the HTML element. Null if not set.
        /// </summary>
        public string? TooltipTitle { get; set; }

        /// <summary>
        /// Gets or sets the ID attribute of the HTML element. Null if not set.
        /// Used to uniquely identify the element within the page.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name attribute of the HTML element. Null if not set.
        /// The name is used to reference elements in JavaScript, or to reference form data after a form is submitted.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the style attribute for the HTML element as an object. Null if not set.
        /// This can be used to apply inline styles directly to the element.
        /// </summary>
        public object? Style { get; set; }

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
            Tag = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        public HtmlElement(string tagName)
        {
            Tag = tagName.ToLower();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, IRenderable content)
        {
            Tag = tagName.ToLower();
            WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, Action<HtmlElement> content)
        {
            Tag = tagName.ToLower();
            WithContent(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlElement"/> class with the specified tag name.
        /// </summary>
        /// <param name="tagName">The name of the tag to be used for the HTML element. The tag name will be converted to lowercase.</param>
        /// <param name="content">Optional parameter that defines content for the creating HTML tag.</param>
        public HtmlElement(string tagName, string? content)
        {
            Tag = tagName.ToLower();
            WithContent(content);
        }

        /// <summary>
        /// Configures the current element to be self-closing.
        /// </summary>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement SelfClosed()
        {
            SelfClosing = true;
            return this;
        }

        /// <summary>
        /// Applies a specified action to the current <see cref="HtmlElement"/> instance, allowing for custom configuration.
        /// </summary>
        /// <param name="handler">The action to apply to this instance. It allows configuring the element's properties and states.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithContent(Action<HtmlElement> handler)
        {
            handler(this);
            return this;
        }

        /// <summary>
        /// Adds a child IRenderable to the current element's children.
        /// </summary>
        /// <param name="children">The IRenderable child to add.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithContent(IRenderable children)
        {
            Children.Add(children);
            return this;
        }

        /// <summary>
        /// Adds text content to the current element's children.
        /// </summary>
        /// <param name="contents">The text to add as a child.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithContent(string? contents)
        {
            Children.Add(new Text(contents));
            return this;
        }

        /// <summary>
        /// Sets the ID attribute of the current element.
        /// </summary>
        /// <param name="id">The ID to set.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// Sets the name attribute of the current element.
        /// </summary>
        /// <param name="name">The name to set.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// Sets the inline style of the current element.
        /// </summary>
        /// <param name="style">The style object to apply.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithStyle(object style)
        {
            Style = style;
            return this;
        }

        /// <summary>
        /// Adds an attribute to the current element without a value.
        /// </summary>
        /// <param name="key">The key of the attribute to add.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithAttribute(string key)
        {
            Attributes.Add(key, "");
            return this;
        }

        /// <summary>
        /// Adds or updates an attribute of the current element with the specified value.
        /// </summary>
        /// <param name="key">The key of the attribute.</param>
        /// <param name="value">The value of the attribute. If Boolean and true, adds the attribute without value.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithAttribute(string key, object? value)
        {
            if (value == null) return this;
            if (value is Boolean b)
            {
                if (b == true)
                    Attributes.Add(key, "");
            }
            else
            {
                Attributes.Add(key, value.ToString());
            }
            return this;
        }

        /// <summary>
        /// Adds one or more CSS class names to the current element's class list.
        /// </summary>
        /// <param name="classNames">The class names to add.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithClass(params string[] classNames)
        {
            foreach (string className in classNames) ClassList.Add(className);
            return this;
        }

        /// <summary>
        /// Adds multiple attributes to the current element from an object.
        /// </summary>
        /// <param name="attributes">The object from which to parse and add attributes.</param>
        /// <returns>The current <see cref="HtmlElement"/> instance for fluent chaining.</returns>
        public HtmlElement WithAttributes(object attributes)
        {
            GetAttributesFromObject(attributes);
            return this;
        }

        void RenderChildren(StringBuilder sb)
        {
            foreach (IRenderable HtmlElement in Children)
            {
                string? render = HtmlElement.Render();
                sb.Append(render);
            }
        }

        string BuildAttributes()
        {
            NameValueCollection clone = new NameValueCollection(Attributes);
            if (TabIndex != null) clone.Add("tabindex", TabIndex.ToString());
            if (TooltipTitle != null) clone.Add("title", TooltipTitle.ToString());
            if (Id != null) clone.Add("id", Id.ToString());
            if (Name != null) clone.Add("name", Name.ToString());
            if (ClassList?.Count > 0) clone.Add("class", string.Join(' ', ClassList));
            if (Style != null) clone.Add("style", Stylesheet.FromObject(Style).Render()?.ToString());

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
                else if (value == name || value == "")
                {
                    attributeList.Add($"{name}");
                }
                else if (value.Contains('"'))
                {
                    attributeList.Add($"{name}='{value}'");
                }
                else
                {
                    attributeList.Add($"{name}=\"{value}\"");
                }
            }
            return string.Join(' ', attributeList);
        }

        void GetAttributesFromObject(object obj)
        {
            foreach (PropertyInfo pinfo in obj.GetType().GetProperties())
            {
                string name = pinfo.Name;
                object? value = pinfo.GetValue(obj);
                WithAttribute(name, value);
            }
        }

        public static HtmlElement operator +(HtmlElement a, HtmlElement? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        public static HtmlElement operator +(HtmlElement a, IRenderable? b)
        {
            if (b == null) return a;
            a.Children.Add(b);
            return a;
        }

        public static HtmlElement operator +(HtmlElement a, string? b)
        {
            if (b == null) return a;
            a.Children.Add(new Text(b, false));
            return a;
        }

        /// <summary>
        /// Renders the HTML element into string.
        /// </summary>
        /// <returns>The rendered HTML string.</returns>
        public virtual string? Render()
        {
            StringBuilder sb = new StringBuilder();
            string attributes = BuildAttributes();

            if (Tag == "")
            {
                // is an fragment tag
                if (attributes.Length > 0) throw new InvalidOperationException("Cannot define attributes in a fragment element.");

                RenderChildren(sb);
                return sb.ToString();
            }

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
    }

}