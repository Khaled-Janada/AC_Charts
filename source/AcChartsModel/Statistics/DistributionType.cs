namespace AcCharts.Statistics;

public enum DistributionType {

    Erlang,
    Exponential,
    Frechet,
    Gamma,
    HalfLogistic,
    LogNormal,
    LogLogistic,
    Rayleigh,
    Weibull,

}

public static class DistributionTypeExt {
    
    public static double GetShapeParameter(this DistributionType type, double shape) {
        if (!type.HasShape()) return double.NaN;
        return type switch {
            DistributionType.Erlang => Math.Floor(shape),
            DistributionType.LogLogistic => Math.Max(1.01, shape),
            _ => Math.Max(0.01, shape)
        };
    }

    public static bool HasShape(this DistributionType type) {
        return type is not (DistributionType.Exponential or DistributionType.Rayleigh or DistributionType.HalfLogistic);
    }

    public static bool IsShapeInteger(this DistributionType type) {
        return type is DistributionType.Erlang;
    }
    
    public static string ShapeParameterSymbol(this DistributionType type) {
        // return type is DistributionType.LogNormal ? "\U0001D70E" : "\U0001D6FD";
        return "\U0001D6FD";
    }

}
