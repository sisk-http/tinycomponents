﻿using System.Collections.Generic;

namespace TinyComponents {
    /// <summary>
    /// Represents an renderable node.
    /// </summary>
    public interface INode {
        /// <summary>
        /// Gets or sets the tag name of the XML node.
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this XML node is self-closing.
        /// </summary>
        public bool SelfClosing { get; set; }

        /// <summary>
        /// Gets or sets an collection of attributes for this node.
        /// </summary>
        public NodeAttributeCollection Attributes { get; set; }

        /// <summary>
        /// Gets or sets the collection of child elements (<see cref="object"/>) within this node.
        /// </summary>
        public ICollection<object?> Children { get; set; }

    }
}
