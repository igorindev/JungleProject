using UnityEngine;

public interface ILeaderboard
{
    void Setup(IUIViewFactory uiViewFactory, IGameRound gameRound);
}

public class Leaderboard : MonoBehaviour, ILeaderboard
{
    [SerializeField] UILeaderboardView uILeaderboardView;

    IUIViewFactory _uiViewFactory;

    readonly ILoad<LeaderboardSave> _load = new Load<LeaderboardSave>();
    readonly ISave<LeaderboardSave> _save = new Save<LeaderboardSave>();

    public void Setup(IUIViewFactory uiViewFactory, IGameRound gameRound)
    {
        _uiViewFactory = uiViewFactory;
        gameRound.OnCompleteGame += Show;
    }

    void Show(float timer, int wave)
    {
        _uiViewFactory.CreateLeaderboardViewController(uILeaderboardView, timer, wave, _load, _save);
    }
}
