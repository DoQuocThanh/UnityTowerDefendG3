using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeHoang : MonoBehaviour
{
    // public GameObject[] Towers;
    public GameObject Panel;
    public Tower TurretLoaded { get; set; }
    public TowerItem TurretItemLoaded { get; set; }
    private void Start()
    {
        Panel.SetActive(false);
    }

    private void OnMouseDown()
    {
        
    Panel.SetActive(true);
        

    }

  
    
}