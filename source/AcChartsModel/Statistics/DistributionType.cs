namespace AcCharts.Statistics;

public enum DistributionType {

    Erlang,
    Exponential,
    Frechet,
    Gamma,
    LogNormal,
    Rayleigh,
    Weibull

}

public static class DistributionTypeExt {

    public static bool HasShape(this DistributionType type) {
        return type is not (DistributionType.Exponential or DistributionType.Rayleigh);
    }

    public static bool IsShapeInteger(this DistributionType type) {
        return type is DistributionType.Erlang;
    }
    
    public static string ShpaeParameterSymbol(this DistributionType type) {
        return type is DistributionType.LogNormal ? "\U0001D70E" : "\U0001D6FD";
    }

}
