namespace Infinity.Collections.Events; 

public class ItemValueChangedEventArgs<T> : EventArgs {

    public T OldValue { get; }
    public T NewValue { get; }
    public int Index { get; }
    
    public ItemValueChangedEventArgs(int index, T oldValue, T newValue) {
        (Index, OldValue, NewValue) = (index, oldValue, newValue);
    }

}

public delegate void ItemValueChangedEventHandler<T>(object sender, ItemValueChangedEventArgs<T> eventArgs);
