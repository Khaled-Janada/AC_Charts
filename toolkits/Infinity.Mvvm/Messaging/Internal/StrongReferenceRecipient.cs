using Infinity.Messaging.Messages;

namespace Infinity.Messaging.Internal;

internal class StrongReferenceRecipient<TRecipient, TMessage> : IRecipient 
    where TRecipient : class where TMessage : Message {

    public bool IsAlive => true;
    public TRecipient Recipient { get; }
    private Action<TMessage> Action { get; }

    public StrongReferenceRecipient(TRecipient recipient, Action<TMessage> action) {
        (Recipient, Action) = (recipient, action);
    }
    
    public bool IsSameRecipient<T>(T other) where T : class {
        return IRecipient.IsSameRecipient(Recipient, other);
    }

    public void Execute<T>(T message) where T : Message {
        if (message is not TMessage tMessage) throw new MessangerException("Message is of wrong type");
        Action.Invoke(tMessage);
    }

}
;
