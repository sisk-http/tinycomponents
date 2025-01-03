using System;
using System.IO;

namespace TinyComponents.Serializer {

    /// <summary>
    /// Represents a writer for nodes in a serialized format.
    /// </summary>
    public class NodeWriter : IDisposable {

        private TextWriter _writer;
        int indentLevel;

        /// <summary>
        /// Gets or sets a value indicating whether to write the output in an indented format.
        /// </summary>
        public bool WriteIndented { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to write tag names in lowercase.
        /// </summary>
        public bool TagNamesLowercase { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to include a closing slash in self-closing tags.
        /// </summary>
        public bool TagCloseSlash { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to repeat identical attributes.
        /// </summary>
        public bool RepeatIdenticAttributes { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeWriter"/> class with the specified writer.
        /// </summary>
        /// <param name="writer">The writer to use for output.</param>
        public NodeWriter ( TextWriter writer ) {
            this._writer = writer;
        }

        /// <summary>
        /// Configures the writer to use HTML styles.
        /// </summary>
        /// <param name="indented">Whether to write the output in an indented format. Defaults to true.</param>
        public void UseHTMLStyles ( bool indented = true ) {
            this.TagNamesLowercase = true;
            this.TagCloseSlash = false;
            this.RepeatIdenticAttributes = false;
            this.WriteIndented = indented;
        }

        /// <summary>
        /// Configures the writer to use XML styles.
        /// </summary>
        /// <param name="indented">Whether to write the output in an indented format. Defaults to true.</param>
        public void UseXMLStyles ( bool indented = true ) {
            this.TagNamesLowercase = false;
            this.TagCloseSlash = true;
            this.RepeatIdenticAttributes = true;
            this.WriteIndented = indented;
        }

        /// <summary>
        /// Writes the specified object to the output.
        /// </summary>
        /// <param name="obj">The object to write.</param>
        public void Write ( object? obj ) {
            if (obj is null)
                return;

            if (obj is INode node) {
                this.WriteNode ( node );
            }
            else {
                this._writer.Write ( obj.ToString () );
            }
        }

        /// <summary>
        /// Writes the specified node to the output.
        /// </summary>
        /// <param name="node">The node to write.</param>
        protected void WriteNode ( INode node ) {
            this.WriteNodeOpenTag ( node );

            this.WriteLineBreak ();
            this.indentLevel++;

            foreach (object? child in node.Children) {
                this.WriteIndent ();
                this.Write ( child );
                this.WriteLineBreak ();
            }

            this.indentLevel--;
            this.WriteIndent ();
            this.WriteNodeCloseTag ( node );
        }

        /// <summary>
        /// Writes a line break to the output if <see cref="WriteIndented"/> is true.
        /// </summary>
        protected void WriteLineBreak () {
            if (this.WriteIndented) {
                this._writer.WriteLine ();
            }
        }

        /// <summary>
        /// Writes an indent to the output if <see cref="WriteIndented"/> is true.
        /// </summary>
        protected void WriteIndent () {
            if (this.WriteIndented) {
                this._writer.Write ( new string ( ' ', this.indentLevel * 4 ) );
            }
        }

        /// <summary>
        /// Writes the closing tag for the specified node to the output.
        /// </summary>
        /// <param name="node">The node to write the closing tag for.</param>
        protected void WriteNodeCloseTag ( INode node ) {
            if (node.SelfClosing)
                return;

            this._writer.Write ( "</" );
            if (this.TagNamesLowercase) {
                this._writer.Write ( node.TagName.ToLower () );
            }
            else {
                this._writer.Write ( node.TagName );
            }

            this._writer.Write ( '>' );
        }

        /// <summary>
        /// Writes the opening tag for the specified node to the output.
        /// </summary>
        /// <param name="node">The node to write the opening tag for.</param>
        protected void WriteNodeOpenTag ( INode node ) {
            this._writer.Write ( '<' );
            if (this.TagNamesLowercase) {
                this._writer.Write ( node.TagName.ToLower () );
            }
            else {
                this._writer.Write ( node.TagName );
            }

            foreach (var at in node.Attributes) {
                if (string.IsNullOrEmpty ( at.Key ))
                    continue;

                this._writer.Write ( ' ' );
                this._writer.Write ( at.Key );

                string value = RenderableText.SafeRenderSubject ( at.Value );
                if (this.RepeatIdenticAttributes && string.Compare ( value, at.Key ) == 0) {
                    continue;
                }
                else {
                    this._writer.Write ( '=' );
                    this._writer.Write ( '"' );
                    this._writer.Write ( value );
                    this._writer.Write ( '"' );
                }
            }

            if (this.TagCloseSlash && node.SelfClosing) {
                this._writer.Write ( '/' );
            }

            this._writer.Write ( '>' );
        }

        /// <summary>
        /// Releases held by the writer.
        /// </summary>
        public void Dispose () {
            this._writer.Dispose ();
        }
    }
}
