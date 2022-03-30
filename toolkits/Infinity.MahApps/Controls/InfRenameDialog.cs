using System.Windows.Data;
using Infinity.ComponentModel.ViewModels;

namespace Infinity.Controls; 

public class InfRenameDialog : InfWindow {

    #region Constructors
    private InfRenameDialog() {
        ShowInTaskbar = false;
        Width = 800;
        Height = 450;
        SizeToContent = SizeToContent.WidthAndHeight;
        Owner = Application.Current.MainWindow;
    }

    public InfRenameDialog(RenameViewModel renameViewModel) : this() {
        Title = $"Rename {renameViewModel.ObjectType}";

        DataContext = renameViewModel;
        SetBinding(DialogResultProperty, new Binding(nameof(DialogResult)) { Source = DataContext });

        ContentControl contentControl = new();
        contentControl.SetBinding(ContentProperty, new Binding { Source = renameViewModel });
        Content = contentControl;
    }
    #endregion

}
