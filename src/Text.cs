using System;
using System.Web;

namespace TinyComponents
{
    /// <summary>
    /// Represents an simple renderable text.
    /// </summary>
    public class Text : IRenderable
    {
        /// <summary>
        /// Gets or stets whether this text should be HTML encoded or not.
        /// </summary>
        public bool Escape { get; set; } = true;

        /// <summary>
        /// Gets or sets the contents which this text will render.
        /// </summary>
        public object? Contents { get; set; }

        /// <summary>
        /// Creates an new instance of <see cref="Text"/> with the provided raw, unencoded text.
        /// </summary>
        /// <param name="contents">The object which will be converted to text.</param>
        /// <returns>An <see cref="Text"/> with the content value.</returns>
        public static Text Raw(object? contents)
            => new Text(contents, false);

        /// <summary>
        /// Creates an new instance of <see cref="Text"/> with the specified text
        /// contents from the object, encoding it as an HTML entity.
        /// </summary>
        /// <param name="textContents">The object which will be encoded.</param>
        public Text(object? textContents)
        {
            Contents = textContents;
        }

        /// <summary>
        /// Creates an new instance of <see cref="Text"/> with the specified text
        /// contents from the object.
        /// </summary>
        /// <param name="textContents">The object which will be encoded.</param>
        /// <param name="escape">Determines if the text contents should be HTML encoded or not.</param>
        public Text(object? textContents, bool escape)
        {
            Contents = textContents;
            Escape = escape;
        }

        /// <summary>
        /// Renders this <see cref="Text"/> into an string.
        /// </summary>
        /// <returns>The string representation of this <see cref="Text"/>.</returns>
        public String? Render()
        {
            if (Escape)
            {
                return HttpUtility.HtmlEncode(Contents);
            }
            else
            {
                return Contents?.ToString();
            }
        }
    }

}