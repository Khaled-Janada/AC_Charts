using AcCharts.Statistics;
using Infinity.ComponentModel.Numeric;

namespace AcCharts.Chart;

public class RandomDataGeneratorViewModel : DialogViewModel, IRequestViewModel<IList<ObservationData>> {

    #region Public Properties
    public DistributionListViewModel DistributionList { get; }
    public BindableInt NumPoints { get; }

    public IList<ObservationData> Response { get; }
    #endregion

    public RandomDataGeneratorViewModel(ChartSpecs chartSpecs, int maxNumPoints) {
        DistributionList = new DistributionListViewModel(chartSpecs.Distributions, true) {
            Counter = { MaxValue = chartSpecs.Distributions.Count, MinValue = chartSpecs.Distributions.Count }
        };

        NumPoints = new BindableInt { Name = "Number of Observations", MinValue = 1, MaxValue = maxNumPoints };
        Response = new Collection<ObservationData>();
    }

    #region Overrides
    protected override void FinishOk() {
        base.FinishOk();

        var distributions = DistributionList.ToDistributionList();

        for (int n = 0; n < NumPoints.Value; n++) {
            (int index, double ttf) = IDistribution.GenerateRandomTransition(distributions);
            Response.Add(new ObservationData(ttf, index + 1));
        }

        // for (int n = 0; n < NumPoints.Value; n++) {
        //     (double value, int index) = BasicMath.Min(distributions.Select(dist => dist.Sample()).ToList());
        //     Response.Add(new ObservationData(value, index + 1));
        // }
    }
    #endregion

}

public record ObservationData(double TimeToFail, int State);
