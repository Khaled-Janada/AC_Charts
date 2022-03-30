namespace Infinity.Extensions; 

public static class LinqExt {

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
        foreach (var item in enumerable) action.Invoke(item);
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action) {
        var n = 0;
        foreach (var item in enumerable) action.Invoke(item, n++);
    }

}
