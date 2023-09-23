using UnityEngine;

public interface ILeaderboard
{
    void Setup(IUIViewFactory uiViewFactory, IGameRound gameRound, IScore _score);
}

public class Leaderboard : MonoBehaviour, ILeaderboard
{
    [SerializeField] UILeaderboardView uILeaderboardView;

    IUIViewFactory _uiViewFactory;
    IScore _score;

    readonly ILoad<LeaderboardSave> _load = new Load<LeaderboardSave>();
    readonly ISave<LeaderboardSave> _save = new Save<LeaderboardSave>();

    public void Setup(IUIViewFactory uiViewFactory, IGameRound gameRound, IScore score)
    {
        _score = score;
        _uiViewFactory = uiViewFactory;
        gameRound.OnCompleteGame += Show;
    }

    void Show(char[] timer, int wave)
    {
        _uiViewFactory.CreateLeaderboardViewController(uILeaderboardView, timer, wave, _score.GetScore(), _load, _save);
    }
}
