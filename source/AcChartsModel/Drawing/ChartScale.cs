using System.Collections.ObjectModel;
using Infinity.Extensions;
using Infinity.Math;

namespace AcCharts.Drawing;

public delegate double Mapper(double value);

public class ChartScale {

    #region Static
    public static ReadOnlyDictionary<ScaleType, ChartScale> ChartScales { get; }

    static ChartScale() {
        ChartScales = Enum<ScaleType>.ToDictionary(scaleType => new ChartScale(scaleType));
    }
    #endregion

    #region Properties
    private Mapper ScaleMapper { get; }
    private Mapper InverseScaleMapper { get; }
    #endregion

    #region Constructors
    private ChartScale(ScaleType scaleType) {
        if (scaleType == ScaleType.Linear) {
            ScaleMapper = InverseScaleMapper = d => d;
        }
        else {
            double root = scaleType switch {
                ScaleType.SquareRoot  => 2,
                ScaleType.CubicRoot   => 3,
                ScaleType.QuarticRoot => 4,
                _                     => throw new ArgumentOutOfRangeException(nameof(scaleType), scaleType, null)
            };
            ScaleMapper = d => BasicMath.Root(d, root);
            InverseScaleMapper = d => Math.Pow(d, root);
        }
    }
    #endregion

    #region Public Methods
    public double Scale(double value) => ScaleMapper(value);

    public (double s1, double s2, double s3) Scale(double v1, double v2, double v3) => (Scale(v1), Scale(v2), Scale(v3));

    public double InverseScale(double value) => InverseScaleMapper(value);
    #endregion

}
