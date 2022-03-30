namespace AcCharts.Statistics;

public class DistributionViewModel : ViewModelBase {

    #region Fields
    private DistributionType _type = DistributionType.Exponential;
    private double _scale = 1;
    private double _shape = 1;
    private double _location;
    #endregion

    #region Properties
    public DistributionType Type {
        get => _type;
        set {
            SetProperty(ref _type, value);
            OnPropertyChanged(nameof(HasShape));
            OnPropertyChanged(nameof(Shape));
        }
    }

    public double Shape {
        get => Type.IsShapeInteger() ? (int)_shape : Type.HasShape() ? _shape : double.NaN;
        set => SetProperty(ref _shape, value);
    }

    public double Scale { get => _scale; set => SetProperty(ref _scale, value); }
    public double Location { get => _location; set => SetProperty(ref _location, value); }

    public bool HasShape => Type.HasShape();
    #endregion

    #region Constructors
    public DistributionViewModel(IDistribution distribution) {
        (Type, Scale, Shape, Location) = distribution;
    }

    public DistributionViewModel() { }
    #endregion

    #region Public Methods
    public IDistribution ToDistribution() => IDistribution.CreateDistribution(Type, Scale, Shape, Location);
    #endregion

}