using System;

public interface IGameTimer
{
    void Setup();
    void UpdateTimer(float deltaTime);
    float GetTime();
}

public class GameTimer : IGameTimer
{
    float _time;

    public void Setup()
    {
        
    }

    public void UpdateTimer(float deltaTime)
    {
        _time += deltaTime;
    }

    public float GetTime()
    {
        return _time;
    }
}
