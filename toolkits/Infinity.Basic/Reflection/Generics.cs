namespace Infinity.Reflection; 

public static class Generics {

    public static bool IsEqualGenerics(Type generic1, Type generic2) {
        return generic1.IsGenericType && generic2.IsGenericType &&
               generic1.GetGenericTypeDefinition() == generic2.GetGenericTypeDefinition();
    }

}
