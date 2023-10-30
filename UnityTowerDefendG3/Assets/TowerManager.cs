using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public List<Tower> TowerList = new List<Tower>();
    private int count = 0;
    public LayerMask whatIsPlacement, whatIsObstacle;
    public Transform indicator;
    public bool isPlacing;
    
    [Header("Reference")]
    public GameObject selectedTowerEffect;
	public GameObject Panel_money;
	public GameObject Panel_Upgrade;
	public GameObject towerInfo;
	public TextMeshProUGUI textWarning;

	[HideInInspector]
	private TowerItem towerItem;
	[HideInInspector]
	public Tower selectedTower;
	private void Awake()    
    {
        
        Instance = this;
     
    }
    void Start()
    {
		Panel_money.SetActive(true);
		towerInfo.transform.Find("Button - Close").GetComponent<Button>().onClick.AddListener(() => CloseTowerUpgradePanel());
		towerInfo.transform.Find("Button - Sell").GetComponent<Button>().onClick.AddListener(() => SellTowerUpgradePanel());
		towerInfo.transform.Find("Button - UpgradeRange").GetComponent<Button>().onClick.AddListener(() => UpgradeRange());
		towerInfo.transform.Find("Button - UpgradeFirerate").GetComponent<Button>().onClick.AddListener(() => UpgradeFirerate());
		towerInfo.transform.Find("Button - UpgradeDamage").GetComponent<Button>().onClick.AddListener(() => UpgradeDamage());
	}

    // Update is called once per frame
    void Update()
    {
		if (isPlacing)
        {
            indicator.position = GetGridPosition();
            RaycastHit2D hit;
            if (Physics2D.Raycast(indicator.position , Vector2.zero, 10f, whatIsObstacle))
            {
                indicator.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
            else
            {
                indicator.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;


                if (Input.GetMouseButtonDown(0))
                {
                    if (Money.instance.SpendMoney(towerItem.cost))
                    {
                        isPlacing = false;
                        Instantiate(towerItem.towerPrefab, indicator.position, towerItem.towerPrefab.transform.rotation);
                        indicator.gameObject.SetActive(false);
                       
					}
                }
            }
        }
    }

    public Vector2 GetGridPosition()
    {
        Vector2 location = Vector2.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            location = hit.point;
        }

        return location;
    }
    public void SelectedTower(TowerItem towerBtn)
    {

        towerItem = towerBtn;
        isPlacing = true;
        Destroy(indicator.gameObject);
        Tower placeTower = Instantiate(towerItem.towerPrefab);
        placeTower.enabled = false;
        placeTower.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        indicator = placeTower.transform;
        placeTower.getRange();
    }

    public void moveTowerSelectionEffect()
    {
        if (selectedTower!=null) {
            selectedTowerEffect.transform.position = selectedTower.transform.position;
			selectedTowerEffect.SetActive(true);
		}
    }

	public void SelectTarget(int val)
	{

		if (val == 1)
		{
			Debug.Log("cc1");
			selectedTower.target = Target.First;
			Debug.Log("cc");
		}
		else
		 if (val == 2)
		{
			selectedTower.target = Target.Last;
		}
		else
		 if (val == 3)
		{
			selectedTower.target = Target.StrongestEnememies;
		}
		else
		 if (val == 4)
		{
			selectedTower.target = Target.WeakestEnememies;
		}

	}


	public void meme()
	{
		Panel_money.SetActive(false);
		if (selectedTower == null)
		{
			//Panel_money.SetActive(true);
		}
		else
		{
			Panel_Upgrade.SetActive(true);
		}
	}

	public void CloseTowerUpgradePanel()
	{
		Panel_money.SetActive(true);
		Panel_Upgrade.SetActive(false);
		selectedTower.removeRange();

	}

	public void SellTowerUpgradePanel()
	{
			Money.instance.SpendMoney(-5);
			Destroy(selectedTower.gameObject);
			selectedTower = null;
			Panel_Upgrade.SetActive(false);
		Panel_money.SetActive(true);
		selectedTowerEffect.SetActive(false);
	}
	public void UpgradeRange()
	{
			TowerUpgradeController upgrader = selectedTower.upgrader;
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

	public void UpgradeFirerate()
	{
			TowerUpgradeController upgrader = selectedTower.upgrader;
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
	public void UpgradeDamage()
	{
		TowerUpgradeController upgrader = selectedTower.upgrader;
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
