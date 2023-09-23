using System;
using UnityEngine;

public interface IGameRound
{
    Action<char[], int> OnCompleteGame { get; set; }
    Action<char[], int> OnUpdateRoundTimer { get; set; }

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

    public Action<char[], int> OnCompleteGame { get; set; }

    public Action<char[], int> OnUpdateRoundTimer { get; set; }

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
            OnUpdateRoundTimer?.Invoke(_gameTimer.GetTimeInChar(), _round);
    }

    public int NewRound()
    {
        _round++;
        return maxEnemyBaseRound * _round;
    }

    public void CompleteGame()
    {
        char[] time = _gameTimer.GetTimeInChar();
        Time.timeScale = 0;
        OnUpdateRoundTimer?.Invoke(time, _round);
        OnCompleteGame?.Invoke(time, _round);
    }
}