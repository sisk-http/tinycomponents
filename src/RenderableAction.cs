using System;

namespace TinyComponents {
    /// <summary>
    /// Represents an object which their renderable contents is called by an
    /// function.
    /// </summary>
    public sealed class RenderableFunction {

        /// <summary>
        /// Gets or sets the renderable function.
        /// </summary>
        public Func<object?> Callable { get; set; }

        /// <summary>
        /// Creates an new <see cref="RenderableFunction"/> class with the specified
        /// function.
        /// </summary>
        /// <param name="callable">The function which will result the contents to be rendered.</param>
        public RenderableFunction ( Func<object?> callable ) {
            this.Callable = callable;
        }

        /// <summary>
        /// Invokes <see cref="Callable"/> and returns its result as an string.
        /// </summary>
        public override string? ToString () {
            return this.Callable ()?.ToString ();
        }
    }
}
