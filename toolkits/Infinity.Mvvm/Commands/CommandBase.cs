using System.Windows.Input;

namespace Infinity.Commands;

public abstract class CommandBase : ICommand {

    public static bool DefaultUseCommandManager { get; set; } = true;

    public abstract bool CanExecute(object? parameter);
    public abstract void Execute(object? parameter);
    public abstract event EventHandler? CanExecuteChanged;

    #region Static Methods
    public static void Subscribe(EventHandler? canExecuteChangedHandler) {
        CommandManager.RequerySuggested += canExecuteChangedHandler;
    }

    public static void Unsubscribe(EventHandler? canExecuteChangedHandler) {
        CommandManager.RequerySuggested -= canExecuteChangedHandler;
    }

    public static void InvalidateRequerySuggested() {
        CommandManager.InvalidateRequerySuggested();
    }
    #endregion

}
