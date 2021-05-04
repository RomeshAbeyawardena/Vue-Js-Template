using System.Collections.Generic;

namespace PackageManager.Shared.Abstractions
{
    public interface IDictionaryBuilder<TKey, TValue>
        : IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <inheritdoc cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/>
        IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValue);

        /// <inheritdoc cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/>
        IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);

        /// <summary>
        /// Gets the underlining <see cref="IDictionary{TKey, TValue}"/>
        /// </summary>
        IDictionary<TKey, TValue> Dictionary { get; }
    }
}
