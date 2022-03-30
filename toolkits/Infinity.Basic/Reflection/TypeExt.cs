namespace Infinity.Reflection;

public static class TypeExt {

    #region Basic
    public static bool IsClassOrSubclass(this Type type, Type baseType) {
        return type == baseType || type.IsSubclassOf(baseType);
    }

    public static bool IsSubclassGeneric(this Type type, Type baseType) {
        if (!baseType.IsGenericType) throw new ReflectionException(baseType, "must be a generic type");

        var testType = type;
        
        while (testType != typeof(object)) {
            if (Generics.IsEqualGenerics(testType, baseType)) return true;
        
            testType = testType.BaseType;
            if (testType is null) return false;
        }

        return false;
    }
    #endregion

    #region Interfaces
    public static bool ImplementsInterface(this Type type, Type interfaceType) {
        if (!interfaceType.IsInterface) throw new ReflectionException(interfaceType, "must be an interface type");
        return type.GetInterfaces().Contains(interfaceType);
    }

    public static bool ImplementsGenericInterface(this Type type, Type genericInterfaceType) {
        if (!genericInterfaceType.IsInterface || !genericInterfaceType.IsGenericType)
            throw new ReflectionException(genericInterfaceType, "must be a generic interface type");

        var interfaces = type.FindInterfaces((testType, _) => Generics.IsEqualGenerics(testType, genericInterfaceType), null);
        
        return interfaces.Length != 0;
    }
    #endregion

}
