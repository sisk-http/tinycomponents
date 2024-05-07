namespace TinyComponents
{
    /// <summary>
    /// Represents an object that can be rendered into HTML.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Renders the current instance into an string.
        /// </summary>
        /// <returns>A string representing the content of this instance.</returns>
        public string Render();
    }
}
