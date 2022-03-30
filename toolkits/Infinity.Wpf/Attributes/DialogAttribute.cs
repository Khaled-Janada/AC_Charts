using Infinity.Messaging.Messages;
using Infinity.Reflection;

namespace Infinity.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DialogAttribute : Attribute {

    public Type ViewModelMessageType { get; }

    public DialogAttribute(Type viewModelMessageType) {
        if (!viewModelMessageType.ImplementsInterface(typeof(IViewModelMessage)))
            throw new AttributeException("Type must implement the 'IViewModelMessage' interface");
        
        ViewModelMessageType = viewModelMessageType;
    }

}
