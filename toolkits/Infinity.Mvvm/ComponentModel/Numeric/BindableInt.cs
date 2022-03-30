namespace Infinity.ComponentModel.Numeric;

public class BindableInt : BindableNumberBase<int> {

    #region Constrcutors
    public BindableInt() : base(0, short.MaxValue) { }

    public BindableInt(Func<int> getter, Action<int> setter) : base(getter, setter, 0, short.MaxValue) { }
    #endregion

}
