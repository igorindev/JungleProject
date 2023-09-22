using System;
using UnityEngine;

public interface IScore
{
    void AddScore(int value);
    int GetScore();
    void Setup(IUIViewFactory uiViewFactory);
    Action<int> OnAddScore { get; set; }
}

public class Score : MonoBehaviour, IScore
{
    [SerializeField] UIScoreView uiScoreView;

    int _score;

    IUIViewFactory _uiViewFactory;

    public Action<int> OnAddScore { get; set; }

    public void Setup(IUIViewFactory uiViewFactory)
    {
        _uiViewFactory = uiViewFactory;
        _uiViewFactory.CreateScoreViewController(uiScoreView, this);
    }

    public void AddScore(int value)
    {
        _score += value;
        OnAddScore.Invoke(_score);
    }

    public int GetScore()
    {
        return _score;
    }
}
