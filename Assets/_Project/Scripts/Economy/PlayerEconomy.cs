using System;
using UnityEngine;
public interface IPlayerEconomy
{
    void AddCoins(int value);
    bool CanUseCoins(int cost);
    void RemoveCoins(int value);
    void Setup(IUIViewFactory uiViewFactory);

    Action<int> OnUpdate { get; set; }
}

public class PlayerEconomy : MonoBehaviour, IPlayerEconomy
{
    [SerializeField] int _coins;
    [SerializeField] UIEconomyView _economyView;

    public Action<int> OnUpdate { get; set; }

    public void Setup(IUIViewFactory uiViewFactory)
    {
        uiViewFactory.CreateEconomyViewController(_economyView, this);

        OnUpdate.Invoke(_coins);
    }

    public void AddCoins(int value)
    {
        SetCoins(value);
    }

    public void RemoveCoins(int value)
    {
        SetCoins(-value);
    }

    void SetCoins(int value)
    {
        _coins += value;
        OnUpdate.Invoke(_coins);
    }

    public bool CanUseCoins(int cost) => _coins >= cost;
}
