using UnityEngine;

[CreateAssetMenu]
public class TowerData : ScriptableObject
{
    [SerializeField] Tower _tower;
    [SerializeField] Sprite _towerIcon;
    [SerializeField] string _towerName;
    [SerializeField] int _towerCost;
    [SerializeField] int _towerUpgradeCost;
    [SerializeField] float _towerRadius = 10;
    [SerializeField] float _towerSpeed;
    [SerializeField] float _towerDamage;
    [SerializeField, Multiline] string _towerDescription;

    public Tower Tower { get => _tower; }
    public string TowerName { get => _towerName; }
    public int TowerCost { get => _towerCost; }
    public float TowerSpeed { get => _towerSpeed; }
    public float TowerDamage { get => _towerDamage; }
    public Sprite TowerSprite { get => _towerIcon; }
    public string TowerDescription { get => _towerDescription; }
    public int TowerUpgradeCost { get => _towerUpgradeCost; }
    public float TowerRadius { get => _towerRadius; }
}
