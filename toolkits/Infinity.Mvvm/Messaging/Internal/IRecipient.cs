using Infinity.Messaging.Messages;

namespace Infinity.Messaging.Internal;

internal interface IRecipient {

    #region Properties
    public bool IsAlive { get; }
    #endregion

    #region Methods
    public bool IsSameRecipient<T>(T other) where T : class;
    public void Execute<TMessage>(TMessage message) where TMessage : Message;
    #endregion

    #region Static Methods
    public static bool IsSameRecipient(object obj1, object obj2) {
        if (obj1 is ValueType v1  && obj2 is ValueType v2) return Equals(v1, v2);
        if (obj1 is not ValueType && obj2 is not ValueType) return ReferenceEquals(obj1, obj2);
        return false;
    }
    #endregion

}
