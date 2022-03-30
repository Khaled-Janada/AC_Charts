using Meta.Numerics.Statistics.Distributions;

namespace AcCharts.Statistics.DistributionImplementations;

internal class MetaDistribution : IDistribution {

    #region Public Properties
    public DistributionParameters Parameters { get; init; }

    public double Mean { get; }
    public double Median { get; }
    #endregion

    #region Private Methods
    private ContinuousDistribution InnerMetaDistribution { get; }
    #endregion

    internal MetaDistribution(DistributionParameters parameters) {
        Parameters = parameters;

        InnerMetaDistribution = Parameters.Type switch {
            DistributionType.Erlang      => new GammaDistribution(shape: Parameters.Shape, scale: Parameters.Scale),
            DistributionType.Exponential => new ExponentialDistribution(mu: Parameters.Scale),
            DistributionType.Frechet     => new FrechetDistribution(shape: Parameters.Shape, scale: Parameters.Scale),
            DistributionType.Gamma       => new GammaDistribution(shape: Parameters.Shape, scale: Parameters.Scale),
            DistributionType.LogNormal   => new LognormalDistribution(mu: Parameters.Scale, sigma: Parameters.Shape),
            DistributionType.Rayleigh    => new RayleighDistribution(scale: Parameters.Scale),
            DistributionType.Weibull     => new WeibullDistribution(scale: Parameters.Scale, shape: Parameters.Shape),
            _                            => throw new ArgumentOutOfRangeException()
        };

        Mean = InnerMetaDistribution.Mean     + Parameters.Location;
        Median = InnerMetaDistribution.Median + Parameters.Location;
    }

    public double Quantile(double probability) {
        return InnerMetaDistribution.InverseLeftProbability(probability) + Parameters.Location;
    }

    public double Sample() {
        return InnerMetaDistribution.GetRandomValue(new Random()) + Parameters.Location;
    }

}
