using AcCharts.Options;

namespace AcCharts.Shared.Messages;

public record DpiRequestMessage : RequestViewModelMessage<DpiOptionViewModel, int> {

    public override DpiOptionViewModel RequestViewModel { get; }

    public DpiRequestMessage(string objectType, int dpi) {
        RequestViewModel = new DpiOptionViewModel(objectType, dpi);
    }

}
