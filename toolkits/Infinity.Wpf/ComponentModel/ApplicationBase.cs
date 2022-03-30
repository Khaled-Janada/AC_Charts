using System.Reflection;
using Infinity.Attributes;
using Infinity.Messaging;
using Infinity.Messaging.Messages;
using Infinity.Reflection;
using Infinity.Services;

namespace Infinity.ComponentModel;

public abstract class ApplicationBase : Application,
    IRecipient<ScreenShotMessage>, IRecipient<SaveRequestMessage>, IRecipient<LoadRequestMessage> {

    #region Properties
    protected Messenger Messenger { get; }
    protected Dictionary<Type, Type> Dialogs { get; } = new();
    #endregion

    #region Constructors
    protected ApplicationBase() : this(Messenger.Default) { }

    protected ApplicationBase(Messenger messenger) {
        Messenger = messenger;
        Messenger.RegisterAll(this);

        RegisterDialogMessages();
    }
    #endregion

    #region Private Methods
    private void RegisterDialogMessages() {
        var dialogMessageHandler = ReceiveDialogMessage;

        foreach (var type in GetType().Assembly.GetTypes()) {
            if (type.GetCustomAttribute(typeof(DialogAttribute)) is not DialogAttribute dialogAttribute) continue;

            if (!type.IsClassOrSubclass(typeof(Window)))
                throw new AttributeException(dialogAttribute, "can only be applied on a 'Window' class or subclass");

            var msgType = dialogAttribute.ViewModelMessageType;
            Dialogs.Add(msgType, type);

            Messenger.Register(msgType, this, dialogMessageHandler);
        }
    }
    #endregion

    #region Messages
    public void Receive(ScreenShotMessage message) {
        if (MainWindow != null) ScreenShots.TakeScreenShot(MainWindow, message.Element, message.Dpi);
    }

    public void Receive(SaveRequestMessage message) {
        (string? objName, string? objType, string? ext) = message;
        
        var dlg = new SaveFileDialog {
            FileName = objName, DefaultExt = $".{ext}", Filter = $"{objType} (*.{ext})|*.{ext}", Title = $"Save {objType}"
        };
        message.Reply(dlg.ShowDialog(MainWindow) == true ? dlg.FileName : null);
    }

    public void Receive(LoadRequestMessage message) {
        (string? objType, string? ext) = message;
        
        var dlg = new OpenFileDialog { DefaultExt = $".{ext}", Filter = $"{objType} (*.{ext})|*.{ext}", Title = $"Open {objType}" };
        message.Reply(dlg.ShowDialog(MainWindow) == true ? dlg.FileName : null);
    }

    private void ReceiveDialogMessage(IViewModelMessage message) {
        if (Activator.CreateInstance(Dialogs[message.GetType()]) is not Window window) return;

        window.DataContext = message.ViewModel;
        window.Owner = MainWindow;
        window.ShowDialog();

        if (message is IRequestViewModelMessage requestViewModelMessage) requestViewModelMessage.Reply();
    }
    #endregion

}
