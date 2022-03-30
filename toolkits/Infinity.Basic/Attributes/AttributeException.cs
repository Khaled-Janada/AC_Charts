namespace Infinity.Attributes; 

public class AttributeException : Exception {

    public AttributeException(){}
    
    public AttributeException(string message) : base(message){}
    
    public AttributeException(Attribute attribute, string message) : base($"The {attribute.GetType().Name} " + message){}

}
