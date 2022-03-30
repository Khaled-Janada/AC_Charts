namespace Infinity.Collections.Events;

public class ItemsClearedEventArgs : EventArgs { }

public delegate void ItemsClearedEventHandler(object sender, ItemsClearedEventArgs eventArgs);
