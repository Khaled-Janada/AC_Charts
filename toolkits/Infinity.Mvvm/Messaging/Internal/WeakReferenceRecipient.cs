using Infinity.Messaging.Messages;

namespace Infinity.Messaging.Internal; 

internal class WeakReferenceRecipient<TRecipient, TMessage> : IRecipient
    where TRecipient : class where TMessage : Message {

    #region Properties
    public bool IsAlive => IsObjectAlive();
    private WeakReference<TRecipient> RecipientReference { get; }
    private Action<TMessage> Action { get; }
    #endregion

    public WeakReferenceRecipient(TRecipient recipient, Action<TMessage> action) {
        RecipientReference = new WeakReference<TRecipient>(recipient);
        Action = action;
    }
    
    public void Execute<T>(T message) where T : Message {
        if (GetRecipient() is null) return;
        if (message is not TMessage tMessage) throw new MessangerException("Message is of wrong type");
        
        Action.Invoke(tMessage);
    }

    public bool IsSameRecipient<T>(T other) where T : class {
        var recipient = GetRecipient();
        return recipient is not null && IRecipient.IsSameRecipient(recipient, other);
    }

    #region Private Methods
    private TRecipient? GetRecipient() {
        RecipientReference.TryGetTarget(out var obj);
        return obj;
    }
    
    private bool IsObjectAlive() {
        return GetRecipient() is not null;
    }
    #endregion
}
