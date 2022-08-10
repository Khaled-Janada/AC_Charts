using Infinity.Collections;
using Infinity.Extensions;

namespace AcCharts.Statistics;

public class DistributionListViewModel : CounterCollection<DistributionViewModel> {

    #region Constants
    private const ushort _MinLength = 1;
    private const ushort _MaxLength = 9;
    private const ushort _InitialCount = 2;
    private const string _CounterName = "No. of Transitions";

    public const string _ListTitle = "State-Transitions' Probability Distributions";

    public static readonly ReadOnlyCollection<DistributionType> DistributionTypes = Enum<DistributionType>.ToList();
    #endregion

    #region Properties
    public bool IsEditable { get; }
    #endregion

    #region Constructors
    private DistributionListViewModel() : base(() => new DistributionViewModel()) {
        Counter.Name = _CounterName;
        Counter.MinValue = _MinLength;
        Counter.MaxValue = _MaxLength;
    }

    public DistributionListViewModel(bool isEditable) : this() {
        IsEditable = isEditable;
        Counter.Value = _InitialCount;
    }

    public DistributionListViewModel(IEnumerable<IDistribution> distributions, bool isEditable) : this() {
        IsEditable = isEditable;
        foreach (var distribution in distributions) Add(new DistributionViewModel(distribution));
    }
    #endregion

    #region Public Methods
    public List<IDistribution> ToDistributionList() {
        return this.Select(distVm => distVm.ToDistribution()).ToList();
    }
    #endregion

}
