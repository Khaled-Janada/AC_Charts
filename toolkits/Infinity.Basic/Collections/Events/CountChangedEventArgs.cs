namespace Infinity.Collections.Events;

public class CountChangedEventArgs : EventArgs {

    public int OldCount { get; }
    public int NewCount { get; }

    public CountChangedEventArgs(int oldCount, int newCount) {
        (OldCount, NewCount) = (oldCount, newCount);
    }

}

public delegate void CountChangedEventHandler(object sender, CountChangedEventArgs eventArgs);
