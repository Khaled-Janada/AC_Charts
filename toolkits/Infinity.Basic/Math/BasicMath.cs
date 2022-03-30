namespace Infinity.Math;

public static class BasicMath {

    #region Static
    public static double Eps { get; set; } = 0.001;
    #endregion

    #region Basic
    public static double Root(double value, double root) => System.Math.Pow(value, 1.0 / root);

    public static bool IsNearlyEqual(this double d1, double d2) {
        return System.Math.Abs(d1 - d2) < Eps;
    }
    #endregion

    #region Trigonometry
    public static double Slope((double x, double y) p1, (double x, double y) p2) {
        return (p2.y - p1.y) / (p2.x - p1.x);
    }

    public static double RadToDeg(double d) => d * 180            / System.Math.PI;
    public static double DegToRad(double d) => d * System.Math.PI / 180;

    public static double Acosd(double d) => RadToDeg(System.Math.Acos(d));
    public static double Asind(double d) => RadToDeg(System.Math.Asin(d));
    public static double Atand(double d) => RadToDeg(System.Math.Atan(d));

    public static double Cosd(double d) => RadToDeg(System.Math.Cos(d));
    public static double Sind(double d) => RadToDeg(System.Math.Sin(d));
    public static double Tand(double d) => RadToDeg(System.Math.Tan(d));
    #endregion

    #region List Operations
    public static (T value, int index) Min<T>(IList<T> list) where T : IComparable<T> {
        var min = list.Min();
        if (min is null) throw new NullReferenceException();
        return (min, list.IndexOf(min));
    }

    public static (T value, int index) Max<T>(IList<T> list) where T : IComparable<T> {
        var min = list.Max();
        if (min is null) throw new NullReferenceException();
        return (min, list.IndexOf(min));
    }
    #endregion

}
