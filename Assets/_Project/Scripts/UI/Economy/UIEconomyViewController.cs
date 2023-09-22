public class UIEconomyViewController : UIViewController<UIEconomyView>
{
    readonly IPlayerEconomy _economy;

    public UIEconomyViewController(UIEconomyView view, IPlayerEconomy economy) : base(view)
    { 
        _economy = economy;
    }

    public override void Present()
    {
        _economy.OnUpdate += Update;
        _view.Setup();

        base.Present();
    }

    public override void Destroy()
    {
        _economy.OnUpdate -= Update;
        base.Destroy();
    }

    void Update(int value)
    {
        _view.UpdateView(value);
    }
}
