using Infinity.ComponentModel.ViewModels;

namespace Infinity.Messaging.Messages;

// Save Request Message
public record SaveRequestMessage(string ObjName, string ObjType, string Ext) : RequestMessage<string?>;

// Load Request Message
public record LoadRequestMessage(string ObjType, string Ext) : RequestMessage<string?>;

// Rename request Message
public record RenameRequestMessage : RequestViewModelMessage<RenameViewModel, string> {

    #region Properties
    public override RenameViewModel RequestViewModel { get; }
    #endregion

    public RenameRequestMessage(string name, string objectType) {
        RequestViewModel = new RenameViewModel(name, objectType);
    }

}

// Screen Shot Message
public record ScreenShotMessage(string Element, int Dpi = ScreenShotMessage._DefaultDpi) : Message {

    private const int _DefaultDpi = 300;

}
