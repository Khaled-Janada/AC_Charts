using System.Text.Json.Serialization;
using AcCharts.Statistics;

namespace AcCharts.Serialization;

internal record MultiStateSystemData(
    string Name, List<DistributionParameters> Distributions,
    [property: JsonPropertyName("Charts")] IEnumerable<AcChartData> ChartDataList) {

    public static MultiStateSystemData ToData(MultiStateSystem system) {
        return new MultiStateSystemData(system.Name,
            system.ChartSpecs.Distributions.Select(dist => dist.Parameters).ToList(), system.Charts.Select(AcChartData.ToData));
    }

    public MultiStateSystem ToMultiStateSystem() {
        return new MultiStateSystem(Name, Distributions.Select(IDistribution.CreateDistribution).ToList(), ChartDataList);
    }

}