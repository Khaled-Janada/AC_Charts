using Infinity.Events;

namespace Infinity.ComponentModel.Numeric;

public abstract class BindableNumberBase<T> : BindableBase where T : struct, IComparable<T> {

    #region Fields
    private readonly Func<T>? _getter;
    private readonly Action<T>? _setter;

    private T _value;

    protected T _minValue;
    protected T _maxValue;
    #endregion

    #region Properties
    public string Name { get; set; } = "Value";

    public T MinValue { get => _minValue; set => SetProperty(ref _minValue, value); }
    public T MaxValue { get => _maxValue; set => SetProperty(ref _maxValue, value); }

    public T Value { get => _getter?.Invoke() ?? _value; set => SetProperty(ref _value, value, SetValue); }
    #endregion

    #region Event
    public event Action<object, ValueChangedEventArgs<T>>? ValueChanged;

    public void RaiseValueChanged() {
        OnPropertyChanged(nameof(Value));
        ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(Value));
    }
    #endregion

    #region Constructors
    protected BindableNumberBase(T minValue, T maxValue) {
        (_minValue, _maxValue) = (minValue, maxValue);
    }

    protected BindableNumberBase(Func<T>? getter, Action<T>? setter, T minValue, T maxValue) : this(minValue, maxValue) {
        _getter = getter ?? (() => _value);
        _setter = setter;
    }
    #endregion

    #region Public Methods
    public static implicit operator T(BindableNumberBase<T> numberObject) => numberObject.Value;
    #endregion

    #region Protected Methods
    protected virtual void SetValue(T newValue) {
        if (newValue.CompareTo(_maxValue) > 0)
            throw new ArgumentException($"Value {newValue} is larger than the maximum value {_maxValue}");
        if (newValue.CompareTo(_minValue) < 0)
            throw new ArgumentException($"Value {newValue} is smaller than the minimum value {_minValue}");

        _setter?.Invoke(newValue);
        RaiseValueChanged();
    }
    #endregion

}
