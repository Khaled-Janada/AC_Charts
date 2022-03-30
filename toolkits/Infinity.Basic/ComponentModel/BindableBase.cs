using System.ComponentModel;
using System.Data;

namespace Infinity.ComponentModel;

public abstract class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging {

    #region Events
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;
    #endregion

    #region Event Handlers
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanging([CallerMemberName] string? propertyName = null) {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }
    #endregion

    #region Set Property
    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;

        OnPropertyChanging(propertyName);
        field = newValue;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected bool SetProperty<T>(ref T field, T newValue, Action<T> callback, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;

        OnPropertyChanging(propertyName);
        field = newValue;
        callback(newValue);
        OnPropertyChanged(propertyName);
        return true;
    }

    protected bool SetProperty<T>(T oldValue, T newValue, Action<T> callback, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return false;

        OnPropertyChanging(propertyName);
        callback(newValue);
        OnPropertyChanged(propertyName);

        return true;
    }

    protected bool SetProperty<TModel, T>(
        TModel model, T newValue,
        [CallerMemberName] string? modelPropertyName = null, [CallerMemberName] string? propertyName = null) where T : class {
        if (model is null || modelPropertyName is null) throw new NoNullAllowedException();

        var modelProperty = model.GetType().GetProperty(modelPropertyName);
        if (modelProperty is null) throw new NullReferenceException($"No property of name : {modelPropertyName} exists");

        if (!(modelProperty.CanRead && modelProperty.CanWrite)) throw new ReadOnlyException();
        if (modelProperty.PropertyType != typeof(T)) throw new ArgumentException($"Properties are not of the same type");

        if (EqualityComparer<T>.Default.Equals((T)modelProperty.GetValue(model)!, newValue)) return false;

        OnPropertyChanging(propertyName);
        modelProperty.SetValue(model, newValue);
        OnPropertyChanged(propertyName);

        return true;
    }
    #endregion

    #region Virtual Methods
    public virtual void Dispose() {
        PropertyChanged = null;
        PropertyChanging = null;
    }
    #endregion

}
