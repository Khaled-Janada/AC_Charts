namespace Infinity.Collections.Events;

public class ItemRemovedEventArgs<T> : EventArgs {

    public T Item { get; }
    public int Index { get; }

    public ItemRemovedEventArgs(T item, int index) => (Item, Index) = (item, index);

}

public delegate void ItemRemovedEventHandler<T>(object sender, ItemRemovedEventArgs<T> eventArgs);
