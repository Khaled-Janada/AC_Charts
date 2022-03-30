namespace AcCharts.Chart.ChartElements;

public class Observation {

    #region Fields
    private int _stateNumber = 1;
    #endregion

    #region Properties
    public double TimeToFail { get; set; }
    public double Median => ChartSpecs.Distributions[StateNumber - 1].Median;

    public int StateNumber {
        get => _stateNumber;
        set {
            if (value < 1 || value > ChartSpecs.Distributions.Count) 
                throw new ArgumentOutOfRangeException($"There is only {ChartSpecs.Distributions.Count} states");
            _stateNumber = value;
        }
    }

    public string Label { get; }

    private ChartSpecs ChartSpecs { get; }
    #endregion

    #region Constructors
    public Observation(ChartSpecs chartSpecs, int index) {
        ChartSpecs = chartSpecs;
        Label = index.ToString();
    } 
    #endregion

}
