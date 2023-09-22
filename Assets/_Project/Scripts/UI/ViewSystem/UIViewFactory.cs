using System;
using Object = UnityEngine.Object;

public interface IUIViewFactory
{
    IUIViewController CreateEconomyViewController(UIEconomyView viewPrefab, IPlayerEconomy playerEconomy);
    IUIViewController CreateGameRoundViewController(UIGameRoundView viewPrefab, IGameRound gameRound);
    IUIViewController CreateLeaderboardViewController(UILeaderboardView viewPrefab, float timer, int wave, int score, ILoad<LeaderboardSave> load, ISave<LeaderboardSave> save);
    IUIViewController CreateScoreViewController(UIScoreView viewPrefab, IScore score);
    IUIViewController CreateTowerUpgradeViewController(UITowerUpgradeView viewPrefab, IUIViewController towerViewController, Tower interactedTower, Action<TowerData> onSelectTower);
    IUIViewController CreateTowerViewController(UITowerView viewPrefab, int size, Func<int, TowerData> getFromCollection, Action<TowerData, Action> onSelectTower);
}

public class UIViewFactory : IUIViewFactory
{
    T CreateView<T>(T view) where T : UIView
    {
        return Object.Instantiate(view);
    }

    public IUIViewController CreateEconomyViewController(UIEconomyView viewPrefab, IPlayerEconomy playerEconomy)
    {
        UIEconomyView viewInstance = CreateView(viewPrefab);
        UIEconomyViewController viewController = new UIEconomyViewController(viewInstance, playerEconomy);
        viewController.Present();

        return viewController;
    }

    public IUIViewController CreateGameRoundViewController(UIGameRoundView viewPrefab, IGameRound gameRound)
    {
        UIGameRoundView viewInstance = CreateView(viewPrefab);
        UIGameRoundViewController viewController = new UIGameRoundViewController(viewInstance, gameRound);
        viewController.Present();

        return viewController;
    }
    
    public IUIViewController CreateLeaderboardViewController(UILeaderboardView viewPrefab, float timer, int wave, int score, ILoad<LeaderboardSave> load, ISave<LeaderboardSave> save)
    {
        UILeaderboardView view = CreateView(viewPrefab);
        UILeaderboardViewController viewController = new UILeaderboardViewController(view, timer, wave, score, load, save);
        viewController.Present();

        return viewController;
    }

    public IUIViewController CreateTowerViewController(UITowerView viewPrefab, int numOfTowers, Func<int, TowerData> getFromCollection, Action<TowerData, Action> onSelectTower)
    {
        UITowerView view = CreateView(viewPrefab);
        UITowerViewController viewController = new UITowerViewController(view, numOfTowers, getFromCollection, onSelectTower);
        viewController.Present();

        return viewController;
    }

    public IUIViewController CreateScoreViewController(UIScoreView viewPrefab, IScore score)
    {
        UIScoreView view = CreateView(viewPrefab);
        UIScoreViewController viewController = new UIScoreViewController(view, score);
        viewController.Present();

        return viewController;
    }

    public IUIViewController CreateTowerUpgradeViewController(UITowerUpgradeView viewPrefab, IUIViewController towerViewController, Tower interactedTower, Action<TowerData> onSelectTower)
    {
        UITowerUpgradeView view = CreateView(viewPrefab);
        UITowerUpgradeViewController viewController = new UITowerUpgradeViewController(view, towerViewController, interactedTower, onSelectTower);
        viewController.Present();

        return viewController;
    }
}
