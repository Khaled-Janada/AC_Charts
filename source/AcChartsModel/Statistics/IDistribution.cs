using AcCharts.Chart.ChartElements;
using AcCharts.Drawing;
using AcCharts.Statistics.DistributionImplementations;
using Infinity.Math;

namespace AcCharts.Statistics;

public interface IDistribution : IComparable<IDistribution> {

    #region Constants
    private const double _C = 0.0027;
    #endregion

    #region Properties
    public DistributionParameters Parameters { get; init; }

    public double Mean { get; }
    public double Median { get; }

    public double LowerPoint => Quantile(_C / 2);
    public double UpperPoint => Quantile(1 - _C / 2);
    #endregion

    #region Default Implementations
    int IComparable<IDistribution>.CompareTo(IDistribution? other) {
        return other is null ? 0 : Math.Sign(Median - other.Median);
    }

    public AngularControlLimits CalculateAngularLimits(ChartScale chartScale) {
        (double median, double lowerPoint, double upperPoint) = chartScale.Scale(Median, LowerPoint, UpperPoint);

        return new AngularControlLimits(BasicMath.Atand(median / lowerPoint), 45, BasicMath.Atand(median / upperPoint));
    }

    public void Deconstruct(out DistributionType type, out double scale, out double shape, out double location) {
        (type, scale, shape, location) = (Parameters.Type, Parameters.Scale, Parameters.Shape, Parameters.Location);
    }
    #endregion

    #region Public Methods
    public double Quantile(double probability);
    public double Sample();
    #endregion

    #region Static Methods
    internal static IDistribution CreateDistribution(DistributionParameters parameters) {
        return new MetaDistribution(parameters);
    }

    public static IDistribution CreateDistribution(DistributionType type, double scale, double shape, double location) {
        return CreateDistribution(new DistributionParameters(type, scale, shape, location));
    }

    public static AngularControlLimits CalculateAngularLimits(
        DistributionType type, double shape, double location, ChartScale chartScale) {
        var dist = CreateDistribution(type, 1, shape, location);
        return dist.CalculateAngularLimits(chartScale);
    }

    public static (int index, double ttf) GenerateRandomTransition(IList<IDistribution> distributions) {
        var arrow = new Random().NextDouble() * distributions.Select(dist => 1 / dist.Mean).Sum();
        var distance = 0.0;

        var index = distributions.Count - 1;

        for (int n = 0; n < distributions.Count - 1; n++) {
            distance += 1 / distributions[n].Mean;

            if (arrow > distance) continue;

            index = n;
            break;
        }

        return (index, distributions[index].Sample());
    }
    #endregion

}
