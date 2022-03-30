namespace Infinity.Messaging.Messages; 

public record RequestMessage<T> : Message {
    
#pragma warning disable CS8618
    private T _response;
#pragma warning restore CS8618
    
    #region Properties
    public bool HasResponded { get; private set; }

    public T Response {
        get {
            if (!HasResponded) throw new MessangerException("Has not received a response");
            return _response;
        }
    }
    #endregion

    #region Public Methods
    public void Reply(T response) {
        if (HasResponded) throw new MessangerException("Has already received a response");
        HasResponded = true;
        _response = response;
    }
    #endregion

}
