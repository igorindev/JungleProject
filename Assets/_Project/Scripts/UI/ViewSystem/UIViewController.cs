public interface IUIViewController
{
    void Present();
    void Destroy();
    void Hide();
}

public abstract class UIViewController<T> where T : IUIView
{
    readonly protected T _view;

    public UIViewController(T view)
    {
        _view = view;
    }

    public virtual void Present()
    {
        _view.Present();
    }

    public virtual void Hide()
    {
        _view.Hide();
    }

    public virtual void Destroy()
    {
        _view.Destroy();
    }
}
