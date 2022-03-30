namespace Infinity.ComponentModel.Numeric;

public class BindableDouble : BindableNumberBase<double> {

    #region Fields
    private double _step;
    private int _precision;
    #endregion

    #region Properties
    public double Step { get => _step; set => SetProperty(ref _step, value); }
    public int Precision { get => _precision; set => SetProperty(ref _precision, value); }
    #endregion

    #region Constrcutors
    public BindableDouble() : base(double.NegativeInfinity, double.PositiveInfinity) { }

    public BindableDouble(Func<double> getter, Action<double> setter) :
        base(getter, setter, double.NegativeInfinity, double.PositiveInfinity) { }
    #endregion

}
