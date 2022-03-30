namespace Infinity.ComponentModel.ViewModels; 

public interface IRequestViewModel<out T> {

    public T Response { get; }

}
