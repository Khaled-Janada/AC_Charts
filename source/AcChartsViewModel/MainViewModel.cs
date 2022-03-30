using AcCharts.Options;

namespace AcCharts;

public class MainViewModel : ViewModelBase {

    #region Fields
    private MultiStateSystemViewModel? _system;
    #endregion

    #region Properties
    public MultiStateSystemViewModel? System { get => _system; set => SetProperty(ref _system, value); }
    #endregion

    #region Commands
    // System Commands
    public Command NewSystemCommand { get; }
    public Command CloseSystemCommand { get; }
    public Command OpenSystemCommand { get; }
    public Command SaveSystemCommand { get; }
    public Command RenameSystemCommand { get; }
    public Command SystemInfoCommand { get; }

    // Chart Commands
    public Command AddChartCommand { get; }
    public Command DeleteChartCommand { get; }
    public Command ClearChartCommand { get; }
    public Command GenerateRandomDataCommand { get; }
    public Command RenameChartCommand { get; }

    // Tools Commands
    public Command LimitsCalculatorCommand { get; }
    public Command LimitsPlotterCommand { get; }

    // Export Commands
    public Command WindowScreenShotCommand { get; }
    public Command ChartScreenShotCommand { get; }

    // Options Commands
    public Command WindowScreenShotDpiCommand { get; }
    public Command ChartScreenShotDpiCommand { get; }
    #endregion

    public MainViewModel() {
        // System Commands
        NewSystemCommand = new Command(NewSystemExecute);
        CloseSystemCommand = new Command(() => System = null, SystemIsNotNull);
        OpenSystemCommand = new Command(() => System = MultiStateSystemViewModel.OpenSystemExecute(System));
        SaveSystemCommand = new Command(() => System?.SaveSystemExecute(), SystemIsNotNull);
        RenameSystemCommand = new Command(() => System?.RenameSystemExecute(), SystemIsNotNull);
        SystemInfoCommand = new Command(() => System?.SystemInfoExecute(), SystemIsNotNull);

        // Chart Commands
        AddChartCommand = new Command(() => System?.AddChart(), SystemIsNotNull);
        DeleteChartCommand = new Command(() => System?.DeleteChart(), ChartIsNotNull);
        RenameChartCommand = new Command(() => System?.RenameChart(), ChartIsNotNull);
        ClearChartCommand = new Command(() => System?.CurrentChart?.ClearChart(), ChartIsNotNull);
        GenerateRandomDataCommand = new Command(() => System?.GenerateRandomExecute(), ChartIsNotNull);

        // Tools Commands
        LimitsCalculatorCommand = new Command(LimitsCalculatorExecute);
        LimitsPlotterCommand = new Command(LimitsPlotterExecute);

        // Export Commands
        WindowScreenShotCommand = new Command(() => Send(new ScreenShotMessage("Window", DpiOptionViewModel.WindowDpi)));
        ChartScreenShotCommand = new Command(() => Send(new ScreenShotMessage("Chart", DpiOptionViewModel.ChartDpi), 1));

        // Options Commands
        WindowScreenShotDpiCommand = new Command(WindowScreenShotDpiExecute);
        ChartScreenShotDpiCommand = new Command(ChartScreenShotDpiExecute);
    }

    #region General
    private bool SystemIsNotNull() => System is not null;

    private bool ChartIsNotNull() => System?.CurrentChart is not null;
    #endregion

    #region System Commands
    private void NewSystemExecute() {
        System = Send(new NewSystemRequestMessage()).Response ?? System;
    }
    #endregion

    #region Tools Commands
    private void LimitsCalculatorExecute() {
        Send(new LimitsCalculatorMessage(System?.Distributions));
    }

    private void LimitsPlotterExecute() {
        Send(new LimitsPlotterMessage());
    }
    #endregion

    #region Options Commands
    private void WindowScreenShotDpiExecute() {
        DpiOptionViewModel.WindowDpi = Request<DpiRequestMessage, int>(new DpiRequestMessage("Window", DpiOptionViewModel.WindowDpi));
    }

    private void ChartScreenShotDpiExecute() {
        DpiOptionViewModel.ChartDpi = Request<DpiRequestMessage, int>(new DpiRequestMessage("Chart", DpiOptionViewModel.ChartDpi));
    }
    #endregion

}
