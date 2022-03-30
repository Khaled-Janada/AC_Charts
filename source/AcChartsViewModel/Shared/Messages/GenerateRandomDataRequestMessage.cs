using AcCharts.Chart;

namespace AcCharts.Shared.Messages;

public record GenerateRandomDataRequestMessage : RequestViewModelMessage<RandomDataGeneratorViewModel, IList<ObservationData>> {

    public override RandomDataGeneratorViewModel RequestViewModel { get; }

    public GenerateRandomDataRequestMessage(ChartSpecs chartSpecs, int maxNumPoints) {
        RequestViewModel = new RandomDataGeneratorViewModel(chartSpecs, maxNumPoints);
    }

}