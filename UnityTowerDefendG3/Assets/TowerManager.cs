using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
	public static TowerManager Instance;

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
	public TextMeshProUGUI textRange;
	public TextMeshProUGUI textFirerate;
	public TextMeshProUGUI textDamage;

	[HideInInspector]
	private TowerItem towerItem;
	[HideInInspector]
	public Tower selectedTower;
	[HideInInspector]
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

			if (Physics2D.Raycast(indicator.position, Vector2.zero, 10f, whatIsObstacle))
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
		if (count >= 5)
		{
			showMessage("limited tower");
			return;
		}
		towerItem = towerBtn;
		isPlacing = true;
		Destroy(indicator.gameObject);
		Tower placeTower = Instantiate(towerItem.towerPrefab);
		placeTower.enabled = false;
		placeTower.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
		count++;
		indicator = placeTower.transform;
		placeTower.getRange();
	}

	public void moveTowerSelectionEffect()
	{
		if (selectedTower != null)
		{
			selectedTowerEffect.transform.position = selectedTower.transform.position;
			selectedTowerEffect.SetActive(true);
		}
	}

	public void SelectTarget(int val)
	{

		if (val == 1)
		{
			selectedTower.target = Target.First;
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

		Panel_Upgrade.SetActive(true);
		SetupPanel();
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
		count--;
		Destroy(selectedTower.gameObject);
		selectedTower = null;
		Panel_Upgrade.SetActive(false);
		Panel_money.SetActive(true);
		selectedTowerEffect.SetActive(false);
	}
	public void SetupPanel()
	{
		if (selectedTower.upgrader.hasRangeUpgrade)
		{
			TowerUpgradeController upgrader = selectedTower.upgrader;

			textRange.text = "Upgrade range( " + upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost + " )";

		}
		else
		{
			textRange.text = "MAX";
			showMessage("Max Upgrade RANGE");
		}

		if (selectedTower.upgrader.hasFirerateUpgrade)
		{
			TowerUpgradeController upgrader = selectedTower.upgrader;

			textFirerate.text = "Upgrade firerate( " + upgrader.firerateUpgrades[upgrader.currentFirerateUpgrade].cost + " )";

		}
		else
		{
			textFirerate.text = "MAX";
			showMessage("Max Upgrade FIRERATE");
		}

		if (selectedTower.upgrader.hasDamageUpgrade)
		{
			TowerUpgradeController upgrader = selectedTower.upgrader;

			textDamage.text = "Upgrade Damage( " + upgrader.damageUpgrades[upgrader.currentDamageUpgrade].cost + " )";

		}
		else
		{
			textDamage.text = "MAX";
			showMessage("Max Upgrade Damage");

		}

	}

	public void UpgradeRange()
	{

		TowerUpgradeController upgrader = selectedTower.upgrader;
		if (upgrader.hasRangeUpgrade)
		{
			if (Money.instance.SpendMoney(upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost))
			{
				upgrader.upgradeRange();
				SetupPanel();
			}
		}


	}

	public void UpgradeFirerate()
	{
		TowerUpgradeController upgrader = selectedTower.upgrader;
		if (upgrader.hasFirerateUpgrade)
		{
			if (Money.instance.SpendMoney(upgrader.firerateUpgrades[upgrader.currentFirerateUpgrade].cost))
			{
				upgrader.upgradeFirerate();
				SetupPanel();
			}
		}

	}
	public void UpgradeDamage()
	{
		TowerUpgradeController upgrader = selectedTower.upgrader;
		if (upgrader.hasDamageUpgrade)
		{
			if (Money.instance.SpendMoney(upgrader.damageUpgrades[upgrader.currentDamageUpgrade].cost))
			{
				upgrader.upgradeDamage();
				SetupPanel();
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