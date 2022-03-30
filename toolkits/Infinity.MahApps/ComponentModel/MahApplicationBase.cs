using Infinity.Controls;
using Infinity.Messaging;
using Infinity.Messaging.Messages;

namespace Infinity.ComponentModel; 

public class MahApplicationBase : ApplicationBase, IRecipient<RenameRequestMessage> {

    public void Receive(RenameRequestMessage message) {
        new InfRenameDialog(message.RequestViewModel).ShowDialog();
        message.Reply();
    }

}
