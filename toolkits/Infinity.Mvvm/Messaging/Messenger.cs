using System.Reflection;
using Infinity.Extensions;
using Infinity.Messaging.Internal;
using Infinity.Messaging.Messages;
using Infinity.Reflection;

namespace Infinity.Messaging;

public class Messenger {

    #region Properties
    public ReferenceType ReferenceType { get; }

    private Dictionary<MessageKey, List<IRecipient>> Dictionary { get; } = new();
    private readonly object _myLock = new();

    public static Messenger Default { get; } = new(ReferenceType.WeakReference);
    #endregion

    #region Constructors
    public Messenger(ReferenceType referenceType) {
        ReferenceType = referenceType;
    }
    #endregion

    #region Static
    public static MethodInfo RegisterMethod { get; }

    static Messenger() {
        var registerMethods = typeof(Messenger).GetMethod(nameof(Register), 1,
            new[] { typeof(object), typeof(Action<>).MakeGenericType(Type.MakeGenericMethodParameter(0)), typeof(uint?) });
        RegisterMethod = registerMethods ?? throw new NullReferenceException();
    }
    #endregion

    #region Basic Methods
    public void Register<TMessage>(object recipient, Action<TMessage> handler, uint? channel = null) where TMessage : Message {
        lock (_myLock) {
            Clean();

            var recipients = Dictionary.GetOrSetValue(new MessageKey(typeof(TMessage), channel), new List<IRecipient>());

            if (recipients.Any(recp => recp.IsSameRecipient(recipient)))
                throw new MessangerException("The recipient has already subscribed to this message");

            recipients.Add(GetRecipientKey(recipient, handler));
        }
    }

    public void Send<TMessage>(TMessage message, uint? channel = null) where TMessage : Message {
        lock (_myLock) {
            Clean();

            if (!Dictionary.TryGetValue(new MessageKey(message.GetType(), channel), out var recipients)) return;
            recipients.ToList().ForEach(recp => recp.Execute(message));
        }
    }

    public T Request<TMessage, T>(TMessage message, uint? channel = null) where TMessage : RequestMessage<T> {
        Send(message, channel);
        return message.Response;
    }

    public void Unregister<TMessage>(object recipient, uint? channel = null) where TMessage : Message {
        lock (_myLock) {
            Clean();

            if (!Dictionary.TryGetValue(new MessageKey(typeof(TMessage), channel), out var recipients)) return;
            recipients.RemoveAll(recp => recp.IsSameRecipient(recipient));
        }
    }
    #endregion

    #region Overloads
    public void Register(Type messageType, object recipient, Delegate handler, uint? channel = null) {
        if (!messageType.IsSubclassOf(typeof(Message))) throw new MessangerException("Type must be a subclass of 'Message'");

        var actionParameter = handler.GetType().GenericTypeArguments[0];

        if (!actionParameter.IsAssignableFrom(messageType))
            throw new MessangerException($"Action<{actionParameter}> cannot handle a message of type '{messageType}'");

        RegisterMethod.MakeGenericMethod(messageType).Invoke(this, new[] { recipient, handler, channel });
    }
    #endregion

    #region All Methods
    public void RegisterAll(object recipient) {
        var recpType = recipient.GetType();
        var interfaces = recpType.FindInterfaces((type, _) => Generics.IsEqualGenerics(type, typeof(IRecipient<>)), null);

        foreach (var interfaceType in interfaces) {
            var messageType = interfaceType.GenericTypeArguments[0];

            var handler = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(messageType), recipient, "Receive") ??
                          throw new MessangerException("No corresponding 'Receive' method was found");

            Register(messageType, recipient, handler);
        }
    }

    public void UnregisterAll(object recipient) {
        lock (_myLock) {
            Clean();

            Dictionary.Values.ForEach(recipients => recipients.RemoveAll(recp => recp.IsSameRecipient(recipient)));
        }
    }
    #endregion

    #region Private Method
    private IRecipient GetRecipientKey<TRecipient, TMessage>(TRecipient recipient, Action<TMessage> handler)
        where TRecipient : class where TMessage : Message {
        return ReferenceType == ReferenceType.StrongReference ?
            new StrongReferenceRecipient<TRecipient, TMessage>(recipient, handler) :
            new WeakReferenceRecipient<TRecipient, TMessage>(recipient, handler);
    }

    private void Clean() {
        foreach (var recipients in Dictionary.Values) {
            recipients.RemoveAll(recp => !recp.IsAlive);
        }
    }
    #endregion

}
