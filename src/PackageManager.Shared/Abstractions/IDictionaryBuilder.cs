using System.Collections.Generic;

namespace PackageManager.Shared.Abstractions
{
    public interface IDictionaryBuilder<TKey, TValue> 
        : IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValue);
        IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
        IDictionary<TKey, TValue> Dictionary { get; }
    }
}
