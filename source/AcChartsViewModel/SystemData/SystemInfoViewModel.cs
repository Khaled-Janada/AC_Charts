using AcCharts.Statistics;

namespace AcCharts.SystemData;

public class SystemInfoViewModel : DialogViewModel, IRequestViewModel<string>, ISystemDataViewModel {

    #region Private Fields
    private string _name;
    #endregion

    #region Properties
    public DistributionListViewModel DistributionListViewModel { get; }

    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public string Response { get; private set; }
    #endregion

    #region Constructors
    public SystemInfoViewModel(string name, IEnumerable<IDistribution> distributions) {
        Response = _name = name;
        DistributionListViewModel = new DistributionListViewModel(distributions, isEditable: false);
    }
    #endregion

    protected override void FinishOk() {
        base.FinishOk();
        Response = Name;
    }

}