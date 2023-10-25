using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public List<Tower> TowerList = new List<Tower>();
    private int count = 0;
    public LayerMask whatIsPlacement, whatIsObstacle;
    public Transform indicator;
    public bool isPlacing;
    [HideInInspector]
    private TowerItem towerItem;
    public Tower selectedTower;

    public GameObject selectedTowerEffect;
    private void Awake()    
    {
        Instance = this;
    }
    void Start()
    {
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
        placeTower.GetComponentInChildren<CapsuleCollider2D>().enabled = false;

        indicator = placeTower.transform;
        placeTower.getRange();
    }
}
