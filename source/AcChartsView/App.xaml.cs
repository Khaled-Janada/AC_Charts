namespace AcCharts;

public partial class App {

    public App() {
        var res = new ResourceDictionary { Source = new Uri(@"Resources\Styles\ChartElementStyle.xaml", UriKind.Relative) };
        Properties.Add("ChartElementStyle", res["ChartElementStyle"]);
    }

    private void App_OnStartup(object sender, StartupEventArgs e) {
        MainWindow = new MainWindow { DataContext = new MainViewModel() };
        MainWindow.Show();
    }

}