using System.Windows;

namespace Infinity.Commands;

public class Command<T> : CommandBase {

    #region Fields
    protected readonly Predicate<T> _canExecuteCallback;
    protected readonly Action<T> _executCallback;
    protected readonly bool _useCommandManager;
    #endregion

    #region Events
    private event EventHandler? PrivateCanExecuteChanged;

    public override event EventHandler? CanExecuteChanged {
        add {
            if (_useCommandManager)
                Subscribe(value);
            else
                PrivateCanExecuteChanged += value;
        }
        remove {
            if (_useCommandManager)
                Unsubscribe(value);

            else
                PrivateCanExecuteChanged -= value;
        }
    }
    #endregion

    #region Constructors
    public Command(Action<T> executCallback) {
        _executCallback = executCallback;
        _canExecuteCallback = _ => true;
        _useCommandManager = false;
    }

    public Command(Action<T> executCallback, Predicate<T> canExecuteCallback, bool? useCommandManager = null) {
        _executCallback = executCallback;
        _canExecuteCallback = canExecuteCallback;
        _useCommandManager = useCommandManager ?? DefaultUseCommandManager;
    }
    #endregion

    #region Execute
    public void RaiseCanExecuteChanged() {
        if (_useCommandManager)
            InvalidateRequerySuggested();
        else
            PrivateCanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(T parameter) {
        return _canExecuteCallback.Invoke(parameter);
    }

    public override bool CanExecute(object? parameter) => _canExecuteCallback((T)parameter!);

    public void Execute(T parameter) {
        if (!CanExecute(parameter)) return;

        try {
            _executCallback((T)parameter!);
        }
        catch (Exception e) {
            MessageBox.Show(e.Message, "Error");
            throw;
        }
    }

    public override void Execute(object? parameter) {
        Execute((T)parameter!);
    }
    #endregion

}
