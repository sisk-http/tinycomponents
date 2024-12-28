using System;

namespace TinyComponents {
    /// <summary>
    /// Provides extension methods for <see cref="INode"/> objects.
    /// </summary>
    public static class XmlNodeExtensions {
        /// <summary>
        /// Specifies that this <see cref="INode"/> will be self-closed.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="INode"/>.</typeparam>
        /// <param name="node">The current <see cref="INode"/>.</param>
        /// <returns>The self <see cref="INode"/> object for fluent chaining.</returns>
        public static TXmlNode SelfClosed<TXmlNode> ( this TXmlNode node ) where TXmlNode : INode {
            node.SelfClosing = true;
            return node;
        }

        /// <summary>
        /// Specifies an attribute for this <see cref="INode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="INode"/>.</typeparam>
        /// <param name="node">The current <see cref="INode"/>.</param>
        /// <param name="value">The attribute name and value.</param>
        /// <returns>The self <see cref="INode"/> object for fluent chaining.</returns>
        public static TXmlNode WithAttribute<TXmlNode> ( this TXmlNode node, string value ) where TXmlNode : INode {
            node.Attributes [ value ] = value;
            return node;
        }

        /// <summary>
        /// Specifies an attribute for this <see cref="INode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="INode"/>.</typeparam>
        /// <param name="node">The current <see cref="INode"/>.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The self <see cref="INode"/> object for fluent chaining.</returns>
        public static TXmlNode WithAttribute<TXmlNode> ( this TXmlNode node, string name, object? value ) where TXmlNode : INode {
            node.Attributes [ name ] = value;
            return node;
        }

        /// <summary>
        /// Specifies children contents for this <see cref="INode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="INode"/>.</typeparam>
        /// <param name="node">The current <see cref="INode"/>.</param>
        /// <param name="selector">The action which will be executed in the self <see cref="INode"/>.</param>
        /// <returns>The self <see cref="INode"/> object for fluent chaining.</returns>
        public static TXmlNode WithContent<TXmlNode> ( this TXmlNode node, Action<TXmlNode> selector ) where TXmlNode : INode {
            selector ( node );
            return node;
        }

        /// <summary>
        /// Specifies children contents for this <see cref="INode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="INode"/>.</typeparam>
        /// <param name="node">The current <see cref="INode"/>.</param>
        /// <param name="contents">The children object.</param>
        /// <returns>The self <see cref="INode"/> object for fluent chaining.</returns>
        public static TXmlNode WithContent<TXmlNode> ( this TXmlNode node, object? contents ) where TXmlNode : INode {
            node.Children.Add ( contents );
            return node;
        }

        /// <summary>
        /// Specifies children contents for this <see cref="INode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="INode"/>.</typeparam>
        /// <param name="node">The current <see cref="INode"/>.</param>
        /// <param name="contents">The children object.</param>
        /// <returns>The self <see cref="INode"/> object for fluent chaining.</returns>
        public static TXmlNode WithContent<TXmlNode> ( this TXmlNode node, string? contents ) where TXmlNode : INode {
            node.Children.Add ( new RenderableText ( contents ) );
            return node;
        }
    }
}
