using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class TowerItem : ScriptableObject
{
    [Header ("Config tower")]
    public string ID;
    public Tower towerPrefab;
    public Sprite Image;
    public Sprite CoinImg;
    public int cost;

    [Header("Projectile")]
    public Sprite towerInforImg;
    public Sprite Projectile;
}