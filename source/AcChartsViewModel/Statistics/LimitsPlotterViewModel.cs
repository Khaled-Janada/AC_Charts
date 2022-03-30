using System.Windows.Media;
using AcCharts.Drawing;
using AcCharts.Drawing.DrawingElements;
using AcCharts.Options;
using AcCharts.Shared;
using Infinity.Extensions;
using Infinity.Math;
using LiveCharts.Defaults;

namespace AcCharts.Statistics;

public class LimitsPlotterViewModel : ControlBaseViewModel {

    #region Static
    private const double _MaxShapeParameterValue = 3;
    private const int _NumPoints = 100;

    private static readonly IList<double> ShapeParameterRange = MatMath.LinSpace(BasicMath.Eps, _MaxShapeParameterValue, _NumPoints);

    public static readonly ReadOnlyCollection<DistributionType> PlottableDistributionTypes =
        Enum<DistributionType>.ToList().Where(distType => !distType.IsShapeInteger()).ToList().AsReadOnly();
    #endregion

    #region Fields
    private DistributionType _distributionType = DistributionType.Exponential;
    #endregion

    #region Private Properties
    private SmoothLine LclAngle { get; }
    private SmoothLine CclAngle { get; }
    private SmoothLine UclAngle { get; }
    #endregion

    #region Public Properties
    public ChartViewModel Chart { get; }

    public DistributionType DistributionType {
        get => _distributionType;
        set => SetProperty(ref _distributionType, value, _ => Update());
    }

    public Command ChartScreenShotCommand { get; }
    #endregion

    public LimitsPlotterViewModel() {
        ChartScreenShotCommand = new Command(() => Send(new ScreenShotMessage("Chart", DpiOptionViewModel.ChartDpi), 1));

        Chart = new ChartViewModel {
            XAxisTitle = new AxisTitleViewModel("Shape Parameter (\U0001D6FD)"),
            YAxisTitle = new AxisTitleViewModel("Angle (Degree)"), XMax = _MaxShapeParameterValue, YMax = 90,
            UsePaddingForXAxis = false
        };

        LclAngle = new SmoothLine("LCL") { Stroke = Brushes.Green };
        CclAngle = new SmoothLine("CCL") { Stroke = Brushes.Blue };
        UclAngle = new SmoothLine("UCL") { Stroke = Brushes.Red };

        Chart.AddLines(new[] { LclAngle, CclAngle, UclAngle });
        Update();
    }

    protected override void UpdateScale() => Update();

    private void Update() {
        var lclPoints = new List<ObservablePoint>(_NumPoints);
        var cclPoints = new List<ObservablePoint>(_NumPoints);
        var uclPoints = new List<ObservablePoint>(_NumPoints);

        foreach (var shape in ShapeParameterRange) {
            (double lcl, double ccl, double ucl) = IDistribution.CalculateAngularLimits(DistributionType, shape, 0, ChartScale);
            lclPoints.Add(new ObservablePoint(shape, lcl));
            cclPoints.Add(new ObservablePoint(shape, ccl));
            uclPoints.Add(new ObservablePoint(shape, ucl));
        }

        LclAngle.Update(lclPoints);
        CclAngle.Update(cclPoints);
        UclAngle.Update(uclPoints);
    }

}