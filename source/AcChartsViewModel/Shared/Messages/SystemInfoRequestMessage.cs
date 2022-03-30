using AcCharts.Statistics;
using AcCharts.SystemData;

namespace AcCharts.Shared.Messages;

public record SystemInfoRequestMessage : RequestViewModelMessage<SystemInfoViewModel, string> {

    public override SystemInfoViewModel RequestViewModel { get; }

    public SystemInfoRequestMessage(string name, IEnumerable<IDistribution> distributions) {
        RequestViewModel = new SystemInfoViewModel(name, distributions);
    }

}