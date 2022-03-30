using System.Windows.Input;
using Infinity.Commands;

namespace Infinity.ComponentModel.ViewModels;

public abstract class DialogViewModel : ViewModelBase {

    #region Fields
    private bool? _dialogresult;
    #endregion

    #region Public Properties
    public bool? DialogResult { get => _dialogresult; set => SetDialogResult(value); }
    public ICommand FinishCommand { get; }
    #endregion

    protected DialogViewModel() {
        FinishCommand = new Command(() => SetDialogResult(true), CanFinishExecute);
    }

    #region Private
    private void SetDialogResult(bool? result) {
        if (result == true)
            FinishOk();
        else
            FinishCancel();

        SetProperty(ref _dialogresult, result, nameof(DialogResult));
        Dispose();
    }
    #endregion

    #region Virtual
    protected virtual void FinishOk() { }
    protected virtual void FinishCancel() { }

    protected virtual bool CanFinishExecute() => true;
    #endregion

}
