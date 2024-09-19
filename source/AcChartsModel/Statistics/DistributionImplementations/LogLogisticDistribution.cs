namespace AcCharts.Statistics.DistributionImplementations;

public class LogLogisticDistribution : IDistribution{
    public DistributionParameters Parameters { get; init; }
    public double Mean { get; }
    public double Median { get; }
    
    public LogLogisticDistribution(DistributionParameters parameters) {
        Parameters = parameters;
        
        Mean = Parameters.Scale * Math.PI / (Parameters.Shape * Math.Sin(Math.PI / Parameters.Shape)) + Parameters.Location;
        Median = Parameters.Scale + Parameters.Location;
    }
    
    public double Quantile(double probability) {
        return Parameters.Scale * Math.Pow(probability / (1 - probability), 1 / Parameters.Shape) + Parameters.Location;
    }

    public double Sample() {
        return Quantile(new Random().NextDouble());
    }
}