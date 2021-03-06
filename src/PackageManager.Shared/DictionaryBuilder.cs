using PackageManager.Shared.Abstractions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PackageManager.Shared
{
    public static class DictionaryBuilder
    {
        public static IDictionaryBuilder<TKey, TValue> Create<TKey, TValue>(IDictionary<TKey, TValue> dictionary = default)
        {
            return new DictionaryBuilder<TKey, TValue>(dictionary);
        }
    }

    public class DictionaryBuilder<TKey, TValue>
        : IDictionaryBuilder<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        public DictionaryBuilder(IDictionary<TKey, TValue> dictionary = default)
        {
            this.dictionary = dictionary ?? new Dictionary<TKey, TValue>();
        }

        public TValue this[TKey key] => dictionary[key];

        public IEnumerable<TKey> Keys => dictionary.Keys;

        public IEnumerable<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValue)
        {
            dictionary.Add(keyValue);
            return this;
        }

        public IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value)
        {
            return Add(KeyValuePair.Create(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public IDictionary<TKey, TValue> Dictionary => dictionary;
    }
}
