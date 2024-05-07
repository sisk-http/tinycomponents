using System;
using System.Text;
using System.Web;

namespace TinyComponents
{
    /// <summary>
    /// Represents an simple renderable text.
    /// </summary>
    public class RenderableText : IRenderable
    {
        /// <summary>
        /// Gets or stets whether this text should be XML/HTML encoded or not.
        /// </summary>
        public bool Escape { get; set; } = true;

        /// <summary>
        /// Gets or sets the contents which this text will render.
        /// </summary>
        public object? Contents { get; set; }

        /// <summary>
        /// Creates an new instance of <see cref="RenderableText"/> with the provided raw, unencoded text.
        /// </summary>
        /// <param name="contents">The object which will be converted to text.</param>
        /// <returns>An <see cref="RenderableText"/> with the content value.</returns>
        public static RenderableText Raw(object? contents)
            => new RenderableText(contents, false);

        /// <summary>
        /// Creates an new instance of <see cref="RenderableText"/> with the specified text
        /// contents from the object, encoding it as an HTML entity.
        /// </summary>
        /// <param name="textContents">The object which will be encoded.</param>
        public RenderableText(object? textContents)
        {
            Contents = textContents;
        }

        /// <summary>
        /// Creates an new instance of <see cref="RenderableText"/> with the specified text
        /// contents from the object.
        /// </summary>
        /// <param name="textContents">The object which will be encoded.</param>
        /// <param name="escape">Determines if the text contents should be HTML encoded or not.</param>
        public RenderableText(object? textContents, bool escape)
        {
            Contents = textContents;
            Escape = escape;
        }

        /// <summary>
        /// Renders this <see cref="RenderableText"/> into an string.
        /// </summary>
        /// <returns>The string representation of this <see cref="RenderableText"/>.</returns>
        public String Render()
        {
            if (Escape)
            {
                return EscapeXmlLikeLiteral(Contents?.ToString());
            }
            else
            {
                return Contents?.ToString() ?? "";
            }
        }

        /// <summary>
        /// Escapes the specified XML compatible value.
        /// </summary>
        /// <param name="literal">The XML or HTML value.</param>
        public static string EscapeXmlLikeLiteral(string? literal)
        {
            if (literal is null) return "";
            StringBuilder sb = new StringBuilder(literal.Length);
            ReadOnlySpan<char> rs = literal.ToCharArray();
            for (int i = 0; i < rs.Length; i++)
            {
                char c = rs[i];
                switch (c)
                {
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '\'':
                        sb.Append("&apos;");
                        break;
                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }

}