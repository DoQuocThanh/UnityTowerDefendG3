using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    // public GameObject[] Towers;
    public GameObject Panel_money;
    public GameObject Panel;
    public Tower TurretLoaded { get; set; }
    public TowerItem TurretItemLoaded { get; set; }
    private void Start()
    {
        Panel_money.SetActive(true);
        Panel.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (TurretItemLoaded == null)
        {
            
                if (Money.instance.SelectedCard != null)
                {
                    TurretItemLoaded = Money.instance.SelectedCard.towerItem;
                    if (Money.instance.SpendMoney(TurretItemLoaded.cost))
                    {
                        TurretLoaded = Instantiate(TurretItemLoaded.towerPrefab, transform);
                        TurretLoaded.transform.localPosition = Vector3.zero;
                        Money.instance.SelectedCard = null;
                    }
                
                }
        }
        else
        {
            Panel.SetActive(true);
        }

    }




}