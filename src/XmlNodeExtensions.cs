using System;
using System.Collections.Generic;
using System.Text;

namespace TinyComponents
{
    /// <summary>
    /// Provides extension methods for <see cref="IXmlNode"/> objects.
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Specifies that this <see cref="IXmlNode"/> will be self-closed.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="IXmlNode"/>.</typeparam>
        /// <param name="node">The current <see cref="IXmlNode"/>.</param>
        /// <returns>The self <see cref="IXmlNode"/> object for fluent chaining.</returns>
        public static TXmlNode SelfClosed<TXmlNode>(this TXmlNode node) where TXmlNode : IXmlNode
        {
            node.SelfClosing = true;
            return node;
        }

        /// <summary>
        /// Specifies an attribute for this <see cref="IXmlNode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="IXmlNode"/>.</typeparam>
        /// <param name="node">The current <see cref="IXmlNode"/>.</param>
        /// <param name="value">The attribute name and value.</param>
        /// <returns>The self <see cref="IXmlNode"/> object for fluent chaining.</returns>
        public static TXmlNode WithAttribute<TXmlNode>(this TXmlNode node, string value) where TXmlNode : IXmlNode
        {
            node.Attributes[value] = value;
            return node;
        }

        /// <summary>
        /// Specifies an attribute for this <see cref="IXmlNode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="IXmlNode"/>.</typeparam>
        /// <param name="node">The current <see cref="IXmlNode"/>.</param>
        /// <param name="name">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>The self <see cref="IXmlNode"/> object for fluent chaining.</returns>
        public static TXmlNode WithAttribute<TXmlNode>(this TXmlNode node, string name, object? value) where TXmlNode : IXmlNode
        {
            node.Attributes[name] = value;
            return node;
        }

        /// <summary>
        /// Specifies children contents for this <see cref="IXmlNode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="IXmlNode"/>.</typeparam>
        /// <param name="node">The current <see cref="IXmlNode"/>.</param>
        /// <param name="selector">The action which will be executed in the self <see cref="IXmlNode"/>.</param>
        /// <returns>The self <see cref="IXmlNode"/> object for fluent chaining.</returns>
        public static TXmlNode WithContent<TXmlNode>(this TXmlNode node, Action<TXmlNode> selector) where TXmlNode : IXmlNode
        {
            selector(node);
            return node;
        }

        /// <summary>
        /// Specifies children contents for this <see cref="IXmlNode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="IXmlNode"/>.</typeparam>
        /// <param name="node">The current <see cref="IXmlNode"/>.</param>
        /// <param name="children">The children <see cref="IRenderable"/> object.</param>
        /// <returns>The self <see cref="IXmlNode"/> object for fluent chaining.</returns>
        public static TXmlNode WithContent<TXmlNode>(this TXmlNode node, IRenderable children) where TXmlNode : IXmlNode
        {
            node.Children.Add(children);
            return node;
        }

        /// <summary>
        /// Specifies children contents for this <see cref="IXmlNode"/>.
        /// </summary>
        /// <typeparam name="TXmlNode">The object type which implements <see cref="IXmlNode"/>.</typeparam>
        /// <param name="node">The current <see cref="IXmlNode"/>.</param>
        /// <param name="contents">The children object.</param>
        /// <returns>The self <see cref="IXmlNode"/> object for fluent chaining.</returns>
        public static TXmlNode WithContent<TXmlNode>(this TXmlNode node, object? contents) where TXmlNode : IXmlNode
        {
            if (contents is null)
            {
                ;
            }
            else if (contents is IRenderable renderable)
            {
                node.Children.Add(renderable);
            }
            else
            {
                node.Children.Add(new RenderableText(contents.ToString()));
            }
            return node;
        }
    }
}
