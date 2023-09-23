using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] TowerPlacer _towerPlacer;
    [SerializeField] TowerUpgrader _towerUpgrader;
    [SerializeField] PlayerEconomy _playerEconomy;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] Navigation _navigation;
    [SerializeField] GameRound _gameRound;
    [SerializeField] Leaderboard _leaderboard;
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] Score _score;
    [SerializeField] Health _core;

    IUIViewFactory _uiViewFactory;

    void Awake()
    {
        _uiViewFactory = new UIViewFactory();

        _navigation.Setup();
        _gameRound.Setup(_uiViewFactory);
        _playerEconomy.Setup(_uiViewFactory);
        _score.Setup(_uiViewFactory);
        _towerPlacer.Setup(_uiViewFactory, _playerEconomy, _navigation, _playerInput);
        _leaderboard.Setup(_uiViewFactory, _gameRound, _score);
        _enemySpawner.Setup(_gameRound, _playerEconomy, _score);

        _core.Initialize();
        _core.OnDie += _gameRound.CompleteGame;

        _enemySpawner.StartGame();
    }
}