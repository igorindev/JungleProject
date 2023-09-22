using System;

public class UIScoreViewController : UIViewController<UIScoreView>
{
    IScore _score;

    public UIScoreViewController(UIScoreView view, IScore score) : base(view)
    {
        _score = score;
        _score.OnAddScore += Update;
    }

    void Update(int value)
    {
        _view.UpdateView(value);
    }

    public override void Destroy()
    {
        _score.OnAddScore -= Update;
        base.Destroy();
    }
}
