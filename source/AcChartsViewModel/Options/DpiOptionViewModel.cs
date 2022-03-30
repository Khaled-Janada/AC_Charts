namespace AcCharts.Options;

public class DpiOptionViewModel : DialogViewModel, IRequestViewModel<int> {

    #region Fields
    private int _dpi;
    #endregion

    #region Static / Constants
    public static int MinDpi => 150;
    public static int MaxDpi => 1200;

    internal static int WindowDpi { get; set; } = 600;
    internal static int ChartDpi { get; set; } = 600;
    #endregion

    #region Properties
    public int Dpi { get => _dpi; set => SetProperty(ref _dpi, value); }

    public string ObjectType { get; }
    public string Message => $"{ObjectType} screenshot resolution:";

    public int Response { get; private set; }
    #endregion

    public DpiOptionViewModel(string objectType, int dpi) {
        Response = _dpi = dpi;
        ObjectType = objectType;
    }

    protected override void FinishOk() {
        base.FinishOk();
        Response = Dpi;
    }

}
