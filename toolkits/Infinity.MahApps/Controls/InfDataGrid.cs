using System.Windows.Data;

namespace Infinity.Controls; 

public class InfDataGrid : DataGrid {

    #region Row Number String Format
    public static readonly DependencyProperty RowNumberStringFormatProperty = DependencyProperty.Register(
        nameof(RowNumberStringFormat), typeof(string), typeof(InfDataGrid), new PropertyMetadata(default(string?)));

    public string? RowNumberStringFormat {
        get => (string?)GetValue(RowNumberStringFormatProperty);
        set => SetValue(RowNumberStringFormatProperty, value);
    }
    #endregion

    #region Row Number Converter
    public static readonly DependencyProperty RowNumberConverterProperty = DependencyProperty.Register(
        nameof(RowNumberConverter), typeof(IValueConverter), typeof(InfDataGrid),
        new PropertyMetadata(default(IValueConverter?)));

    public IValueConverter? RowNumberConverter {
        get => (IValueConverter?)GetValue(RowNumberConverterProperty);
        set => SetValue(RowNumberConverterProperty, value);
    }
    #endregion

    #region Constructors
    static InfDataGrid() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfDataGrid), new FrameworkPropertyMetadata(typeof(InfDataGrid)));
    }

    public InfDataGrid() {
        IsKeyboardFocusWithinChanged += OnIsKeyboardFocusWithinChanged;
        LoadingRow += OnLoadingRow;

        CurrentCellChanged += OnCurrentCellChanged;
    }
    #endregion

    #region Callbacks
    private void OnIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e) {
        if ((bool)e.NewValue == false) UnselectAllCells();
    }

    private static void OnLoadingRow(object? sender, DataGridRowEventArgs e) {
        e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }

    private void OnCurrentCellChanged(object? sender, EventArgs e) {
        CommitEdit();
    }
    #endregion

}
