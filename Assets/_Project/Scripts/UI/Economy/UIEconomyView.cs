using TMPro;
using UnityEngine;

public interface IUIEconomyView
{
    void UpdateView(int value);
}

public class UIEconomyView : UIView, IUIEconomyView
{
    [SerializeField] TextMeshProUGUI text;


    public void UpdateView(int value)
    {
        text.SetText("$" + value);
    }
}
