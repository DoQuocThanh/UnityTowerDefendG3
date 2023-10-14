using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    private Tower theTower;
    private Bullet bullet;
    public UpgradeStage[] rangeUpgrades;
    public int currentRangeUpgrade;
    public bool hasRangeUpgrade = true;

    public UpgradeStage[] firerateUpgrades;
    public int currentFirerateUpgrade;
    public bool hasFirerateUpgrade = true;

    public UpgradeStage[] damageUpgrades;
    public int currentDamageUpgrade;
    public bool hasDamageUpgrade = true;

    void Start()
    {
        theTower = GetComponent<Tower>();
        bullet = GetComponent<Bullet>();
    }

    public void upgradeRange()
    {
        theTower.circleCollider.radius = rangeUpgrades[currentRangeUpgrade].amount;
        currentRangeUpgrade++;
        if (currentRangeUpgrade >= rangeUpgrades.Length)
        {
            hasRangeUpgrade = false;
        }
    }

    public void upgradeFirerate()
    {
        theTower.firerate = firerateUpgrades[currentFirerateUpgrade].amount;
        currentFirerateUpgrade++;
        if (currentFirerateUpgrade >= firerateUpgrades.Length)
        {
            hasFirerateUpgrade = false;
        }
    }

    public void upgradeDamage()
    {
        if (bullet !=null)
        {
            bullet.bulletDamage = damageUpgrades[currentDamageUpgrade].amount;
            currentDamageUpgrade++;
            if (currentDamageUpgrade >= damageUpgrades.Length)
            {
                hasDamageUpgrade = false;
            }
        }
    }
}

[System.Serializable]
public class UpgradeStage
{
    public float amount;
    public int cost;
}
