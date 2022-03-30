namespace Infinity.Commands;

public class Command : Command<object> {

    public Command(Action executeCallback) : base(_ => executeCallback()) { }

    public Command(Action executeCallback, Func<bool> canExecuteCallback) :
        base(_ => executeCallback(), _ => canExecuteCallback()) { }

}
