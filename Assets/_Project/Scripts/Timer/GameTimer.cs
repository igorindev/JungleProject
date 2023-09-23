using System;
using System.Text;

public interface IGameTimer
{
    void Setup();
    void UpdateTimer(float deltaTime);
    float GetTime();
    char[] GetTimeInChar();
}

public class GameTimer : IGameTimer
{
    readonly char[] _array = new char[9];
    float _time;

    public void Setup()
    {

    }

    public void UpdateTimer(float deltaTime)
    {
        _time += deltaTime;
        SecondsToCharArray(_time, _array);
    }

    public float GetTime()
    {
        return _time;
    }

    public char[] GetTimeInChar()
    {
        return _array;
    }

    void SecondsToCharArray(float timeInSeconds, char[] array)
    {
        int minutes = (int)(timeInSeconds / 60f);
        array[0] = (char)(48 + minutes * 0.1f);
        array[1] = (char)(48 + (minutes % 10));
        array[2] = ':';

        int seconds = (int)(timeInSeconds - minutes * 60);
        array[3] = (char)(48 + seconds * 0.1f);
        array[4] = (char)(48 + seconds % 10);
        array[5] = '.';

        int milliseconds = (int)((timeInSeconds % 1) * 1000);
        array[6] = (char)(48 + milliseconds * 0.01f);
        array[7] = (char)(48 + (milliseconds % 100) * 0.1f);
        array[8] = (char)(48 + milliseconds % 10);
    }
}
