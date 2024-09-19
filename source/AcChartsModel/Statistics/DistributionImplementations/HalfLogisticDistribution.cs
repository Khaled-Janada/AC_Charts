namespace AcCharts.Statistics.DistributionImplementations;

public class HalfLogisticDistribution : IDistribution{
    public DistributionParameters Parameters { get; init; }
    public double Mean { get; }
    public double Median { get; }
    
    public HalfLogisticDistribution(DistributionParameters parameters) {
        Parameters = parameters;
        
        Mean = Math.Log(4) / Parameters.Scale + Parameters.Location;
        Median = Math.Log(3) / Parameters.Scale + Parameters.Location;
    }
    
    public double Quantile(double probability) {
        var g = (1 + probability) / (1 - probability);
        return Math.Log(g) / Parameters.Scale + Parameters.Location;
    }

    public double Sample() {
        return Quantile(new Random().NextDouble());
    }
}