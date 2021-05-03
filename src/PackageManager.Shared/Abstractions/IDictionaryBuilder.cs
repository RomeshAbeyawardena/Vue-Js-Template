using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IDictionaryBuilder<TKey, TValue> 
        : IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValue);
        IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
    }
}
