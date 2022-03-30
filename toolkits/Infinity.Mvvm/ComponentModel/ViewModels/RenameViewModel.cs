namespace Infinity.ComponentModel.ViewModels;

public class RenameViewModel : DialogViewModel, IRequestViewModel<string> {

    #region Fields
    private string _name;
    #endregion

    #region Properties
    public string Name { get => _name; set => SetProperty(ref _name, value); }

    public string ObjectType { get; }
    public string Message => $"New {ObjectType} name:";

    public string Response { get; private set; }
    #endregion

    public RenameViewModel(string name, string objectType) {
        Response = _name = name;
        ObjectType = objectType;
    }

    protected override void FinishOk() {
        base.FinishOk();
        Response = Name;
    }

}
