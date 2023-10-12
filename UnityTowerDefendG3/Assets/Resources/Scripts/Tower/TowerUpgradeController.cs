using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    private Tower theTower;
    public UpgradeStage[] rangeUpgrades;
    public int currentRangeUpgrade;
    public bool hasRangeUpgrade = true;

    public UpgradeStage[] firerateUpgrades;
    public int currentFirerateUpgrade;
    public bool hasFirerateUpgrade = true;

    void Start()
    {
        theTower = GetComponent<Tower>();
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
}

[System.Serializable]
public class UpgradeStage
{
    public float amount;
    public int cost;
}
