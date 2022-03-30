namespace Infinity.Events;

public class ValueChangedEventArgs<T> : EventArgs {

    public T NewValue { get; }

    public ValueChangedEventArgs(T newValue) => NewValue = newValue;

}

public delegate void ValueChangedEventHandler<T>(object sender, ValueChangedEventArgs<T> eventArgs);
