using Collection;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IUIViewFactory
{
    void CreateEconomyViewController(UIEconomyView viewPrefab, IPlayerEconomy playerEconomy);
    void CreateGameRoundViewController(UIGameRoundView viewPrefab, IGameRound gameRound);
    void CreateLeaderboardViewController(UILeaderboardView viewPrefab, float timer, int wave, ILoad<LeaderboardSave> load, ISave<LeaderboardSave> save);
    void CreateTowerViewController(UITowerView viewPrefab, int size, Func<int, TowerData> getFromCollection, Action<TowerData, Action> onSelectTower);
}

public class UIViewFactory : IUIViewFactory
{
    T CreateView<T>(T view) where T : UIView
    {
        return Object.Instantiate(view);
    }

    public void CreateEconomyViewController(UIEconomyView viewPrefab, IPlayerEconomy playerEconomy)
    {
        UIEconomyView viewInstance = CreateView(viewPrefab);
        UIEconomyViewController viewController = new UIEconomyViewController(viewInstance, playerEconomy);
        viewController.Present();
    }

    public void CreateGameRoundViewController(UIGameRoundView viewPrefab, IGameRound gameRound)
    {
        UIGameRoundView viewInstance = CreateView(viewPrefab);
        UIGameRoundViewController viewController = new UIGameRoundViewController(viewInstance, gameRound);
        viewController.Present();
    }
    
    public void CreateLeaderboardViewController(UILeaderboardView viewPrefab, float timer, int wave, ILoad<LeaderboardSave> load, ISave<LeaderboardSave> save)
    {
        UILeaderboardView view = CreateView(viewPrefab);
        UILeaderboardViewController viewController = new UILeaderboardViewController(view, timer, wave, load, save);
        viewController.Present();
    }

    public void CreateTowerViewController(UITowerView viewPrefab, int size, Func<int, TowerData> getFromCollection, Action<TowerData, Action> onSelectTower)
    {
        UITowerView view = CreateView(viewPrefab);
        UITowerViewController viewController = new UITowerViewController(view, size, getFromCollection, onSelectTower);
        viewController.Present();
    }
}
