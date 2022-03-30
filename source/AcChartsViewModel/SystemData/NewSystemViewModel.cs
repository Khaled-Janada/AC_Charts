using AcCharts.Statistics;

namespace AcCharts.SystemData;

public class NewSystemViewModel : DialogViewModel, IRequestViewModel<MultiStateSystemViewModel?>, ISystemDataViewModel {

    #region Private Fields
    private string _name = "Multi-State System";
    #endregion

    #region Public Properties
    public string Title => "New Multi-State System";
    public string Name { get => _name; set => SetProperty(ref _name, value); }

    public MultiStateSystemViewModel? Response { get; private set; }
    public DistributionListViewModel DistributionListViewModel { get; }
    #endregion

    public NewSystemViewModel() {
        DistributionListViewModel = new DistributionListViewModel(isEditable: true);
    }

    protected override void FinishOk() {
        base.FinishOk();
        Response = new MultiStateSystemViewModel(Name, DistributionListViewModel.ToDistributionList());
    }

}
