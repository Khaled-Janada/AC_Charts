using System.Data;

namespace Infinity.Collections;

public class BindingLockedCollection<TModel, TViewModel> : CounterCollection<TViewModel> {

    #region Properties
    private LockedCollection<TModel> ModelCollection { get; }
    #endregion

    #region Constructors
    public BindingLockedCollection(LockedCollection<TModel> modelCollection, Func<TModel, TViewModel> viewModelFactory) :
        base(modelCollection.Select(viewModelFactory), () => viewModelFactory(modelCollection.AddNew())) {
        ModelCollection = modelCollection;
    }
    #endregion

    #region Overrides
    protected override void ClearItems() {
        ModelCollection.Clear();
        base.ClearItems();
    }

    protected override void InsertItem(int index, TViewModel item) {
        throw new ReadOnlyException();
    }

    protected override void MoveItem(int oldIndex, int newIndex) {
        ModelCollection.MoveItem(oldIndex, newIndex);
        base.MoveItem(oldIndex, newIndex);
    }

    protected override void RemoveItem(int index) {
        ModelCollection.RemoveAt(index);
        base.RemoveItem(index);
    }

    protected override void SetItem(int index, TViewModel item) {
        throw new ReadOnlyException();
    }
    #endregion

}
