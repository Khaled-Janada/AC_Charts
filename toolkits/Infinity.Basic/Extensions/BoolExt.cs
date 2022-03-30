namespace Infinity.Extensions;

public static class BoolExt {

    public static void IfElse(this bool test, Action acion1, Action action2) {
        if (test)
            acion1.Invoke();
        else
            action2.Invoke();
    }

}
