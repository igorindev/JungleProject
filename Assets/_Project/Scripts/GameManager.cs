using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TowerPlacer _towerPlacer;
    [SerializeField] PlayerEconomy _playerEconomy;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] Navigation _navigation;
    [SerializeField] GameRound _gameRound;
    [SerializeField] Leaderboard _leaderboard;
    [SerializeField] Health _core;

    IUIViewFactory _uiViewFactory;

    void Awake()
    {
        _uiViewFactory = new UIViewFactory();

        _core.Initialize();
        _core.OnDie += _gameRound.CompleteGame;

        _navigation.Setup();
        _gameRound.Setup(_uiViewFactory);
        _playerEconomy.Setup(_uiViewFactory);
        _towerPlacer.Setup(_uiViewFactory, _playerEconomy, _navigation);
        _leaderboard.Setup(_uiViewFactory, _gameRound);
        _enemySpawner.Setup(_gameRound);

        _enemySpawner.StartGame();
    }
}