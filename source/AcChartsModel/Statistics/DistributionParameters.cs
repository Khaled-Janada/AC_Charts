using System.Text.Json.Serialization;
using Infinity.Extensions;
using Infinity.Serialization;

namespace AcCharts.Statistics;

public class DistributionParameters {

    #region Properties
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DistributionType Type { get; }

    public double Scale { get; }

    [JsonConverter(typeof(NaNConverter))] public double Shape { get; }

    public double Location { get; }
    #endregion

    public DistributionParameters(DistributionType type, double scale, double shape, double location) {
        if (!type.HasShape() && !double.IsNaN(shape))
            throw new ArgumentException($"The {type.ToString().SeparateCamelCase()} doesn't have a shape parameter.");

        if (type.IsShapeInteger() && shape % 1 != 0)
            throw new ArgumentException($"The shape parameter of the {type.ToString().SeparateCamelCase()} must be an integer.");

        (Type, Scale, Shape, Location) = (type, scale, shape, location);
    }

}
