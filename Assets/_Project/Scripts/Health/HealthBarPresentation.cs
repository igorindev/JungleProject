using UnityEngine;

public interface IHealthBarPresentation
{
    void UpdateValues(float amount);
}

public class HealthBarPresentation : MonoBehaviour, IHealthBarPresentation
{
    public void UpdateValues(float amount)
    {
        
    }
}
