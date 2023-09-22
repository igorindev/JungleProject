public interface ITowerUpgrader
{
    void UpgradeTower(IUpgradable tower);
}

public class TowerUpgrader : ITowerUpgrader
{
    IPlayerEconomy _playerEconomy;

    public TowerUpgrader(IPlayerEconomy playerEconomy)
    {
        _playerEconomy = playerEconomy;
    }

    public void UpgradeTower(IUpgradable tower)
    {
        if (tower.CanUpgrade() && tower.CanBuyUpgrade(_playerEconomy.GetNumOfCoins()))
        {
            _playerEconomy.RemoveCoins(tower.GetUpgradeCost());
            tower.Upgrade();
        }
    }
}
