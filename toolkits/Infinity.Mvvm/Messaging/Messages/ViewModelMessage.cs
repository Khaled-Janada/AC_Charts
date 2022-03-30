using Infinity.ComponentModel.ViewModels;

namespace Infinity.Messaging.Messages;

// IViewModelMessage Interface
public interface IViewModelMessage {

    public ViewModelBase ViewModel { get; }

}

// IRequestViewModelMessage Inter face
public interface IRequestViewModelMessage {

    public void Reply();

}

// RequestViewModelMessage Abstract Class
public abstract record RequestViewModelMessage<TViewModel, T> : RequestMessage<T>, IViewModelMessage, IRequestViewModelMessage
    where TViewModel : ViewModelBase, IRequestViewModel<T> {

    public ViewModelBase ViewModel => RequestViewModel;
    public abstract TViewModel RequestViewModel { get; }
    
    public void Reply() {
        Reply(RequestViewModel.Response);
    }
}
