namespace Infinity.Collections.Events;

public class ItemAddedEventArgs<T> : EventArgs {

    public T Item { get; }
    public int Index { get; }

    public ItemAddedEventArgs(T item, int index) => (Item, Index) = (item, index);

}

public delegate void ItemAddedEventHandler<T>(object sender, ItemAddedEventArgs<T> eventArgs);
