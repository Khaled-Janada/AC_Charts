namespace Infinity.Controls;

public class InfTabControl : TabControl {

    #region Constructors
    static InfTabControl() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfTabControl), new FrameworkPropertyMetadata(typeof(InfTabControl)));
    }
    #endregion

}
