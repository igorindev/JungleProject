using System;
using TMPro;
using UnityEngine;

public interface IUIGameRoundView
{
    void Setup(IGameRound gameRound);
}

public class UIGameRoundView : UIView, IUIGameRoundView
{
    [SerializeField] TextMeshProUGUI _currentRound;
    [SerializeField] TextMeshProUGUI _timer;

    public void Setup(IGameRound gameRound)
    {
        gameRound.OnUpdateRoundTimer += UpdateRoundTimer;
    }

    void UpdateRoundTimer(float timer, int round)
    {
        _currentRound.text = "Wave: " + round.ToString();
        _timer.text = timer.ToString("00:00.000");
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
