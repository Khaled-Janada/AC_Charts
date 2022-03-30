namespace Infinity.Reflection; 

public class ReflectionException : Exception {

    public ReflectionException(){}
    
    public ReflectionException(string message) : base(message){}
    
    public ReflectionException(Type type, string message) : base($"Type {type} " + message){}

}
