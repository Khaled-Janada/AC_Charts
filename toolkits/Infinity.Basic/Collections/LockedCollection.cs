using Infinity.Collections.Events;

namespace Infinity.Collections;

public class LockedCollection<T> : ReadOnlyCollection<T> {

    #region Priavte
    protected Func<T> ModelFactory { get; }
    #endregion

    #region Events
    public event ItemAddedEventHandler<T>? ItemAdded;
    public event ItemRemovedEventHandler<T>? ItemRemoved;
    public event ItemMovedEventHandler<T>? ItemMoved;
    public event ItemsClearedEventHandler? ItemsCleared;
    public event CountChangedEventHandler? CountChanged;
    #endregion

    #region Constructors
    public LockedCollection(IEnumerable<T> list, Func<T> modelFactory) : base(list.ToList()) {
        ModelFactory = modelFactory;
    }
    
    public LockedCollection(IList<T> list, Func<T> modelFactory) : base(list) {
        ModelFactory = modelFactory;
    }

    public LockedCollection(Func<T> modelFactory) : this(new Collection<T>(), modelFactory) { }
    #endregion

    #region Public Methods
    public T AddNew() {
        Items.Add(ModelFactory());
        var count = Items.Count;
        
        ItemAdded?.Invoke(this, new ItemAddedEventArgs<T>(Items[^1], count - 1));
        CountChanged?.Invoke(this, new CountChangedEventArgs(count - 1, count));
        
        return Items[^1];
    }

    public bool Remove(T item) {
        var index = Items.IndexOf(item);
        if (index < 0) return false;

        Items.Remove(item);
        var count = Items.Count;
        
        ItemRemoved?.Invoke(this, new ItemRemovedEventArgs<T>(item, index));
        CountChanged?.Invoke(this, new CountChangedEventArgs(count + 1, count));
        
        return true;
    }

    public void RemoveAt(int index) {
        var item = Items[index];
        
        Items.RemoveAt(index);
        var count = Items.Count;
        
        ItemRemoved?.Invoke(this, new ItemRemovedEventArgs<T>(item, index));
        CountChanged?.Invoke(this, new CountChangedEventArgs(count + 1, count));
    }

    public void MoveItem(int oldIndex, int newIndex) {
        (Items[oldIndex], Items[newIndex]) = (Items[newIndex], Items[oldIndex]);
        ItemMoved?.Invoke(this, new ItemMovedEventArgs<T>(Items[newIndex], oldIndex, newIndex));
    }

    public void Clear() {
        var count = Items.Count;
        Items.Clear();
        
        ItemsCleared?.Invoke(this, new ItemsClearedEventArgs());
        CountChanged?.Invoke(this, new CountChangedEventArgs(count, 0));
    }
    #endregion

}
