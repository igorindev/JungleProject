using System;
using TMPro;
using UnityEngine;

public interface IUIEconomyView
{
    void Setup();
    void UpdateView(int value);
}

public class UIEconomyView : UIView, IUIEconomyView
{
    [SerializeField] TextMeshProUGUI text;

    public void Setup()
    {
        
    }

    public void UpdateView(int value)
    {
        text.SetText(value.ToString());
    }
}
