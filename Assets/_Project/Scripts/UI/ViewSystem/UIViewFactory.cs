using System;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IUIViewFactory
{
    IUIViewController CreateEconomyViewController(UIEconomyView viewPrefab, IPlayerEconomy playerEconomy);
    IUIViewController CreateGameRoundViewController(UIGameRoundView viewPrefab, IGameRound gameRound);
    IUIViewController CreateLeaderboardViewController(UILeaderboardView viewPrefab, char[] timer, int wave, int score, ILoad<LeaderboardSave> load, ISave<LeaderboardSave> save);
    IUIViewController CreateScoreViewController(UIScoreView viewPrefab, IScore score);
    IUIViewController CreateTowerUpgradeViewController(UITowerUpgradeView viewPrefab, ITowerUpgradable interactedTower, Func<ITowerUpgradable, bool> canUpgrade, Action<ITowerUpgradable> upgradeTower);
    IUIViewController CreateTowerViewController(UITowerView viewPrefab, int size, Func<int, ITowerData> getFromCollection, Action<ITowerData> onSelectTower);
}

public class ViewInstantiator : IInstantiator<UIView>
{
    public UIView Spawn(UIView gameObject, Vector3 _, Quaternion __) => Object.Instantiate(gameObject);
}

public class UIViewFactory : IUIViewFactory
{
    readonly IInstantiator<UIView> instantiator;

    public UIViewFactory()
    {
        instantiator = new ViewInstantiator();
    }

    T CreateView<T>(T view) where T : UIView
    {
        return (T)instantiator.Spawn(view, Vector3.zero, Quaternion.identity);
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

    public IUIViewController CreateLeaderboardViewController(UILeaderboardView viewPrefab, char[] timer, int wave, int score, ILoad<LeaderboardSave> load, ISave<LeaderboardSave> save)
    {
        UILeaderboardView view = CreateView(viewPrefab);
        UILeaderboardViewController viewController = new UILeaderboardViewController(view, timer, wave, score, load, save);
        viewController.Present();

        return viewController;
    }

    public IUIViewController CreateTowerViewController(UITowerView viewPrefab, int numOfTowers, Func<int, ITowerData> getFromCollection, Action<ITowerData> onSelectTower)
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

    public IUIViewController CreateTowerUpgradeViewController(UITowerUpgradeView viewPrefab, ITowerUpgradable interactedTower, Func<ITowerUpgradable, bool> canUpgrade, Action<ITowerUpgradable> upgradeTower)
    {
        UITowerUpgradeView view = CreateView(viewPrefab);
        UITowerUpgradeViewController viewController = new UITowerUpgradeViewController(view, interactedTower, canUpgrade, upgradeTower);
        viewController.Present();

        return viewController;
    }
}