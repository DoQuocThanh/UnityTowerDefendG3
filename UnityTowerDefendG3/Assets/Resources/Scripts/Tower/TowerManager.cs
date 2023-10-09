using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [Header("References")]
    public Image towerInforImg;
    public Image projectile;
    public TextMeshProUGUI nameTmp;
    public static TowerManager instance;

    public TowerItem[] towers;
    public Transform container;
    public Tower TurretLoaded { get; set; }
    public TowerItem TurretItemLoaded { get; set; }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadStore();
    }

    private void LoadStore()
    {
        if (Money.instance.SelectedCard != null)
        {
           /* TurretLoaded = Money.instance.SelectedCard.towerItem.towerPrefab;
            towerInforImg.sprite = TurretLoaded.towerInforImg;
            Instantiate(towerInforImg, container);
            nameTmp.text = TurretItemLoaded.cost.ToString();
            Instantiate(nameTmp, container);
            projectile.sprite = TurretItemLoaded.Projectile;
            Instantiate(projectile, container);*/
        }
    }
}
