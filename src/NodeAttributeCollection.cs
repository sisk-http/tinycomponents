using System;
using System.Collections;
using System.Collections.Generic;

namespace TinyComponents {
    /// <summary>
    /// Represents an dictionary collection of attributes.
    /// </summary>
    public class NodeAttributeCollection : IDictionary<string, object?> {
        readonly Dictionary<string, object?> _attributes;

        /// <summary>
        /// Creates an new instance of the <see cref="NodeAttributeCollection"/>.
        /// </summary>
        public NodeAttributeCollection () {
            this._attributes = new Dictionary<string, object?> ( StringComparer.OrdinalIgnoreCase );
        }

        /// <inheritdoc/>
        public object? this [ string key ] {
            get {
                if (this._attributes.TryGetValue ( key, out object? value )) {
                    return value;
                }
                return null;
            }
            set => ((IDictionary<string, object?>) this._attributes) [ key ] = value;
        }

        /// <inheritdoc/>
        public ICollection<string> Keys => ((IDictionary<string, object?>) this._attributes).Keys;

        /// <inheritdoc/>
        public ICollection<object?> Values => ((IDictionary<string, object?>) this._attributes).Values;

        /// <inheritdoc/>
        public int Count => ((ICollection<KeyValuePair<string, object?>>) this._attributes).Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((ICollection<KeyValuePair<string, object?>>) this._attributes).IsReadOnly;

        /// <inheritdoc/>
        public void Add ( string key, object? value ) {
            ((IDictionary<string, object?>) this._attributes).Add ( key, value );
        }

        /// <inheritdoc/>
        public void Add ( KeyValuePair<string, object?> item ) {
            ((ICollection<KeyValuePair<string, object?>>) this._attributes).Add ( item );
        }

        /// <inheritdoc/>
        public void Clear () {
            ((ICollection<KeyValuePair<string, object?>>) this._attributes).Clear ();
        }

        /// <inheritdoc/>
        public bool Contains ( KeyValuePair<string, object?> item ) {
            return ((ICollection<KeyValuePair<string, object?>>) this._attributes).Contains ( item );
        }

        /// <inheritdoc/>
        public bool ContainsKey ( string key ) {
            return ((IDictionary<string, object?>) this._attributes).ContainsKey ( key );
        }

        /// <inheritdoc/>
        public void CopyTo ( KeyValuePair<string, object?> [] array, int arrayIndex ) {
            ((ICollection<KeyValuePair<string, object?>>) this._attributes).CopyTo ( array, arrayIndex );
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator () {
            return ((IEnumerable<KeyValuePair<string, object?>>) this._attributes).GetEnumerator ();
        }

        /// <inheritdoc/>
        public bool Remove ( string key ) {
            return ((IDictionary<string, object?>) this._attributes).Remove ( key );
        }

        /// <inheritdoc/>
        public bool Remove ( KeyValuePair<string, object?> item ) {
            return ((ICollection<KeyValuePair<string, object?>>) this._attributes).Remove ( item );
        }

        /// <inheritdoc/>
        public bool TryGetValue ( string key, out object? value ) {
            return ((IDictionary<string, object?>) this._attributes).TryGetValue ( key, out value );
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return ((IEnumerable) this._attributes).GetEnumerator ();
        }
    }
}
