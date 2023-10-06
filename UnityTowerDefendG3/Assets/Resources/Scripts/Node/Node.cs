using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public GameObject[] Towers;
    public GameObject TowerSelectionPopup;
    private GameObject currentTower = null;

    private bool onClick = false;
    private void Start()
    {
        TowerSelectionPopup.SetActive(false);
        TowerSelectionPopup.transform.Find("Button1").GetComponent<Button>().onClick.AddListener(() => BuyTower(Towers[0]));
        TowerSelectionPopup.transform.Find("Button2").GetComponent<Button>().onClick.AddListener(() => BuyTower(Towers[1]));
        TowerSelectionPopup.transform.Find("Button3").GetComponent<Button>().onClick.AddListener(() => BuyTower(Towers[2]));

    }

    public void Print() {
        Debug.Log("dsadas");
    }


    

    private void OnMouseDown()
    {
        onClick = true;
        if (TowerSelectionPopup.activeSelf)
        {
            HideTowerSelectionPopup();
        }
        else
        {
            ShowTowerSelectionPopup();
        }
    }

    private void ShowTowerSelectionPopup()
    {
        Vector3 addPos = transform.position + Vector3.up;
        TowerSelectionPopup.transform.position = addPos;
        TowerSelectionPopup.SetActive(true);


    }

    private void HideTowerSelectionPopup()
    {
        TowerSelectionPopup.SetActive(false);
    }

    public void BuyTower(GameObject selectedTower)
    {

        if (this.onClick) {
            if (currentTower)
            {
                Destroy(currentTower);
            }

            currentTower = Instantiate(selectedTower, transform.position, Quaternion.identity, this.transform);            
            onClick = false;
            HideTowerSelectionPopup();
        }
       
    }
}