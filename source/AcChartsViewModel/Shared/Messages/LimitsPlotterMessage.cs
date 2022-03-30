using AcCharts.Statistics;

namespace AcCharts.Shared.Messages;

public record LimitsPlotterMessage : Message, IViewModelMessage {

    public ViewModelBase ViewModel { get; } = new LimitsPlotterViewModel();

}
