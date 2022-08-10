using System.Text.Json;
using System.Text.Json.Serialization;
using AcCharts.Chart;
using AcCharts.Statistics;
using Infinity.Collections;
using Infinity.Services;

namespace AcCharts;

public class MultiStateSystemViewModel : ViewModelBase {

    #region Static
    private static Serializer Serializer { get; }

    static MultiStateSystemViewModel() {
        Serializer = new Serializer(new JsonSerializerOptions {
            WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        });
    }
    #endregion

    #region Fields
    private AcChartViewModel? _currentChart;
    private const ushort _InitialChartCount = 2;
    #endregion

    #region Public Properties
    private MultiStateSystem Model { get; }
    public string Name { get => Model.Name; set => SetProperty(Model, value); }

    public IEnumerable<IDistribution> Distributions => Model.ChartSpecs.Distributions;

    public AcChartViewModel? CurrentChart { get => _currentChart; set => SetProperty(ref _currentChart, value); }
    public BindingLockedCollection<AcChart, AcChartViewModel> Charts { get; }
    #endregion

    #region Constructors
    private MultiStateSystemViewModel(MultiStateSystem model) {
        Model = model;

        Charts = new BindingLockedCollection<AcChart, AcChartViewModel>(Model.Charts, chart => new AcChartViewModel(chart));
        if (Charts.Count != 0) CurrentChart = Charts[0];
    }

    public MultiStateSystemViewModel(string name, List<IDistribution> distributions) :
        this(new MultiStateSystem(name, distributions)) {
        Charts.Counter.Value = _InitialChartCount;
        CurrentChart = Charts[0];
    }
    #endregion

    #region System Commands
    public void SaveSystemExecute() {
        Serializer.SaveWithDialog(Model, Model.Name, "Multi-State System", "mss");
    }

    public static MultiStateSystemViewModel? OpenSystemExecute(MultiStateSystemViewModel? current) {
        var newSystem = Serializer.LoadWithDialog<MultiStateSystem>("Multi-State System", "mss");
        return newSystem is null ? current : new MultiStateSystemViewModel(newSystem);
    }

    public void RenameSystemExecute() {
        Name = Request<RenameRequestMessage, string>(new RenameRequestMessage(Name, "Multi-State System"));
    }

    public void SystemInfoExecute() {
        Name = Request<SystemInfoRequestMessage, string>(new SystemInfoRequestMessage(Name, Distributions));
    }
    #endregion

    #region Charts Commands
    public void AddChart() {
        Charts.AddNew();
        CurrentChart = Charts[^1];
    }

    public void DeleteChart() {
        if (CurrentChart is not null) Charts.Remove(CurrentChart);
    }

    public void RenameChart() {
        if (CurrentChart is not null) CurrentChart.Name = Send(new RenameRequestMessage(CurrentChart.Name, "AC Chart")).Response;
    }

    public void GenerateRandomExecute() {
        if (CurrentChart is null) return;
        var maxNumPoints = AcChartViewModel.MaxObservations - CurrentChart.Observations.Count;

        CurrentChart.AddObservations(Send(new GenerateRandomDataRequestMessage(Model.ChartSpecs, maxNumPoints)).Response);
    }
    #endregion

}