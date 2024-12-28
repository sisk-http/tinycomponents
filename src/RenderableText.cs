using System;
using System.Text;

namespace TinyComponents {
    /// <summary>
    /// Represents an simple renderable text.
    /// </summary>
    public sealed class RenderableText {
        /// <summary>
        /// Gets or sets whether this text should be XML/HTML encoded or not.
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
        public static RenderableText Raw ( object? contents )
            => new RenderableText ( contents, false );

        /// <summary>
        /// Creates an new instance of <see cref="RenderableText"/> with the specified text
        /// contents from the object, encoding it as an HTML entity.
        /// </summary>
        /// <param name="textContents">The object which will be encoded.</param>
        public RenderableText ( object? textContents ) {
            this.Contents = textContents;
        }

        /// <summary>
        /// Creates an new instance of <see cref="RenderableText"/> with the specified text
        /// contents from the object.
        /// </summary>
        /// <param name="textContents">The object which will be encoded.</param>
        /// <param name="escape">Determines if the text contents should be HTML encoded or not.</param>
        public RenderableText ( object? textContents, bool escape ) {
            this.Contents = textContents;
            this.Escape = escape;
        }

        /// <summary>
        /// Renders this <see cref="RenderableText"/> into an string.
        /// </summary>
        /// <returns>The string representation of this <see cref="RenderableText"/>.</returns>
        public override string ToString () {
            if (this.Escape) {
                return SafeRenderSubject ( this.Contents?.ToString () );
            }
            else {
                return this.Contents?.ToString () ?? "";
            }
        }

        /// <summary>
        /// Renders the specified object into an safe HTML content.
        /// </summary>
        /// <param name="obj">The object to be rendered.</param>
        public static string SafeRenderSubject ( object? obj ) {
            if (obj is null) {
                return string.Empty;
            }
            else if (obj is RenderableText rt) {
                return rt.ToString ();
            }
            else {
                string? literal = obj.ToString ();

                if (literal is null)
                    return string.Empty;

                StringBuilder sb = new StringBuilder ( literal.Length );
                ReadOnlySpan<char> rs = literal.ToCharArray ();

                for (int i = 0; i < rs.Length; i++) {
                    char c = rs [ i ];
                    switch (c) {
                        case '"':
                            sb.Append ( "&quot;" );
                            break;

                        case '\'':
                            sb.Append ( "&apos;" );
                            break;

                        case '<':
                            sb.Append ( "&lt;" );
                            break;

                        case '>':
                            sb.Append ( "&gt;" );
                            break;

                        case '&':
                            sb.Append ( "&amp;" );
                            break;

                        default:
                            sb.Append ( c );
                            break;
                    }
                }

                return sb.ToString ();
            }
        }
    }
}