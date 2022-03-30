using Infinity.Messaging;
using Infinity.Messaging.Messages;

namespace Infinity.ComponentModel.ViewModels; 

public class ViewModelBase : BindableBase {

    #region Properties
    protected Messenger Messenger { get; }
    #endregion
    
    #region Constructors
    protected ViewModelBase() : this(Messenger.Default) { }

    protected ViewModelBase(Messenger messenger) {
        Messenger = messenger;
        Messenger.RegisterAll(this);
    }
    #endregion

    #region Virtual Methods
    public override void Dispose() {
        base.Dispose();
        Messenger.UnregisterAll(this);
    }
    #endregion

    #region Messaging
    protected TMessage Send<TMessage>(TMessage message, uint? channel = null) where TMessage : Message {
        Messenger.Send(message, channel);
        return message;
    }
    
    protected T Request<TMessage, T>(TMessage message, uint? chnnel = null) where TMessage : RequestMessage<T> {
        return Messenger.Request<TMessage, T>(message, chnnel);
    }
    #endregion
}
