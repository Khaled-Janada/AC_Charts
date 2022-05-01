using System.Collections.ObjectModel;
using AcCharts.Chart.ChartElements;
using AcCharts.Drawing;
using AcCharts.Statistics;

namespace AcCharts.Chart;

public class ChartSpecs {

    #region Properties
    public ReadOnlyCollection<IDistribution> Distributions { get; }

    public ReadOnlyCollection<Line> StateLines { get; }

    public ControlLine Ccl { get; }
    public ControlLine Lcl { get; }
    public ControlLine Ucl { get; }
    #endregion

    public ChartSpecs(List<IDistribution> distributions) {
        Distributions = distributions.AsReadOnly();

        StateLines = distributions.Select(
            (dist, n) => new Line($"S{n + 1}", (0, dist.Median), (double.PositiveInfinity, dist.Median))).ToList().AsReadOnly();

        var ccl = new List<Point> { new(0, 0) };
        var lcl = new List<Point> { new(0, 0) };
        var ucl = new List<Point> { new(0, 0) };

        var orderedDists = distributions.ToList();
        orderedDists.Sort();

        foreach (var dist in orderedDists) {
            ccl.Add(new Point(dist.Median, dist.Median));
            lcl.Add(new Point(dist.LowerPoint, dist.Median));
            ucl.Add(new Point(dist.UpperPoint, dist.Median));
        }

        Ccl = new ControlLine(ControlLineType.Acl, ccl);
        Lcl = new ControlLine(ControlLineType.Alcl, lcl);
        Ucl = new ControlLine(ControlLineType.Aucl, ucl);
    }

}