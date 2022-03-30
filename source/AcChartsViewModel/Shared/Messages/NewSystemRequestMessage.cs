using AcCharts.SystemData;

namespace AcCharts.Shared.Messages;

public record NewSystemRequestMessage : RequestViewModelMessage<NewSystemViewModel, MultiStateSystemViewModel?> {

    public override NewSystemViewModel RequestViewModel { get; } = new();

}