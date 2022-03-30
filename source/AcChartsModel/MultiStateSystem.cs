using System.Text.Json.Serialization;
using AcCharts.Chart;
using AcCharts.Serialization;
using AcCharts.Statistics;
using Infinity.Collections;

namespace AcCharts;

[JsonConverter(typeof(MultiStateSystemConverter))]
public class MultiStateSystem {

    #region Public Properties
    public string Name { get; set; }
    public ChartSpecs ChartSpecs { get; }
    public LockedCollection<AcChart> Charts { get; }
    #endregion

    #region Constructors
    public MultiStateSystem(string name, List<IDistribution> distributions) {
        Name = name;
        ChartSpecs = new ChartSpecs(distributions);
        Charts = new LockedCollection<AcChart>(AddChart);
    }

    internal MultiStateSystem(string name, List<IDistribution> distributions, IEnumerable<AcChartData> chartDataList) {
        Name = name;
        ChartSpecs = new ChartSpecs(distributions);

        Charts = new LockedCollection<AcChart>(chartDataList.Select(AddChart), AddChart);
    }
    #endregion

    #region Private Methods
    private AcChart AddChart() {
        return new AcChart($"Chart {Charts.Count + 1}", ChartSpecs);
    }

    private AcChart AddChart(AcChartData chartData) {
        return new AcChart(chartData.Name, ChartSpecs, chartData.ObservationDataList);
    }
    #endregion

}
