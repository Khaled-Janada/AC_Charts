namespace Infinity.Controls;

public class InfComboBox : ComboBox {

    static InfComboBox() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfComboBox), new FrameworkPropertyMetadata(typeof(InfComboBox)));
    }

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

        if (GetTemplateChild("Border") is not Border border) return;
        border.CornerRadius = new CornerRadius(6);
    }

}
