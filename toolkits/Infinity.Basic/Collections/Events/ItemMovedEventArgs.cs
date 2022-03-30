namespace Infinity.Collections.Events;

public class ItemMovedEventArgs<T> : EventArgs {

    public int OldIndex { get; }
    public int NewIndex { get; }
    public T Item { get; }

    public ItemMovedEventArgs(T item, int oldIndex, int newIndex) {
        (OldIndex, NewIndex, Item) = (oldIndex, newIndex, item);
    }

}

public delegate void ItemMovedEventHandler<T>(object sender, ItemMovedEventArgs<T> eventArgs);
