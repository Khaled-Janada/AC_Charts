using AcCharts.Drawing.DrawingElements;
using LiveCharts.Configurations;
using LiveCharts.Defaults;

namespace AcCharts.Drawing;

public class ChartViewModel : ViewModelBase {

    #region Static
    public static double MinValue => 0;
    private static readonly Func<double, string> DefaultLabelFormatter = value => $"{value:N1}";
    #endregion

    #region Fields
    private ChartScale _chartScale = ChartScale.ChartScales[ScaleType.Linear];
    private Func<double, string> _labelFormatter = DefaultLabelFormatter;

    private double _xAxisPadding = 1.05;
    private double _xYxisPadding = 1.05;

    private double _xMax;
    private double _yMax;

    private bool _usePaddingForXAxis = true;
    private bool _usePaddingForYAxis = true;

    private AxisTitleViewModel? _xAxisTitle;
    private AxisTitleViewModel? _yAxisTitle;
    #endregion

    #region Public Properties
    public ChartSeriesCollection ChartSeriesCollection { get; }
    public ChartScale ChartScale { get => _chartScale; set => SetProperty(ref _chartScale, value, SetScale); }

    public Func<double, string> LabelFormatter { get => _labelFormatter; private set => SetProperty(ref _labelFormatter, value); }

    public double XAxisPadding {
        get => _xAxisPadding;
        set => SetProperty(ref _xAxisPadding, value, _ => OnPropertyChanged(nameof(XMax)));
    }

    public double YAxisPadding {
        get => _xYxisPadding;
        set => SetProperty(ref _xYxisPadding, value, _ => OnPropertyChanged(nameof(YMax)));
    }

    public double XMax {
        get => UsePaddingForXAxis ? ChartScale.Scale(_xMax) * _xAxisPadding : ChartScale.Scale(_xMax);
        set => SetProperty(ref _xMax, value);
    }

    public double YMax {
        get => UsePaddingForYAxis ? ChartScale.Scale(_yMax) * _xYxisPadding : ChartScale.Scale(_yMax);
        set => SetProperty(ref _yMax, value);
    }

    public bool UsePaddingForXAxis {
        get => _usePaddingForXAxis;
        set => SetProperty(ref _usePaddingForXAxis, value, _ => OnPropertyChanged(nameof(XMax)));
    }

    public bool UsePaddingForYAxis {
        get => _usePaddingForYAxis;
        set => SetProperty(ref _usePaddingForYAxis, value, _ => OnPropertyChanged(nameof(YMax)));
    }

    public AxisTitleViewModel? XAxisTitle { get => _xAxisTitle; set => SetProperty(ref _xAxisTitle, value); }
    public AxisTitleViewModel? YAxisTitle { get => _yAxisTitle; set => SetProperty(ref _yAxisTitle, value); }
    #endregion

    public ChartViewModel() {
        ChartSeriesCollection = new ChartSeriesCollection();
        LabelFormatter = DefaultLabelFormatter;
    }

    #region Public Methods
    public void AddElement(DrawingElement element) {
        ChartSeriesCollection.Add(element);
    }

    public void AddElements(IEnumerable<DrawingElement> elements) {
        ChartSeriesCollection.AddRange(elements);
    }

    public void RemoveElement(DrawingElement element) {
        ChartSeriesCollection.Remove(element);
    }

    public void AddLine(LineViewModel line) {
        ChartSeriesCollection.Add(line);
        ChartSeriesCollection.Add(line.Label);
    }

    public void AddLines(IEnumerable<LineViewModel> lines) {
        foreach (var line in lines) AddLine(line);
    }
    #endregion

    #region Private Methods
    private void SetScale(ChartScale chartScale) {
        LabelFormatter = d => DefaultLabelFormatter(ChartScale.InverseScale(d));

        OnPropertyChanged(nameof(XMax));
        OnPropertyChanged(nameof(YMax));

        ChartSeriesCollection.Configuration =
            new CartesianMapper<ObservablePoint>().X(point => chartScale.Scale(point.X)).Y(point => chartScale.Scale(point.Y));
    }
    #endregion

}
