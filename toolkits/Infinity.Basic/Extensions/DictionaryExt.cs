namespace Infinity.Extensions;

public static class DictionaryExt {

    public static TValue GetOrSetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,TValue defaultValue)
        where TKey : notnull {
        dictionary.TryAdd(key, defaultValue);
        return dictionary[key];
    }

}
