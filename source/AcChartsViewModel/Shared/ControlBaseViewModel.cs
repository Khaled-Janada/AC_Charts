using AcCharts.Drawing;
using Infinity.Extensions;

namespace AcCharts.Shared;

public abstract class ControlBaseViewModel : ViewModelBase {

    #region Static
    public static ReadOnlyDictionary<ScaleType, string> ChartScaleTypes =>
        Enum<ScaleType>.ToStringDictionary(StringExt.SeparateCamelCase);
    #endregion

    #region Fields
    private ScaleType _scaleType;
    #endregion

    #region Properties
    public ScaleType ScaleType { get => _scaleType; set => SetProperty(ref _scaleType, value, _ => UpdateScale()); }

    protected ChartScale ChartScale => ChartScale.ChartScales[ScaleType];
    #endregion

    protected abstract void UpdateScale();

}