namespace AcCharts.Options;

[Dialog(typeof(DpiRequestMessage))]
public partial class DpiOptionView {

    public DpiOptionView() {
        Owner = Application.Current.MainWindow;
        InitializeComponent();
    }

}