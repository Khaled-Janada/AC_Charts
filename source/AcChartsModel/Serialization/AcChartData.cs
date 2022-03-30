using System.Text.Json.Serialization;
using AcCharts.Chart;

namespace AcCharts.Serialization;

internal record AcChartData(string Name, [property: JsonPropertyName("Data")] IEnumerable<ObservationData> ObservationDataList) {

    public static AcChartData ToData(AcChart chart) {
        return new AcChartData(chart.Name, chart.Observations.Select(ObservationData.ToData));
    }

}
