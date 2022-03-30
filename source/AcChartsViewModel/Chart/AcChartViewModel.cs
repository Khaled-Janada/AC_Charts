using System.Windows;
using System.Windows.Documents;
using AcCharts.Chart.ChartElements;
using AcCharts.Drawing;
using AcCharts.Shared;
using Infinity.Collections;
using Infinity.Collections.Events;
using Infinity.ComponentModel.Interfaces;
using Infinity.Events;
using Infinity.Math;

namespace AcCharts.Chart;

public class AcChartViewModel : ControlBaseViewModel, IHasName {

    #region Static
    private const double _PointPadding = 1.1;
    private const double _CLsLabelPadding = 1.02;
    internal const int MaxObservations = 60;

    private const string _XTitle = "Observed Time to Failure (\U0001D461)";

    private static readonly IList<Run> YAxisTitle = new List<Run> {
        new("Median Time To Failure ("), new("T") { FontStyle = FontStyles.Italic },
        new("C") { BaselineAlignment = BaselineAlignment.Subscript, FontStyle = FontStyles.Italic, FontSize = 9 }, new(")")
    };
    #endregion

    #region Private Properties
    private StateLines StateLines { get; }
    private ControlLines ControlLines { get; }

    private double OriginalMax { get; }
    private double MaxWithPadding => OriginalMax * ChartScale.InverseScale(_CLsLabelPadding);

    private double XMax =>
        Model.Observations.Count > 0 ?
            Math.Max(Model.Observations.Select(obs => obs.TimeToFail).Max() * _PointPadding, MaxWithPadding) : MaxWithPadding;

    private double CurrentXMax { get; set; }
    #endregion

    #region Public Properties
    private AcChart Model { get; }
    public string Name { get => Model.Name; set => SetProperty(Model.Name, value, str => Model.Name = str); }

    public ChartViewModel Chart { get; }
    public BindingLockedCollection<Observation, ObservationViewModel> Observations { get; }

    public IReadOnlyList<int> States { get; }
    #endregion

    public AcChartViewModel(AcChart model) {
        Model = model;
        OriginalMax = Model.ChartSpecs.Distributions.Select(dist => dist.Median).Max();

        Chart = new ChartViewModel {
            XAxisTitle = new AxisTitleViewModel(_XTitle), YAxisTitle = new AxisTitleViewModel(YAxisTitle), XMax = OriginalMax,
            YMax = OriginalMax, UsePaddingForXAxis = false
        };

        Observations = new BindingLockedCollection<Observation, ObservationViewModel>(Model.Observations, AddObservation) {
            Counter = { MinValue = 0, MaxValue = MaxObservations }
        };
        Observations.ItemRemoved += RemoveObservation;

        States = Enumerable.Range(1, Model.ChartSpecs.Distributions.Count).ToList().AsReadOnly();
        StateLines = new StateLines(Model.ChartSpecs.StateLines);
        ControlLines = new ControlLines(Model.ChartSpecs);

        Chart.AddLines(StateLines);
        Chart.AddLines(ControlLines);
        Chart.AddElements(Observations);

        SetScale();
        SetXMax(XMax);
    }

    #region Public Methods
    public void AddObservations(IEnumerable<ObservationData> observationData) {
        foreach (var data in observationData) {
            var observation = Observations.AddNew();
            (observation.TimeToFail, observation.State) = data;
        }

        SetXMax(XMax);
    }

    public void ClearChart() {
        Observations.Clear();
        for (int n = Chart.ChartSeriesCollection.Count - 1; n >= 0; n--) {
            if (Chart.ChartSeriesCollection[n] is ObservationViewModel) Chart.ChartSeriesCollection.RemoveAt(n);
        }

        SetXMax(XMax);
    }
    #endregion

    #region Overrides
    protected override void UpdateScale() => SetScale();
    #endregion

    #region Private Methods
    private ObservationViewModel AddObservation(Observation observation) {
        var observationVm = new ObservationViewModel(observation);
        observationVm.TimeChanged += SetXMax;

        Chart.AddElement(observationVm);
        return observationVm;
    }

    private void RemoveObservation(object _, ItemRemovedEventArgs<ObservationViewModel> eventArgs) {
        var observationVm = eventArgs.Item;
        observationVm.TimeChanged -= SetXMax;

        Chart.RemoveElement(eventArgs.Item);
        SetXMax(XMax);
    }

    private void SetXMax(object _, ValueChangedEventArgs<double> eventArgs) => SetXMax(XMax);

    private void SetXMax(double value) {
        if (value.IsNearlyEqual(CurrentXMax)) return;

        Chart.XMax = CurrentXMax = value;

        StateLines.SetXMax(value);
        ControlLines.SetXMax(value);
    }

    private void SetScale() {
        Chart.ChartScale = ChartScale;

        Chart.YMax = MaxWithPadding;

        ControlLines.SetYMax(ChartScale.InverseScale(_CLsLabelPadding));
    }
    #endregion

}
