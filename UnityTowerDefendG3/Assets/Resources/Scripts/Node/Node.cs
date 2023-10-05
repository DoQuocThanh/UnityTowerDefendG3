using System;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public GameObject[] Towers; // An array of Tower prefabs
    public GameObject TowerSelectionPopup;
    private bool hasTower = false;

    private void Start()
    {
        TowerSelectionPopup.SetActive(false);
        
        // Setup listeners for each button
        TowerSelectionPopup.transform.Find("Button1").GetComponent<Button>().onClick.AddListener(() => BuyTower(Towers[0]));
        TowerSelectionPopup.transform.Find("Button2").GetComponent<Button>().onClick.AddListener(() => BuyTower(Towers[1]));
        TowerSelectionPopup.transform.Find("Button3").GetComponent<Button>().onClick.AddListener(() => BuyTower(Towers[2]));
       
    }

    private void OnMouseDown()
    {
        if (!hasTower)
        {
            Debug.Log(hasTower);
            ShowTowerSelectionPopup();
        }
    }

    private void ShowTowerSelectionPopup()
    {
        TowerSelectionPopup.SetActive(true);
    }

    public void BuyTower(GameObject selectedTower)
    {
        Instantiate(selectedTower, transform.position, Quaternion.identity);
        hasTower = true;
        TowerSelectionPopup.SetActive(false);
    }
}
