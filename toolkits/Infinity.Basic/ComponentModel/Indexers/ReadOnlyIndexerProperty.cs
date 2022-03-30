namespace Infinity.ComponentModel.Indexers;

public class ReadOnlyIndexerProperty<TIndex, TValue> {

    private readonly Func<TIndex, TValue> _getFunc;

    public ReadOnlyIndexerProperty(Func<TIndex, TValue> getFunc) {
        _getFunc = getFunc;
    }

    public TValue this[TIndex i] => _getFunc(i);

}
