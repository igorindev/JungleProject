using System;

public class UIGameRoundViewController : UIViewController<UIGameRoundView>
{
    public UIGameRoundViewController(UIGameRoundView view, IGameRound gameRound) : base(view)
    {
        _view.Setup(gameRound);
    }

    public override void Present()
    {
        base.Present();
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
