
using System.Collections.ObjectModel;
using Infinity.Collections.Events;
using Infinity.ComponentModel.Numeric;

namespace Infinity.Collections {

    public class CounterCollection<T> : ObservableCollection<T> {

        #region Properties
        public BindableInt Counter { get; }
        private Func<T> DefaultFactory { get; }
        #endregion

        #region Events
        public event ItemAddedEventHandler<T>? ItemAdded;
        public event ItemRemovedEventHandler<T>? ItemRemoved;
        public event ItemMovedEventHandler<T>? ItemMoved;
        public event ItemValueChangedEventHandler<T>? ItemSet;
        public event ItemsClearedEventHandler? ItemsCleared;
        public event CountChangedEventHandler? CountChanged;
        #endregion

        #region Constructors
        public CounterCollection(Func<T> defaultFactory) : this(new List<T>(), defaultFactory) { }

        public CounterCollection(IEnumerable<T> list, Func<T> defaultFactory) : base(list) {
            DefaultFactory = defaultFactory;

            Counter = new BindableInt(() => Count, num => {
                while (num > Count) AddNew();
                while (num < Count) RemoveAt(Count - 1);
            }) { Name = "Counter" };
        }
        #endregion

        #region Public Methods
        public T AddNew() {
            base.InsertItem(Count, DefaultFactory());
            
            ItemAdded?.Invoke(this, new ItemAddedEventArgs<T>(this[^1], Count - 1));
            CountChanged?.Invoke(this, new CountChangedEventArgs(Count - 1, Count));
            Counter.RaiseValueChanged();
            
            return this[^1];
        }
        #endregion

        #region Overrides
        protected override void ClearItems() {
            var oldCount = Count;
            base.ClearItems();
            
            ItemsCleared?.Invoke(this, new ItemsClearedEventArgs());
            CountChanged?.Invoke(this, new CountChangedEventArgs(oldCount, 0));
            Counter.RaiseValueChanged();
        }

        protected override void RemoveItem(int index) {
            var oldItem = this[index];
            base.RemoveItem(index);
            
            ItemRemoved?.Invoke(this, new ItemRemovedEventArgs<T>(oldItem, index));
            CountChanged?.Invoke(this, new CountChangedEventArgs(Count + 1, Count));
            Counter.RaiseValueChanged();
        }

        protected override void InsertItem(int index, T item) {
            base.InsertItem(index, item);
            
            ItemAdded?.Invoke(this, new ItemAddedEventArgs<T>(item, index));
            Counter.RaiseValueChanged();
        }

        protected override void SetItem(int index, T item) {
            var oldValue = this[index];
            base.SetItem(index, item);
            
            ItemSet?.Invoke(this, new ItemValueChangedEventArgs<T>(index, oldValue, item));
        }

        protected override void MoveItem(int oldIndex, int newIndex) {
            base.MoveItem(oldIndex, newIndex);
            ItemMoved?.Invoke(this, new ItemMovedEventArgs<T>(this[newIndex], oldIndex, newIndex));
        }
        #endregion

    }

}
