using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public GameObject Panel_money;
    public GameObject Panel_Upgrade;
    public GameObject towerInfo;
    public RectTransform rectTransform;
    public TextMeshProUGUI upRangeText, upFirerateText;
    public Tower TowerLoaded { get; set; }

    private void Start()
    {
        Panel_money.SetActive(false);
        Panel_Upgrade.SetActive(false);
        towerInfo.transform.Find("Button - Close").GetComponent<Button>().onClick.AddListener(() => CloseTowerUpgradePanel());
        towerInfo.transform.Find("Button - Sell").GetComponent<Button>().onClick.AddListener(() => SellTowerUpgradePanel());
        towerInfo.transform.Find("Button - UpgradeRange").GetComponent<Button>().onClick.AddListener(() => UpgradeRange());
        towerInfo.transform.Find("Button - UpgradeFirerate").GetComponent<Button>().onClick.AddListener(() => UpgradeFirerate());
        towerInfo.transform.Find("Button - UpgradeDamage").GetComponent<Button>().onClick.AddListener(() => UpgradeDamage());
    }
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Kiểm tra nút chuột trái đã được nhấn
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Chọn một khoảng z để hiển thị TowerLoaded
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (TowerLoaded == null)
            {
                TowerLoaded = Instantiate(Money.instance.SelectedCard.towerItem.towerPrefab, worldPosition, Quaternion.identity);
            }
        }
    }*/
    private void OnMouseDown()
    {
        if (TowerLoaded == null)
        {

            Panel_money.SetActive(true);
            GetRectPositopn();
            if (Money.instance.SelectedCard != null)
            {

                if (Money.instance.SpendMoney(Money.instance.SelectedCard.towerItem.cost))
                {
                    TowerLoaded = Instantiate(Money.instance.SelectedCard.towerItem.towerPrefab, transform.position, Quaternion.identity, this.transform);
                    Money.instance.SelectedCard = null;
                    Panel_money.SetActive(false);
                }
            }
        }
        else
        {
            Panel_Upgrade.SetActive(true);
        }
    }


    public void GetRectPositopn() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up);

        // Set the RectTransform position to the center and middle of the screen position
        rectTransform.anchoredPosition = new Vector2(screenPosition.x - Screen.width / 2, screenPosition.y - Screen.height / 2);

    }

    public void CloseTowerUpgradePanel()
    {
        Panel_Upgrade.SetActive(false);
        FindObjectOfType<Tower>().range.SetActive(false);
    }

    public void SellTowerUpgradePanel()
    {
        if(TowerLoaded != null)
        {
            Money.instance.SpendMoney(-5);
            Destroy(TowerLoaded.gameObject);
            TowerLoaded = null;
            Panel_Upgrade.SetActive(false);
        }
        
    }

    public void UpgradeRange()
    {
        if (TowerLoaded != null)
        {
            TowerUpgradeController upgrader = TowerLoaded.upgrader;
            if (upgrader.hasRangeUpgrade)
            {
           //     upRangeText.text = "Upgrade range( " + upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost + " )";
                if (Money.instance.SpendMoney(upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost))
                {
                    upgrader.upgradeRange();
                }
            }
            else
            {
              //  upRangeText.text = "max";
            }
        }
    }

    public void UpgradeFirerate()
    {
        if (TowerLoaded != null)
        {
            TowerUpgradeController upgrader = TowerLoaded.upgrader;

            if (upgrader.hasFirerateUpgrade)
            {
             //   upFirerateText.text = "Upgrade firerate( " + upgrader.firerateUpgrades[upgrader.currentFirerateUpgrade].cost + " )";
                if (Money.instance.SpendMoney(upgrader.firerateUpgrades[upgrader.currentFirerateUpgrade].cost))
                {
                    upgrader.upgradeFirerate();
                }
            }
            else
            {
              //  upFirerateText.text = "max";
            }
        }
    }
    public void UpgradeDamage()
    {
        if (TowerLoaded != null)
        {
            TowerUpgradeController upgrader = TowerLoaded.upgrader;

            if (upgrader.hasDamageUpgrade)
            {
                if (Money.instance.SpendMoney(upgrader.damageUpgrades[upgrader.currentDamageUpgrade].cost))
                {
                    upgrader.upgradeDamage();
                }
            }
            else
            {
            }
        }
    }
}
