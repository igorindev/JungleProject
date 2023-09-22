using System;
using UnityEngine;

public interface IGameRound
{
    Action<float, int> OnCompleteGame { get; set; }
    Action<float, int> OnUpdateRoundTimer { get; set; }

    void CompleteGame();
    int NewRound();
    void Setup(IUIViewFactory uiViewFactory);
}

public class GameRound : MonoBehaviour, IGameRound
{
    [SerializeField] UIGameRoundView _view;
    [SerializeField] int maxEnemyBaseRound = 10;

    int _round = 0;

    IGameTimer _gameTimer;

    public Action<float, int> OnCompleteGame { get; set; }

    public Action<float, int> OnUpdateRoundTimer { get; set; }

    public void Setup(IUIViewFactory uiViewFactory)
    {
        Time.timeScale = 1;
        _gameTimer = new GameTimer();

        uiViewFactory.CreateGameRoundViewController(_view, this);
    }

    void Update()
    {
        _gameTimer.UpdateTimer(Time.deltaTime);

        if (Time.frameCount % 5 == 0)
            OnUpdateRoundTimer?.Invoke(_gameTimer.GetTime(), _round);
    }

    public int NewRound()
    {
        _round++;
        return maxEnemyBaseRound * _round;
    }

    public void CompleteGame()
    {
        float time = _gameTimer.GetTime();
        Time.timeScale = 0;
        OnUpdateRoundTimer?.Invoke(time, _round);
        OnCompleteGame?.Invoke(time, _round);
    }
}