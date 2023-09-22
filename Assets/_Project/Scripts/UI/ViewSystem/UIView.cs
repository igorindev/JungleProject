using System;
using UnityEngine;

public interface IUIView
{
    void Destroy();
    void Show();
    void Hide();
}

public abstract class UIView : MonoBehaviour, IUIView
{
    [SerializeField] Canvas _viewCanvas;

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void Show()
    {
        _viewCanvas.enabled = true;
    }

    public virtual void Hide()
    {
        _viewCanvas.enabled = false;
    }
}
