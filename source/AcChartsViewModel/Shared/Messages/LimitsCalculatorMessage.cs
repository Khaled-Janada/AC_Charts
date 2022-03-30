using AcCharts.Statistics;

namespace AcCharts.Shared.Messages;

public record LimitsCalculatorMessage : Message, IViewModelMessage {

    public ViewModelBase ViewModel { get; }

    public LimitsCalculatorMessage(IEnumerable<IDistribution>? distributions) {
        ViewModel = new LimitsCalculatorViewModel(distributions);
    }

}