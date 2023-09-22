using System;
using TMPro;
using UnityEngine;

public interface IUIScoreView
{
    void Setup();
    void UpdateView(int value);
}

public class UIScoreView : UIView, IUIScoreView
{
    [SerializeField] TextMeshProUGUI text;

    public void Setup()
    {
        
    }

    public void UpdateView(int value)
    {
        text.SetText("Score: " + value);
    }
}
