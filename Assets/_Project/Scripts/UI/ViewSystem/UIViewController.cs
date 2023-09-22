public interface IUIViewController
{
    void Present();
    void Destroy();
    void Hide();
    void Show();
}

public abstract class UIViewController<T> : IUIViewController where T : IUIView
{
    readonly protected T _view;

    public UIViewController(T view)
    {
        _view = view;
    }

    public virtual void Present()
    {
        Show();
    }

    public virtual void Hide()
    {
        _view.Hide();
    }

    public virtual void Destroy()
    {
        _view.Destroy();
    }

    public virtual void Show()
    {
        _view.Show();
    }
}
