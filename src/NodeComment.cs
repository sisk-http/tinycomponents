namespace TinyComponents {

    /// <summary>
    /// Represents a node comment in a document.
    /// </summary>
    public sealed class NodeComment {
        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeComment"/> class with the specified comment.
        /// </summary>
        /// <param name="comment">The comment text.</param>
        public NodeComment ( string comment ) {
            this.Comment = comment;
        }

        /// <summary>
        /// Returns a string representation of the comment in the format of an XML comment.
        /// </summary>
        /// <returns>A string representation of the comment.</returns>
        public override string ToString () {
            return $"<!-- {this.Comment.Trim ()} -->";
        }
    }
}
