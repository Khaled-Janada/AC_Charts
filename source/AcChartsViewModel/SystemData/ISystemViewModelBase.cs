using AcCharts.Statistics;

namespace AcCharts.SystemData;

public interface ISystemDataViewModel {

    #region Properties
    public string Name { get; set; }
    public DistributionListViewModel DistributionListViewModel { get; }
    #endregion

}
