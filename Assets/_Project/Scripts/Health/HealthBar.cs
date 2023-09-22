using UnityEngine;

public interface IHealthBar
{
    void Initialize();
    void SetCurrentHealth(float amount);
}

public class HealthBar : MonoBehaviour, IHealthBar
{
    IHealthBarPresentation healthBarPresentation;

    public void Initialize()
    {
        healthBarPresentation = GetComponent<IHealthBarPresentation>();
    }

    public void SetCurrentHealth(float amount)
    {
        healthBarPresentation.UpdateValues(amount);
    }
}
