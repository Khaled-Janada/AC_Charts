using AcCharts.Chart.ChartElements;
using AcCharts.Serialization;
using Infinity.Collections;

namespace AcCharts.Chart;

public class AcChart {

    #region Properties
    public string Name { get; set; }
    public ChartSpecs ChartSpecs { get; }
    public LockedCollection<Observation> Observations { get; }
    #endregion

    #region Constructors
    public AcChart(string name, ChartSpecs chartSpecs) {
        Name = name;
        ChartSpecs = chartSpecs;
        Observations = new LockedCollection<Observation>(AddObservation);
    }

    internal AcChart(string name, ChartSpecs chartSpecs, IEnumerable<ObservationData> observations) {
        Name = name;
        ChartSpecs = chartSpecs;
        Observations = new LockedCollection<Observation>(observations.Select(AddObservation).ToList(),
            AddObservation);
    }
    #endregion

    #region Private Methods
    private Observation AddObservation() {
        return new Observation(ChartSpecs, Observations.Count + 1);
    }

    private Observation AddObservation(ObservationData observationData, int index) {
        return new Observation(ChartSpecs, index + 1) {
            TimeToFail = observationData.TimeToFail, StateNumber = observationData.StateNumber
        };
    }
    #endregion

}
