using System.Linq;
using System.Reflection;
using System.Text;

namespace TinyComponents
{
    /// <summary>
    /// Provides extension methods for <see cref="HtmlElement"/> objects.
    /// </summary>
    public static class HtmlElementExtensions
    {
        /// <summary>
        /// Specifies the HTML element ID of this <see cref="HtmlElement"/>.
        /// </summary>
        /// <typeparam name="THtmlElement">The object type which implements <see cref="HtmlElement"/>.</typeparam>
        /// <param name="node">The current <see cref="HtmlElement"/>.</param>
        /// <param name="id">The element ID.</param>
        /// <returns>The self <see cref="HtmlElement"/> object for fluent chaining.</returns>
        public static THtmlElement WithId<THtmlElement>(this THtmlElement node, string id) where THtmlElement : HtmlElement
        {
            node.Id = id;
            return node;
        }

        /// <summary>
        /// Specifies the HTML element name of this <see cref="HtmlElement"/>.
        /// </summary>
        /// <typeparam name="THtmlElement">The object type which implements <see cref="HtmlElement"/>.</typeparam>
        /// <param name="node">The current <see cref="HtmlElement"/>.</param>
        /// <param name="name">The element name.</param>
        /// <returns>The self <see cref="HtmlElement"/> object for fluent chaining.</returns>
        public static THtmlElement WithName<THtmlElement>(this THtmlElement node, string name) where THtmlElement : HtmlElement
        {
            node.Name = name;
            return node;
        }

        /// <summary>
        /// Specifies the HTML element class list of this <see cref="HtmlElement"/>.
        /// </summary>
        /// <typeparam name="THtmlElement">The object type which implements <see cref="HtmlElement"/>.</typeparam>
        /// <param name="node">The current <see cref="HtmlElement"/>.</param>
        /// <param name="classNames">One or more classes to add to this <see cref="HtmlElement"/>.</param>
        /// <returns>The self <see cref="HtmlElement"/> object for fluent chaining.</returns>
        public static THtmlElement WithClass<THtmlElement>(this THtmlElement node, params string[] classNames) where THtmlElement : HtmlElement
        {
            foreach (string className in classNames)
                node.ClassList.Add(className);
            return node;
        }

        /// <summary>
        /// Adds css styles through the style attribute on this <see cref="HtmlElement"/>.
        /// </summary>
        /// <typeparam name="THtmlElement">The object type which implements <see cref="HtmlElement"/>.</typeparam>
        /// <param name="node">The current <see cref="HtmlElement"/>.</param>
        /// <param name="styleObject">The object which contains CSS properties and values to style the component.</param>
        /// <returns>The self <see cref="HtmlElement"/> object for fluent chaining.</returns>
        public static THtmlElement WithStyle<THtmlElement>(this THtmlElement node, object styleObject) where THtmlElement : HtmlElement
        {
            node.Style = styleObject;
            return node;
        }
    }
}
