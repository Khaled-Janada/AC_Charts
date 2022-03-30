using System.Text.Json.Serialization;
using AcCharts.Chart.ChartElements;

namespace AcCharts.Serialization;

internal record ObservationData(
    [property: JsonPropertyName("S")] int StateNumber,
    [property: JsonPropertyName("T")] double TimeToFail) {

    public static ObservationData ToData(Observation observation) {
        return new ObservationData(observation.StateNumber, observation.TimeToFail);
    }

}
