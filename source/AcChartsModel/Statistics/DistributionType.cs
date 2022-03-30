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

}