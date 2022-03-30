using System.Windows.Documents;

namespace AcCharts.Drawing; 

public class AxisTitleViewModel {

    #region Public Properties
    public bool UseFormattedText { get; }
    public string? UnformattedText { get; }
    public IList<Run>? FormattedText { get; }
    #endregion

    #region Constructors
    public AxisTitleViewModel(string unformattedText) {
        UseFormattedText = false;
        UnformattedText = unformattedText;
    }
    
    public AxisTitleViewModel(IEnumerable<Run> formattedText) {
        UseFormattedText = true;
        FormattedText = formattedText.ToList();
    }
    #endregion
}
