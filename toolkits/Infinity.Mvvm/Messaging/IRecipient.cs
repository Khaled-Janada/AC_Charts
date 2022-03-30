namespace Infinity.Messaging; 

public interface IRecipient<in TMessage> {

    public void Receive(TMessage message);
}
