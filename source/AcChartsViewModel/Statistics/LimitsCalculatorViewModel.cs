﻿using AcCharts.Chart.ChartElements;
using AcCharts.Shared;
using Infinity.Extensions;

namespace AcCharts.Statistics;

public class LimitsCalculatorViewModel : ControlBaseViewModel {

    #region Properties
    public DistributionListViewModel DistributionList { get; }
    public ObservableCollection<AngularControlLimits> AngularControlLimitsList { get; }
    #endregion

    #region Constructors
    public LimitsCalculatorViewModel(IEnumerable<IDistribution>? distributions) {
        DistributionList = distributions is null ?
            new DistributionListViewModel(true) : new DistributionListViewModel(distributions, true);

        AngularControlLimitsList = new ObservableCollection<AngularControlLimits>(
            DistributionList.ToDistributionList().Select(dist => dist.CalculateAngularLimits(ChartScale)));

        DistributionList.ForEach((dist, index) => { dist.PropertyChanged += (_, _) => UpdateLimits(dist, index); });

        DistributionList.ItemAdded += (_, args) => {
            AngularControlLimitsList.Add(args.Item.ToDistribution().CalculateAngularLimits(ChartScale));
            args.Item.PropertyChanged += (_, _) => UpdateLimits(args.Item, args.Index);
        };

        DistributionList.ItemRemoved += (_, args) => {
            AngularControlLimitsList.RemoveAt(args.Index);
            args.Item.Dispose();
        };
    }
    #endregion

    #region Overrides
    protected override void UpdateScale() => UpdateLimits();
    #endregion

    #region Private Methods
    private void UpdateLimits(DistributionViewModel distribution, int index) {
        AngularControlLimitsList[index] = distribution.ToDistribution().CalculateAngularLimits(ChartScale);
    }

    private void UpdateLimits() {
        DistributionList.ForEach(UpdateLimits);
    }
    #endregion

}