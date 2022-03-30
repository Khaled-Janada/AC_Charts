namespace Infinity.Extensions; 

public static class Enum<T> where T : Enum {

    public static ReadOnlyCollection<T> ToList() {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList().AsReadOnly();
    }
    
    public static ReadOnlyDictionary<T, TValue>ToDictionary<TValue>(Func<T, TValue> mapper) {
        return new ReadOnlyDictionary<T, TValue>(ToList().ToDictionary(value => value, mapper));
    }

    public static ReadOnlyCollection<string> ToStringList() {
        return ToList().Select(value => value.ToString()).ToList().AsReadOnly();
    }

    public static ReadOnlyCollection<string> ToStringList(Func<string, string> mapper) {
        return ToList().Select(value => mapper(value.ToString())).ToList().AsReadOnly();
    }

    public static ReadOnlyDictionary<T, string> ToStringDictionary() {
        return new ReadOnlyDictionary<T, string>(ToList().ToDictionary(value => value, value => value.ToString()));
    }

    public static ReadOnlyDictionary<T, string> ToStringDictionary(Func<string, string> mapper) {
        return new ReadOnlyDictionary<T, string>(ToList().ToDictionary(value => value, value => mapper(value.ToString())));
    }

}
