namespace Infinity.Math; 

public static class MatMath {

    #region Define
    public static IList<double> LinSpace(double start, double end, double numPoints) {
        var list = new List<double>();
        var step = (end - start) / numPoints;
        
        for (double n = start; n <= end + BasicMath.Eps; n += step) list.Add(n);
        return list;
    }
    #endregion

}
