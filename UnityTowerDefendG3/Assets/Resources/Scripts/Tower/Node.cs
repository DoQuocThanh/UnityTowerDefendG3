using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Node : MonoBehaviour
{
    public GameObject Panel_money;
    public GameObject Panel_Upgrade;
    public GameObject towerInfo;

    public static Node oldNode;
    private bool isClick = false;
    //  public RectTransform rectTransform;
    public TextMeshProUGUI textWarning;
    public Tower TowerLoaded { get; set; }
    private bool checkRange = true;
    private GameObject selectedPoint;

    private void Start()
    {
        Panel_money.SetActive(false);
        Panel_Upgrade.SetActive(false);
        selectedPoint = GameObject.FindGameObjectsWithTag("SelectedPoint").FirstOrDefault();
        towerInfo.transform.Find("Button - Close").GetComponent<Button>().onClick.AddListener(() => CloseTowerUpgradePanel());
        towerInfo.transform.Find("Button - Sell").GetComponent<Button>().onClick.AddListener(() => SellTowerUpgradePanel());
        towerInfo.transform.Find("Button - UpgradeRange").GetComponent<Button>().onClick.AddListener(() => UpgradeRange());
        towerInfo.transform.Find("Button - UpgradeFirerate").GetComponent<Button>().onClick.AddListener(() => UpgradeFirerate());
        towerInfo.transform.Find("Button - UpgradeDamage").GetComponent<Button>().onClick.AddListener(() => UpgradeDamage());
    }
    private void Update()
    {
        if (TowerLoaded != null && checkRange)
        {
            TowerLoaded.getRange();
        }

        if (oldNode == this)
        {
            isClick = true;

        }
        else
        {
            isClick = false;
        }
        if (TowerLoaded == null)
        {
            setTower();
            Debug.Log("Wrong");
        }
    }

    public void setSelectedPoint() {
        selectedPoint.SetActive(true);
        if (selectedPoint != null) {
            selectedPoint.transform.position = this.transform.position + new Vector3(0,0.5f,0);
        }
    }

    public void setTower() {
        if (Money.instance.SelectedCard != null && isClick )
        {
            if (Money.instance.SpendMoney(Money.instance.SelectedCard.towerItem.cost))
            {
               
                TowerLoaded = Instantiate(Money.instance.SelectedCard.towerItem.towerPrefab, transform.position, Quaternion.identity, this.transform);
                Money.instance.SelectedCard = null;
                Panel_money.SetActive(false);
                selectedPoint.SetActive(false);
            }
            Money.instance.SelectedCard = null;
        }
    }

    public void SelectTarget(int val)
    {

        if (val == 1)
        {
            FindObjectOfType<Tower>().target = Target.First;
        }
        else
         if (val == 2)
        {
            FindObjectOfType<Tower>().target = Target.Last;
        }
        else
         if (val == 3)
        {
            FindObjectOfType<Tower>().target = Target.StrongestEnememies;
        }
        else
         if (val == 4)
        {
            FindObjectOfType<Tower>().target = Target.WeakestEnememies;
        }

    }


    public void meme()
    {
        if (oldNode == null || oldNode != this)
        {
            oldNode = this;
            isClick = true;
        }
        setSelectedPoint();
        Panel_money.SetActive(false);
        Panel_Upgrade.SetActive(false);
        if (TowerLoaded == null)
        {
            
            Panel_money.SetActive(true);
            GetRectPositopn();
            //if (Money.instance.SelectedCard != null)
            //{
            //    if (Money.instance.SpendMoney(Money.instance.SelectedCard.towerItem.cost))
            //    {
            //        TowerLoaded = Instantiate(Money.instance.SelectedCard.towerItem.towerPrefab, transform.position, Quaternion.identity, this.transform);
            //        Money.instance.SelectedCard = null;
            //        Panel_money.SetActive(false);
            //        selectedPoint.SetActive(false);

            //    }
            //}
        }
        else
        {
            Panel_Upgrade.SetActive(true);
            checkRange = true;
        }
    }

    public void GetRectPositopn()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up);

        // Set the RectTransform position to the center and middle of the screen position
        //     rectTransform.anchoredPosition = new Vector2(screenPosition.x - Screen.width / 2, screenPosition.y - Screen.height / 2);

    }

    public void CloseTowerUpgradePanel()
    {
        if (TowerLoaded != null && isClick)
        {
            Panel_money.SetActive(false);
            Panel_Upgrade.SetActive(false);
            TowerLoaded.removeRange();
            checkRange = false;
           
        }
    }

    public void SellTowerUpgradePanel()
    {
        if (TowerLoaded != null && isClick)
        {
            Money.instance.SpendMoney(-5);
            Destroy(TowerLoaded.gameObject);
            TowerLoaded = null;
            Panel_Upgrade.SetActive(false);
        }

    }
    public void UpgradeRange()
    {
        if (TowerLoaded != null &&isClick)
        {
            TowerUpgradeController upgrader = TowerLoaded.upgrader;
            if (upgrader.hasRangeUpgrade)
            {
                //     textWarning.text = "Upgrade range( " + upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost + " )";
                if (Money.instance.SpendMoney(upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost))
                {
                    upgrader.upgradeRange();
                }
            }
            else
            {
                showMessage("Max Upgrade RANGE");
            }
        }
    }

    public void UpgradeFirerate()
    {
        if (TowerLoaded != null && isClick)
        {
            TowerUpgradeController upgrader = TowerLoaded.upgrader;
            Debug.Log("abcd");
            if (upgrader.hasFirerateUpgrade)
            {
                //   textWarning.text = "Upgrade firerate( " + upgrader.firerateUpgrades[upgrader.currentFirerateUpgrade].cost + " )";
                if (Money.instance.SpendMoney(upgrader.firerateUpgrades[upgrader.currentFirerateUpgrade].cost))
                {
                    upgrader.upgradeFirerate();
           
                }
            }
            else
            {
               showMessage("Max Upgrade FIRERATE");
            }
        }
    }
    public void UpgradeDamage()
    {
        if (TowerLoaded != null && isClick)
        {
            TowerUpgradeController upgrader = TowerLoaded.upgrader;
            Debug.Log("abcd");
            if (upgrader.hasDamageUpgrade)
            {
                var money = Money.instance.SpendMoney(upgrader.damageUpgrades[upgrader.currentDamageUpgrade].cost);
                Debug.Log(money);
                if (money)
                {
                    upgrader.upgradeDamage();
                }
            }
            else
            {
                showMessage("Max Upgrade Damage");
            }
        }
    }
    private void ShowWarningAndDelay()
    {
        textWarning.gameObject.SetActive(false);
    }

    private void showMessage(string message)
    {
        textWarning.gameObject.SetActive(true);
        textWarning.SetText(message);
        Invoke("ShowWarningAndDelay", 2);
        
    }
}
