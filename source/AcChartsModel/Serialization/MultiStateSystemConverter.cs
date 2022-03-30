using Infinity.Serialization;

namespace AcCharts.Serialization;

internal class MultiStateSystemConverter : JsonConverterBase<MultiStateSystem, MultiStateSystemData> {

    protected override MultiStateSystem Read(MultiStateSystemData modelData) {
        return modelData.ToMultiStateSystem();
    }

    protected override MultiStateSystemData Write(MultiStateSystem model) {
        return MultiStateSystemData.ToData(model);
    }

}